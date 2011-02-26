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

        private MSLabel idleVolunteersLabel;
        private MSLabel totalVolunteersLabel;
        private MSLabel fundsLabel;

        public bool Paused { get; set; }

        private MSButton mainMenuButton;
        public MSButton MainMenuButton { get { return mainMenuButton; } }

        private MSButton optionsButton;
        public MSButton OptionsButton { get { return optionsButton; } }

        private MSButton exitButton;
        public MSButton ExitButton { get { return exitButton; } }

        private MSAnimatingButton openInGameMenu;
        public MSAnimatingButton OpenInGameMenu { get { return openInGameMenu; } }

        private MSAnimatingButton closeInGameMenu;
        public MSAnimatingButton CloseInGameMenu { get { return closeInGameMenu; } }

        public MSDistrictScreen(String filename, MoodSwing game)
            : base(null /*game.Content.Load<Texture2D>("space")*/, 0, 0, 0, 0, game.SpriteBatch, game) 
        {
            map = new MSMap(filename);
            citizensList = new List<MSCitizen>();
            unitHandler = MSUnitHandler.GetInstance();
            moodManager = MSMoodManager.GetInstance();

            foreach (MS3DTile tile in map.MapArray)
            {
                tile.LightSource = map.LightSource;
            }

            idleVolunteersLabel = new MSLabel(
                "0",
                new Rectangle(125, 15, 10, 12),
                game.Content.Load<SpriteFont>("TopPanel"),
                Color.White,
                spriteBatch,
                game);

            totalVolunteersLabel = new MSLabel(
                "0/0",
                new Rectangle(210, 20, 25, 12),
                game.Content.Load<SpriteFont>("TopPanel"),
                Color.White,
                spriteBatch,
                game);

            fundsLabel = new MSLabel(
                "0",
                new Rectangle(350, 30, 60, 12),
                game.Content.Load<SpriteFont>("TopPanel"),
                Color.White,
                spriteBatch,
                game);

            MSPanel topPanel = new MSPanel(
                game.Content.Load<Texture2D>("GamePanel/TopPanel"),
                new Rectangle(0, 0, 1024, 71),
                Shape.AMORPHOUS,
                spriteBatch,
                game);

            topPanel.AddComponent(idleVolunteersLabel);
            topPanel.AddComponent(totalVolunteersLabel);
            topPanel.AddComponent(fundsLabel);

            AddComponent(topPanel, Alignment.TOP_CENTER);

            mainMenuButton = new MSButton(
                null,
                new OpenMainScreen(),
                new Rectangle(403, 72, 226, 57),
                game.Content.Load<Texture2D>("GamePanel/MainMenu"),
                game.Content.Load<Texture2D>("GamePanel/mainmenuhover"),
                game.Content.Load<Texture2D>("GamePanel/mainmenuhover"),
                Shape.AMORPHOUS,
                spriteBatch,
                game);

            optionsButton = new MSButton(
                null,
                new OpenOptionsScreen(),
                new Rectangle(431, 131, 237, 57),
                game.Content.Load<Texture2D>("GamePanel/Options"),
                game.Content.Load<Texture2D>("GamePanel/optionshover"),
                game.Content.Load<Texture2D>("GamePanel/optionshover"),
                Shape.AMORPHOUS,
                spriteBatch,
                game);

            exitButton = new MSButton(
                null,
                new Exit(),
                new Rectangle(460, 190, 277, 57),
                game.Content.Load<Texture2D>("GamePanel/quit"),
                game.Content.Load<Texture2D>("GamePanel/quithover"),
                game.Content.Load<Texture2D>("GamePanel/quithover"),
                Shape.AMORPHOUS,
                spriteBatch,
                game);

            AddComponent(mainMenuButton);
            AddComponent(optionsButton);
            AddComponent(exitButton);

            mainMenuButton.Visible = false;
            optionsButton.Visible = false;
            exitButton.Visible = false;

            openInGameMenu = new MSAnimatingButton(
                   null,
                   new OpenInGameMenu(),
                   new Rectangle(465, 25, 111, 110),
                   game.Content.Load<Texture2D>("GamePanel/Logo"),
                   game.Content.Load<Texture2D>("GamePanel/Logo"),
                   game.Content.Load<Texture2D>("GamePanel/Logo"),
                   Color.White,
                   Shape.RECTANGULAR,
                   SpriteBatch,
                   Game);
            openInGameMenu.UnclickPosition = new MoodButtonOpenMovement();
            openInGameMenu.UnclickTimerLimit = 10;

            closeInGameMenu = new MSAnimatingButton(
                   null,
                   new CloseInGameMenu(),
                   new Rectangle(639, 215, 111, 110),
                   game.Content.Load<Texture2D>("GamePanel/Logo"),
                   game.Content.Load<Texture2D>("GamePanel/Logo"),
                   game.Content.Load<Texture2D>("GamePanel/Logo"),
                   Color.White,
                   Shape.RECTANGULAR,
                   SpriteBatch,
                   Game);

            closeInGameMenu.UnclickPosition = new MoodButtonCloseMovement();
            closeInGameMenu.UnclickTimerLimit = 10;
            closeInGameMenu.Visible = false;

            AddComponent(openInGameMenu);
            AddComponent(closeInGameMenu);

            resourceManager = new MSResourceManager(1000, game);

            Paused = false;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (MSCitizen citizen in citizensList)
            {
                citizen.Draw(gameTime);
            }

            map.Draw(gameTime);

            base.Draw(gameTime);

            if(moodManager.Mood > 0.9f)
                spriteBatch.DrawString(Game.Content.Load<SpriteFont>("Temp"), "Awesome Mood Reached", new Vector2(500, 10), Color.White);
            else if(moodManager.Mood > 0.75f)
                spriteBatch.DrawString(Game.Content.Load<SpriteFont>("Temp"), "Good Mood Reached", new Vector2(500, 10), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleMouseInput((Game as MoodSwing).OldMouseState);

            if (!Paused)
            {
                map.Update(gameTime);
                resourceManager.Update(gameTime);
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

                idleVolunteersLabel.Text = resourceManager.IdleVolunteers + "";
                totalVolunteersLabel.Text = resourceManager.TotalVolunteers + "/" + resourceManager.VolunteerCapacity;
                fundsLabel.Text = resourceManager.Funds + "";
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
