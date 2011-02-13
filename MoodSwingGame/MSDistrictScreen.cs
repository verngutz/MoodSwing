using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using MoodSwingCoreComponents;
using MoodSwingGUI;

namespace MoodSwingGame
{
    public class MSDistrictScreen: MSScreen
    {
        private static MSDistrictScreen districtScreen;
        public static MSDistrictScreen getInstance()
        {
            if (districtScreen == null)
                districtScreen = new MSDistrictScreen(MoodSwing.getInstance());
            return districtScreen;
        }

        private MSMap map;
        private List<MS3DComponent> elementsList;
        private MSUnitHandler unitHandler;
        private MSMoodManager moodManager;

        private MSDistrictScreen(MoodSwing game)
            : base(null, 150, 150, 150, 150, game.SpriteBatch, game) 
        {
            map = new MSMap(@"Content\mapinfo.txt");
            elementsList = new List<MS3DComponent>();
            unitHandler = MSUnitHandler.getInstance();
            moodManager = MSMoodManager.getInstance();

            foreach (MSTile tile in map.MapArray)
            {
                if (tile is MS3DComponent) elementsList.Add(tile as MS3DComponent);
            }

            AddComponent(new MSButton(
                    null,
                    new Exit(),
                    new Rectangle(0, 0, 574, 60),
                    game.Content.Load<Texture2D>("exit"),
                    game.Content.Load<Texture2D>("exitclicked"),
                    game.Content.Load<Texture2D>("exit"),
                    Color.White,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game)
                    , Alignment.TOP_CENTER);

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            elementsList.Sort();
            foreach( MS3DComponent temp in elementsList ) 
            {
                temp.Draw(gameTime);
            }
            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("Temp"), "" + moodManager.Mood, new Vector2(10, 10), new Color(0, 0, 0));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            
            checkCollision();
            HandleMouseInput((Game as MoodSwing).OldMouseState);
            map.Update(gameTime);
            MSUnit person = unitHandler.TryForBaby(map);
            if (person != null)
            {
                elementsList.Add(person.Get3DComponent());
            }

            List<MS3DComponent> toRemove = unitHandler.Update(map);
            foreach (MS3DComponent temp in toRemove)
            {
                elementsList.Remove(temp);
            }
            foreach (MSTile tile in map.MapArray)
            {
                if (tile is MSTower)
                {
                    MSTower tower = tile as MSTower;
                    MSVolunteer volunteer = tower.sentinel(map);
                    if (volunteer != null)
                    {
                        elementsList.Add(volunteer);
                    }
                }
            }
        }

        public void checkCollision()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Released 
                && MoodSwing.getInstance().OldMouseState.LeftButton == ButtonState.Pressed)
            {
                map.CheckCollision();
            }
        }

        private Vector2 mouseMidHold;
        public override void HandleMouseInput(MouseState oldMouseState)
        {
            //Camera rotation handler
            MouseState newMouseState = Mouse.GetState();
            if (newMouseState.MiddleButton == ButtonState.Pressed)
            {
                MSCamera camera = MSCamera.getInstance();
                if (oldMouseState.MiddleButton == ButtonState.Released)
                {
                    mouseMidHold = new Vector2(newMouseState.X, newMouseState.Y);
                    camera.adjustPitchAxis();
                }
                Vector2 movement = new Vector2(newMouseState.X, newMouseState.Y) - new Vector2(oldMouseState.X, oldMouseState.Y);
                movement.X *= -1;
                if (mouseMidHold.Y < MoodSwing.getInstance().GraphicsDevice.DisplayMode.Height / 2)
                    movement *= -1;

                Vector2 midVector = new Vector2((MoodSwing.getInstance().GraphicsDevice.DisplayMode.Width / 2),
                    (MoodSwing.getInstance().GraphicsDevice.DisplayMode.Height / 2));

                float distance = Vector2.Distance(midVector, mouseMidHold);

                movement *= 100;
                movement /= distance;
                camera.rotate(movement);
            }
            else
            {
                //Camera movement using mouse
                if (newMouseState.X >= 0 && newMouseState.X <= 5)
                    MSCamera.getInstance().shift(new Vector2(1, 0));
                else if (newMouseState.X <= MoodSwing.getInstance().GraphicsDevice.Viewport.Width &&
                    newMouseState.X >= MoodSwing.getInstance().GraphicsDevice.Viewport.Width - 5)
                    MSCamera.getInstance().shift(new Vector2(-1, 0));
                else if (newMouseState.Y >= 0 && newMouseState.Y <= 5)
                    MSCamera.getInstance().shift(new Vector2(0, -1));
                else if (newMouseState.Y <= MoodSwing.getInstance().GraphicsDevice.Viewport.Height &&
                    newMouseState.Y >= MoodSwing.getInstance().GraphicsDevice.Viewport.Height - 5)
                    MSCamera.getInstance().shift(new Vector2(0, 1));

                int delta = (newMouseState.ScrollWheelValue - oldMouseState.ScrollWheelValue);
                if (delta != 0)
                    MSCamera.getInstance().zoom(delta / Math.Abs(delta));


            }
            base.HandleMouseInput(oldMouseState);
        }
    }
}
