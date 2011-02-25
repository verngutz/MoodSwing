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
    public class MSDistrictScreen : MSScreen
    {
        private MSMap map;
        public MSMap Map { get { return map; } }

        private List<MSCitizen> citizensList;
        private MSUnitHandler unitHandler;
        private MSMoodManager moodManager;
        public MSBuyDialog BuyDialog { set; get; }

        private MSResourceManager resourceManager;
        public MSResourceManager ResourceManager { get { return resourceManager; } }

        public MSDistrictScreen(String filename, MoodSwing game)
            : base(null /*game.Content.Load<Texture2D>("space")*/, 150, 150, 150, 150, game.SpriteBatch, game) 
        {
            map = new MSMap(filename);
            citizensList = new List<MSCitizen>();
            unitHandler = MSUnitHandler.getInstance();
            moodManager = MSMoodManager.GetInstance();

            foreach (MS3DTile tile in map.MapArray)
            {
                tile.LightSource = map.LightSource;
            }
            AddComponent(new MSButton(
                    null,
                    new Exit(),
                    new Rectangle(0, 0, 574, 60),
                    game.Content.Load<Texture2D>("exit"),
                    game.Content.Load<Texture2D>("exitclicked"),
                    game.Content.Load<Texture2D>("exitclicked"),
                    Color.White,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game)
                    , Alignment.TOP_CENTER);

            resourceManager = new MSResourceManager(1000, game);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (MSCitizen citizen in citizensList)
            {
                citizen.Draw(gameTime);
            }

            map.Draw(gameTime);

            base.Draw(gameTime);

            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("Temp"), "$: " + resourceManager.Funds, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("Temp"), "Tulog na Tao: " + resourceManager.IdleVolunteers, new Vector2(10, 35), Color.White);
            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("Temp"), "Lahat ng Tao/Ilan Lang Pwede: " + resourceManager.TotalVolunteers + "/" + resourceManager.VolunteerCapacity, new Vector2(10, 60), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            map.Update(gameTime);
            resourceManager.Update(gameTime);

            HandleMouseInput((Game as MoodSwing).OldMouseState);
            MSUnit person = unitHandler.TryForBaby(map);
            if (person as MSCitizen != null)
            {
                citizensList.Add(person as MSCitizen);
                (person as MSCitizen).LightSource = map.LightSource;
            }

            List<MSCitizen> toRemove = unitHandler.Update(map);
            foreach (MSCitizen citizen in toRemove)
            {
                citizensList.Remove(citizen);
            }
            foreach (MS3DTile tile in map.MapArray)
            {
                if (tile is MSTower)
                {
                    MSTower tower = tile as MSTower;
                    MSVolunteer volunteer = tower.sentinel(map);
                    if (volunteer != null)
                    {
                        citizensList.Add(volunteer);
                        volunteer.LightSource = map.LightSource;
                    }
                }
            }
        }

        public void CheckCollision()
        {
            MS3DTile tile = map.CheckCollision();
            if (tile is MSBuyableBuilding)
            {
                BuyDialog = new MSBuyDialog(Game.Content.Load<Texture2D>("CityView"), new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 190, 70), tile as MSBuyableBuilding, Shape.RECTANGULAR, spriteBatch, Game);
                AddComponent(BuyDialog);
            }
        }

        private Vector2 mouseMidHold;
        public override void HandleMouseInput(MouseState oldMouseState)
        {
            MouseState newMouseState = Mouse.GetState();

            base.HandleMouseInput(oldMouseState);

            //Picking
            if (newMouseState.LeftButton == ButtonState.Released
                && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                bool willCheckCollision = false;

                if (BuyDialog == null)
                    willCheckCollision = true;
                else if (!BuyDialog.CollidesWithMouse())
                    willCheckCollision = true;

                if (BuyDialog != null)
                    RemoveComponent(BuyDialog);

                if (willCheckCollision)
                    CheckCollision();
            }

            //Camera Rotation
            else if (newMouseState.MiddleButton == ButtonState.Pressed)
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
                if (BuyDialog != null)
                    RemoveComponent(BuyDialog);
            }
            else
            {
                bool hasMoved = false;
                //Camera movement using mouse
                if (newMouseState.X >= 0 && newMouseState.X <= 5)
                {
                    MSCamera.GetInstance().Shift(new Vector2(1, 0));
                    hasMoved = true;
                }
                else if (newMouseState.X <= MoodSwing.getInstance().GraphicsDevice.Viewport.Width &&
                    newMouseState.X >= MoodSwing.getInstance().GraphicsDevice.Viewport.Width - 5)
                {
                    MSCamera.GetInstance().Shift(new Vector2(-1, 0));
                    hasMoved = true;
                }
                else if (newMouseState.Y >= 0 && newMouseState.Y <= 5)
                {
                    MSCamera.GetInstance().Shift(new Vector2(0, -1));
                    hasMoved = true;
                }
                else if (newMouseState.Y <= MoodSwing.getInstance().GraphicsDevice.Viewport.Height &&
                    newMouseState.Y >= MoodSwing.getInstance().GraphicsDevice.Viewport.Height - 5)
                {
                    MSCamera.GetInstance().Shift(new Vector2(0, 1));
                    hasMoved = true;
                }

                int delta = (newMouseState.ScrollWheelValue - oldMouseState.ScrollWheelValue);
                if (delta != 0)
                {
                    MSCamera.GetInstance().Zoom(delta / Math.Abs(delta));
                    hasMoved = true;
                }

                if (hasMoved && BuyDialog != null)
                    RemoveComponent(BuyDialog);  
            }
        }
    }
}
