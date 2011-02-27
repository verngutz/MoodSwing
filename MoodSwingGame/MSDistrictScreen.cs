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

        //private List<MSCitizen> citizensList;
        private MSUnitHandler unitHandler;
        private MSMoodManager moodManager;
        public MSBuyDialog BuyDialog { set; get; }

        private MSResourceManager resourceManager;
        public MSResourceManager ResourceManager { get { return resourceManager; } }

        private MSTextField idleVolunteers;
        private MSTextField totalVolunteers;
        private MSTextField funds;

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

        private MSPanel blackOutPanel;
        public MSPanel BlackOutPanel { get { return blackOutPanel; } }

        public MSDistrictScreen(String filename, MoodSwing game)
            : base(game.Content.Load<Texture2D>("CityView"), 0, 0, 0, 0, game.SpriteBatch, game) 
        {
            map = new MSMap(filename);
            //citizensList = new List<MSCitizen>();
            MSCamera.initialize();
            unitHandler = MSUnitHandler.Restart();
            moodManager = MSMoodManager.GetInstance();
            resourceManager = MSResourceManager.GetInstance();
            MSResourceManager.instantiate(1000, map.InitialVolunteerCenters);

            foreach (MS3DTile tile in map.MapArray)
            {
                tile.LightSource = map.LightSource;
            }

            blackOutPanel = new MSPanel(
                game.Content.Load<Texture2D>("BlackOut"),
                BoundingRectangle,
                null,
                Shape.RECTANGULAR,
                spriteBatch,
                game);

            blackOutPanel.Visible = false;

            AddComponent(blackOutPanel);

            idleVolunteers = new MSTextField(
                "0",
                new Rectangle(125, 13, 10, 16),
                null,
                null,
                3,
                game.Content.Load<SpriteFont>("TopPanel"),
                Color.White,
                spriteBatch,
                game);

            totalVolunteers = new MSTextField(
                "0/0",
                new Rectangle(210, 19, 25, 16),
                null,
                null,
                7,
                game.Content.Load<SpriteFont>("TopPanel"),
                Color.White,
                spriteBatch,
                game);

            funds = new MSTextField(
                "0",
                new Rectangle(350, 28, 60, 20),
                null,
                null,
                8,
                game.Content.Load<SpriteFont>("TopPanel"),
                Color.White,
                spriteBatch,
                game);

            MSPanel topPanel = new MSPanel(
                game.Content.Load<Texture2D>("GamePanel/TopPanel"),
                new Rectangle(0, 0, 1024, 71),
                null,
                Shape.AMORPHOUS,
                spriteBatch,
                game);

            topPanel.AddComponent(idleVolunteers);
            topPanel.AddComponent(totalVolunteers);
            topPanel.AddComponent(funds);

            AddComponent(topPanel, Alignment.TOP_CENTER);

            mainMenuButton = new MSButton(
                null,
                new OpenMainScreen(),
                new Rectangle(403, 72, 226, 57),
                game.Content.Load<Texture2D>("GamePanel/MainMenu"),
                game.Content.Load<Texture2D>("GamePanel/mainmenuhover"),
                game.Content.Load<Texture2D>("GamePanel/mainmenuhover"),
                null,
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
                null, 
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
                null,
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
                   null,
                   Shape.RECTANGULAR,
                   SpriteBatch,
                   Game);
            openInGameMenu.UnclickPosition = new MoodButtonOpenMovement();
            openInGameMenu.UnclickTimerLimit = 12;

            closeInGameMenu = new MSAnimatingButton(
                   null,
                   new CloseInGameMenu(),
                   new Rectangle(639, 215, 111, 110),
                   game.Content.Load<Texture2D>("GamePanel/Logo"),
                   game.Content.Load<Texture2D>("GamePanel/Logo"),
                   game.Content.Load<Texture2D>("GamePanel/Logo"),
                   Color.White,
                   null,
                   Shape.RECTANGULAR,
                   SpriteBatch,
                   Game);

            closeInGameMenu.UnclickPosition = new MoodButtonCloseMovement();
            closeInGameMenu.UnclickTimerLimit = 12;
            closeInGameMenu.Visible = false;


            AddComponent(closeInGameMenu);
            AddComponent(openInGameMenu);
            
            Paused = false;
        }

        public override void Draw(GameTime gameTime)
        {
            if (background != null)
                SpriteBatch.Draw(background, BoundingRectangle, highlight);

            SpriteBatch.End(); 
            map.Draw(gameTime);

            foreach (MSCitizen citizen in unitHandler.Citizens)
            {
                citizen.Draw(gameTime);
            }

            SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None);
            foreach (MSCitizen citizen in unitHandler.Citizens)
            {
                if(citizen.state == MSCitizen.CitizenState.MOB) 
                {
                    //This is where the position for the MoodFace gets updated when it goes out of bounds
                    Rectangle boundingRectangle = citizen.MoodFace.BoundingRectangle;
                    Vector2 position = citizen.MoodFace.Position;
                    SpriteEffects effect = SpriteEffects.None;

                    if (position.X < 0)
                        position.X = 0;
                    else if (position.X + boundingRectangle.Width > MoodSwing.getInstance().GraphicsDevice.Viewport.Width)
                    {
                        effect = SpriteEffects.FlipHorizontally;
                        position.X = MoodSwing.getInstance().GraphicsDevice.Viewport.Width - boundingRectangle.Width;
                    }

                    if (position.Y < 0)
                    {
                        effect = SpriteEffects.FlipVertically;
                        position.Y = 0;
                    }
                    else if (position.Y + boundingRectangle.Height > MoodSwing.getInstance().GraphicsDevice.Viewport.Height)
                        position.Y = MoodSwing.getInstance().GraphicsDevice.Viewport.Height - boundingRectangle.Height;

                    boundingRectangle = new Rectangle((int)position.X, (int)position.Y, boundingRectangle.Width, boundingRectangle.Height);
                    (Game as MoodSwing).SpriteBatch.Draw(citizen.MoodFace.Image,boundingRectangle , null, Color.White, 0, Vector2.Zero, effect, position.Y / Game.GraphicsDevice.Viewport.Height);
                }
            }
            SpriteBatch.End();

            SpriteBatch.Begin();


            foreach (MS3DTile tile in map.MapArray)
            {
                if (tile is MSBuyableBuilding)
                    (tile as MSBuyableBuilding).DrawLoadingBar(gameTime);
            }

            foreach (MSGUIUnclickable element in Components)
                if (element.Visible)
                    element.Draw(gameTime);

            foreach (MSGUIClickable element in ClickableComponents)
                if (element.Visible)
                    element.Draw(gameTime);

            if (currentHovered != null && currentHovered.ToolTip != null)
                currentHovered.ToolTip.Draw(gameTime);

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
                    (person as MSCitizen).LightSource = map.LightSource;
                }

                List<MSCitizen> toRemove = unitHandler.Update(map);

                foreach (MS3DTile tile in map.MapArray)
                {
                    if (tile is MSTower)
                    {
                        MSTower tower = tile as MSTower;
                        MSVolunteer volunteer = tower.sentinel(map);
                    }
                }

                idleVolunteers.Text = resourceManager.IdleVolunteers + "";
                totalVolunteers.Text = resourceManager.TotalVolunteers + "/" + resourceManager.VolunteerCapacity;
                funds.Text = resourceManager.Funds + "";

            }    
        }

        public void CheckCollision()
        {
            MS3DTile tile = map.CheckCollision();
            if (tile is MSBuyableBuilding && 
                (tile as MSBuyableBuilding).State == MSBuyableBuilding.BuyableBuildingState.BUYABLE)
            {
                BuyDialog = new MSBuyDialog(Game.Content.Load<Texture2D>("CityView"), new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 190, 190), tile as MSBuyableBuilding, Shape.RECTANGULAR, spriteBatch, Game);
                AddComponent(BuyDialog);
            }
        }

        private Vector2 mouseMidHold;
        public void HandleMouseInput(MouseState oldMouseState)
        {
            MouseState newMouseState = Mouse.GetState();

            if (base.HandleMouseInput(oldMouseState))
            {
                if (BuyDialog != null)
                    RemoveComponent(BuyDialog);
                BuyDialog = null;
                return;
            }

            //Picking
            else if (newMouseState.LeftButton == ButtonState.Released
                    && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                if (BuyDialog != null)
                    RemoveComponent(BuyDialog);
                BuyDialog = null;
                CheckCollision();
            }

            //Camera Rotation
            else if (newMouseState.MiddleButton == ButtonState.Pressed && !Paused)
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
                BuyDialog = null;
            }
            else if( !Paused )
            {
                bool hasMoved = false;
                Vector2 shift = Vector2.Zero;
                //Camera movement using mouse
                if (newMouseState.X >= 0 && newMouseState.X <= 5)
                {
                    shift = new Vector2(1, 0);
                    hasMoved = true;
                }
                else if (newMouseState.X <= MoodSwing.getInstance().GraphicsDevice.Viewport.Width &&
                    newMouseState.X >= MoodSwing.getInstance().GraphicsDevice.Viewport.Width - 5)
                {
                    shift = new Vector2(-1, 0);
                    hasMoved = true;
                }
                else if (newMouseState.Y >= 0 && newMouseState.Y <= 5)
                {
                    shift = new Vector2(0, -1);
                    hasMoved = true;
                }
                else if (newMouseState.Y <= MoodSwing.getInstance().GraphicsDevice.Viewport.Height &&
                    newMouseState.Y >= MoodSwing.getInstance().GraphicsDevice.Viewport.Height - 5)
                {
                    shift = new Vector2(0, 1);
                    hasMoved = true;
                }
                MSCamera.GetInstance().Shift(shift, map.Dimension);
                int delta = (newMouseState.ScrollWheelValue - oldMouseState.ScrollWheelValue);
                if (delta != 0)
                {
                    MSCamera.GetInstance().Zoom(delta / Math.Abs(delta));
                    hasMoved = true;
                }

                if (hasMoved)
                {
                    if (BuyDialog != null)
                        RemoveComponent(BuyDialog);
                    BuyDialog = null;
                }
            }
        }
    }
}
