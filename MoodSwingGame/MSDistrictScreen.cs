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
            {
                districtScreen = new MSDistrictScreen(MoodSwing.getInstance());
                districtScreen.Initialize();
            }
            return districtScreen;
        }

        private MSMap map;
        private List<MS3DComponent> elementsList;
        private MSUnitHandler unitHandler;
        private MSMoodManager moodManager;

        private MSDistrictScreen(MoodSwing game)
            : base(null /*game.Content.Load<Texture2D>("space")*/, 150, 150, 150, 150, game.SpriteBatch, game) 
        {
            map = new MSMap(@"Content\mapinfo.txt");
            elementsList = new List<MS3DComponent>();
            unitHandler = MSUnitHandler.getInstance();
            moodManager = MSMoodManager.GetInstance();

            foreach (MS3DTile tile in map.MapArray)
            {
                elementsList.Add(tile);
                tile.LightSource = map.LightSource;
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
            foreach (MS3DComponent temp in elementsList)
            {
                temp.Draw(gameTime);
            }

            base.Draw(gameTime);
            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("Temp"), "" + moodManager.Mood, new Vector2(10, 10), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleMouseInput((Game as MoodSwing).OldMouseState);
            MSUnit person = unitHandler.TryForBaby(map);
            if (person as MSCitizen != null)
            {
                elementsList.Add(person as MSCitizen);
                (person as MSCitizen).LightSource = map.LightSource;
            }

            List<MS3DComponent> toRemove = unitHandler.Update(map);
            foreach (MS3DComponent temp in toRemove)
            {
                elementsList.Remove(temp);
            }
            foreach (MS3DTile tile in map.MapArray)
            {
                if (tile is MSTower)
                {
                    MSTower tower = tile as MSTower;
                    MSVolunteer volunteer = tower.sentinel(map);
                    if (volunteer != null)
                    {
                        elementsList.Add(volunteer);
                        volunteer.LightSource = map.LightSource;
                    }
                }
            }
        }

        public void CheckCollision()
        {
            MS3DTile tile = map.CheckCollision();
            if (tile != null)
                System.Console.WriteLine("Tile Found");
            if (tile is MSBuyableBuilding)
            {
                elementsList.Remove(tile as MS3DComponent);
                System.Console.WriteLine( (tile as MSBuyableBuilding).TileModel.ToString() );
                MS3DTile newTile = new MSTower(MoodSwing.getInstance().Content.Load<Model>("districthall"), MoodSwing.getInstance().Content.Load<Texture2D>("MTextures/building_texture"),
                    MoodSwing.getInstance().Content.Load<Effect>("Mood"), (tile as MS3DComponent).Position);
                map.Change(tile,newTile );
                elementsList.Add(newTile as MS3DComponent);
            }
        }

        private Vector2 mouseMidHold;
        public override void HandleMouseInput(MouseState oldMouseState)
        {
            MouseState newMouseState = Mouse.GetState();

            //Picking
            if (newMouseState.LeftButton == ButtonState.Released
                && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                CheckCollision();
            }

            //Camera Rotation
            if (newMouseState.MiddleButton == ButtonState.Pressed)
            {
                MSCamera camera = MSCamera.GetInstance();
                if (oldMouseState.MiddleButton == ButtonState.Released)
                {
                    mouseMidHold = new Vector2(newMouseState.X, newMouseState.Y);
                    camera.AdjustPitchAxis();
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
                camera.Rotate(movement);
            }
            else
            {
                //Camera movement using mouse
                if (newMouseState.X >= 0 && newMouseState.X <= 5)
                    MSCamera.GetInstance().Shift(new Vector2(1, 0));
                else if (newMouseState.X <= MoodSwing.getInstance().GraphicsDevice.Viewport.Width &&
                    newMouseState.X >= MoodSwing.getInstance().GraphicsDevice.Viewport.Width - 5)
                    MSCamera.GetInstance().Shift(new Vector2(-1, 0));
                else if (newMouseState.Y >= 0 && newMouseState.Y <= 5)
                    MSCamera.GetInstance().Shift(new Vector2(0, -1));
                else if (newMouseState.Y <= MoodSwing.getInstance().GraphicsDevice.Viewport.Height &&
                    newMouseState.Y >= MoodSwing.getInstance().GraphicsDevice.Viewport.Height - 5)
                    MSCamera.GetInstance().Shift(new Vector2(0, 1));

                int delta = (newMouseState.ScrollWheelValue - oldMouseState.ScrollWheelValue);
                if (delta != 0)
                    MSCamera.GetInstance().Zoom(delta / Math.Abs(delta));


            }
            base.HandleMouseInput(oldMouseState);
        }


    }
}
