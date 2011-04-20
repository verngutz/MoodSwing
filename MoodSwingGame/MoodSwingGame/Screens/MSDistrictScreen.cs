﻿using System;
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
        public MSCircularPicker BuyDialog { set; get; }

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

        private MSNotifier storyBox;
        public MSNotifier StoryBox { get { return storyBox; } }

        public MSDistrictScreen(String filename, MoodSwing game)
            : base(game.Content.Load<Texture2D>("districtmap"), 0, 0, 0, 0, game.SpriteBatch, game)
        {
            map = new MSMap(filename);
            MSCamera.initialize((game as MoodSwing).GraphicsDevice.Viewport,
                MSDistrictHall.getInstance().Position, MSDistrictHall.getInstance().Rotation);
            //citizensList = new List<MSCitizen>();
            unitHandler = MSUnitHandler.Restart();
            MSMoodManager.Reset();
            moodManager = MSMoodManager.GetInstance();
            resourceManager = MSResourceManager.GetInstance();
            MSResourceManager.instantiate(1000, map.InitialVolunteerCenters);

            storyBox = new MSNotifier(Game.Content.Load<Texture2D>("BlackOut"), new Rectangle(0, 400, 1024, 100), Shape.RECTANGULAR, SpriteBatch, Game);

            foreach (MS3DTile tile in map.MapArray)
            {
                tile.LightSource = map.LightSource;
            }

            blackOutPanel = new MSPanel
            (
                game.Content.Load<Texture2D>("BlackOut"),
                BoundingRectangle,
                null,
                Shape.RECTANGULAR,
                spriteBatch,
                game
            );
            blackOutPanel.Visible = false;
            AddComponent(blackOutPanel);

            MSToolTip idleVolunteersToolTip = new MSToolTip
            (
                null,
                new Rectangle(52, 25, 50, 14),
                SpriteBatch,
                Game
            );
            MSPanel idleVolunteersPanel = new MSPanel
            (
                null,
                new Rectangle(52, 25, 50, 14),
                idleVolunteersToolTip,
                Shape.RECTANGULAR,
                SpriteBatch,
                Game
            );
            idleVolunteers = new MSTextField
            (
                "0",
                new Rectangle(82, 25, 20, 14),
                null,
                null,
                4,
                game.Content.Load<SpriteFont>("TopPanel"),
                Color.White,
                spriteBatch,
                game
            );
            idleVolunteersPanel.AddComponent(idleVolunteers);
            idleVolunteersToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(82, 39),
                "Idle Volunteers",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                3,
                3,
                3,
                3,
                SpriteBatch,
                Game
            ));


            MSToolTip totalVolunteersToolTip = new MSToolTip
            (
                null,
                new Rectangle(152, 25, 80, 14),
                SpriteBatch,
                Game
            );
            MSPanel totalVolunteersPanel = new MSPanel
            (
                null,
                new Rectangle(152, 25, 80, 14),
                totalVolunteersToolTip,
                Shape.RECTANGULAR,
                SpriteBatch,
                Game
            );
            totalVolunteers = new MSTextField(
                "0/0",
                new Rectangle(192, 34, 40, 14),
                null,
                null,
                9,
                game.Content.Load<SpriteFont>("TopPanel"),
                Color.White,
                spriteBatch,
                game);
            totalVolunteersPanel.AddComponent(totalVolunteers);
            totalVolunteersToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(192, 48),
                "Total Volunteers/Volunteers Manageable",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                3,
                3,
                3,
                3,
                SpriteBatch,
                Game
            ));

            MSToolTip fundsToolTip = new MSToolTip
            (
                null,
                new Rectangle(262, 44, 150, 20),
                SpriteBatch,
                Game
            );
            MSPanel fundsPanel = new MSPanel
            (
                null,
                new Rectangle(262, 44, 150, 20),
                fundsToolTip,
                Shape.RECTANGULAR,
                SpriteBatch,
                Game
            );
            funds = new MSTextField
            (
                "0",
                new Rectangle(332, 44, 80, 20),
                null,
                null,
                8,
                game.Content.Load<SpriteFont>("TopPanel"),
                Color.White,
                spriteBatch,
                game
            );
            fundsPanel.AddComponent(funds);
            fundsToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(332, 64),
                "Funds",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                3,
                3,
                3,
                3,
                SpriteBatch,
                Game
            ));

            MSPanel topPanelBack = new MSPanel
            (
                Game.Content.Load<Texture2D>("GamePanel/WhiteBG"),
                new Rectangle(0, 0, 1024, 91),
                null,
                Shape.AMORPHOUS,
                SpriteBatch,
                Game
            );

            AddComponent(topPanelBack, Alignment.TOP_CENTER);

            MSPanel topPanel = new MSPanel
            (
                Game.Content.Load<Texture2D>("GamePanel/TopPanel"),
                new Rectangle(0, 0, 1024, 91),
                null,
                Shape.AMORPHOUS,
                SpriteBatch,
                Game
            );

            topPanel.AddComponent(idleVolunteersPanel);
            topPanel.AddComponent(totalVolunteersPanel);
            topPanel.AddComponent(fundsPanel);

            AddComponent(topPanel, Alignment.TOP_CENTER);

            mainMenuButton = new MSButton(
                null,
                new OpenExitConfirmation(OpenExitConfirmation.ExitType.ToMainMenu),
                new Rectangle(403, 92, 226, 57),
                game.Content.Load<Texture2D>("GamePanel/MainMenu"),
                game.Content.Load<Texture2D>("GamePanel/mainmenuclicked"),
                game.Content.Load<Texture2D>("GamePanel/mainmenuhovered"),
                null,
                Shape.AMORPHOUS,
                spriteBatch,
                game);

            optionsButton = new MSButton(
                null,
                new OpenOptionsScreen(),
                new Rectangle(431, 151, 237, 57),
                game.Content.Load<Texture2D>("GamePanel/Options"),
                game.Content.Load<Texture2D>("GamePanel/optionsclicked"),
                game.Content.Load<Texture2D>("GamePanel/optionshovered"),
                null,
                Shape.AMORPHOUS,
                spriteBatch,
                game);

            exitButton = new MSButton(
                null,
                new OpenExitConfirmation(OpenExitConfirmation.ExitType.ToWindows),
                new Rectangle(460, 210, 277, 57),
                game.Content.Load<Texture2D>("GamePanel/quit"),
                game.Content.Load<Texture2D>("GamePanel/quitclicked"),
                game.Content.Load<Texture2D>("GamePanel/quithovered"),
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
                   game.Content.Load<Texture2D>("GamePanel/LogoClicked"),
                   game.Content.Load<Texture2D>("GamePanel/LogoHovered"),
                   Color.White,
                   null,
                   Shape.CIRCULAR,
                   SpriteBatch,
                   Game);
            openInGameMenu.UnclickPosition = new SwingButtonOpenMovement();
            openInGameMenu.UnclickTimerLimit = 12;

            closeInGameMenu = new MSAnimatingButton(
                   null,
                   new CloseInGameMenu(),
                   new Rectangle(639, 235, 111, 110),
                   game.Content.Load<Texture2D>("GamePanel/Logo"),
                   game.Content.Load<Texture2D>("GamePanel/LogoClicked"),
                   game.Content.Load<Texture2D>("GamePanel/LogoHovered"),
                   Color.White,
                   null,
                   Shape.CIRCULAR,
                   SpriteBatch,
                   Game);

            closeInGameMenu.UnclickPosition = new SwingButtonCloseMovement();
            closeInGameMenu.UnclickTimerLimit = 12;
            closeInGameMenu.Visible = false;

            AddComponent(closeInGameMenu);
            AddComponent(openInGameMenu);

            AddComponent(moodManager.PovertyProgressBar);
            AddComponent(moodManager.EducationProgressBar);
            AddComponent(moodManager.GenderEqualityProgressBar);
            AddComponent(moodManager.ChildHealthProgressBar);
            AddComponent(moodManager.MaternalHealthProgressBar);
            AddComponent(moodManager.HivAidsProgressBar);
            AddComponent(moodManager.EnvironmentProgressBar);
            AddComponent(moodManager.GlobalPartnershipProgressBar);

            MSImageHolder translucentOverlay = new MSImageHolder
            (
                new Rectangle(0, 0, 1024, 91),
                Game.Content.Load<Texture2D>("GamePanel/translucent"),
                SpriteBatch,
                Game
            );

            AddComponent(translucentOverlay);

            Paused = false;
        }

        public override void Draw(GameTime gameTime)
        {
            if (background != null)
                SpriteBatch.Draw(background, BoundingRectangle, highlight);
            SpriteBatch.End();

            map.Draw(gameTime);
            foreach (MSUnit unit in unitHandler.Units)
                unit.Draw(gameTime);

            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, MSResolution.GetTransformationMatrix());
            foreach (MSUnit unit in unitHandler.Units)
            {
                if (unit is MSMobber)
                {
                    MSMobber mobber = unit as MSMobber;

                    //This is where the position for the MoodFace gets updated when it goes out of bounds
                    Rectangle boundingRectangle = mobber.MoodFace.BoundingRectangle;
                    Vector2 position = mobber.MoodFace.Position;
                    SpriteEffects effect = SpriteEffects.None;

                    if (position.X + 50 < 0 ||
                        position.X + 50 + boundingRectangle.Width > MSResolution.VirtualWidth)
                    {
                        Vector3 left = MSCamera.GetInstance().Frustum.GetCorners()[4];
                        Vector3 right = MSCamera.GetInstance().Frustum.GetCorners()[5];

                        double distLeft = Vector3.Distance(left, unit.Position);
                        double distRight = Vector3.Distance(right, unit.Position);
                        if (distLeft < distRight)
                        {
                            position.X = 0;
                            effect = SpriteEffects.FlipHorizontally;
                        }
                        else
                        {
                            position.X = MSResolution.VirtualWidth - boundingRectangle.Width;
                        }
                    }


                    int maxY = 0;
                    if (position.X >= 0 && position.X <= 465)
                        maxY = (91 - 53) / 465 * (int)(position.X) + 53;
                    else if (position.X > 465 && position.X < 540)
                        maxY = 91;
                    else if (position.X >= 540 && position.X <= 1024)
                        maxY = (91 - 53) / (540 - 1024) * (int)(position.X - 1024) + 53;

                    maxY += 20;

                    if (position.Y < maxY ||
                        position.Y + 50 + boundingRectangle.Height > MSResolution.VirtualHeight)
                    {

                        Vector3 top = MSCamera.GetInstance().Frustum.GetCorners()[4];
                        Vector3 bottom = MSCamera.GetInstance().Frustum.GetCorners()[7];

                        double distTop = Vector3.Distance(top, unit.Position);
                        double distBottom = Vector3.Distance(bottom, unit.Position);

                        if (distTop < distBottom)
                        {
                            effect = SpriteEffects.FlipVertically;
                            position.Y = maxY;
                        }
                        else
                        {
                            position.Y = MSResolution.VirtualHeight - boundingRectangle.Height;
                        }
                    }

                    boundingRectangle = new Rectangle((int)position.X, (int)position.Y, boundingRectangle.Width, boundingRectangle.Height);
                    (Game as MoodSwing).SpriteBatch.Draw(mobber.MoodFace.Image, boundingRectangle, null, Color.White, 0, Vector2.Zero, effect, position.Y / MSResolution.VirtualHeight);
                }
            }
            SpriteBatch.End();

            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, MSResolution.GetTransformationMatrix());

            foreach (MS3DTile tile in map.MapArray)
            {
                if (tile is MSChangeableBuilding)
                    (tile as MSChangeableBuilding).DrawLoadingBar(gameTime);
            }

            foreach (MSGUIObject component in components)
            {
                if (component is MSGUIClickable)
                {
                    if ((component as MSGUIClickable).Visible)
                        (component as MSGUIClickable).Draw(gameTime);
                }
                else if (component is MSGUIUnclickable)
                {
                    if ((component as MSGUIUnclickable).Visible)
                        (component as MSGUIUnclickable).Draw(gameTime);
                }
            }

            if (currentHovered != null && currentHovered.ToolTip != null)
                currentHovered.ToolTip.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleMouseInput((Game as MoodSwing).OldMouseState);

            if (!Paused)
            {
                HandleKeyboardInput(MoodSwing.GetInstance().OldKeyboardState);
                map.Update(gameTime);
                resourceManager.Update(gameTime);
                MSUnit person = unitHandler.TryForBaby(map, gameTime);

                moodManager.Update(gameTime);
                unitHandler.Update(map);

                foreach (MS3DTile tile in map.MapArray)
                {
                    if (tile is MSTower)
                    {
                        MSTower tower = tile as MSTower;
                        tower.sentinel(map, unitHandler);
                    }
                }

                idleVolunteers.Text = resourceManager.IdleVolunteers + "";
                totalVolunteers.Text = resourceManager.TotalVolunteers + "/" + resourceManager.VolunteerCapacity;
                funds.Text = resourceManager.Funds + "";
            }
        }

        public void PickFrom3DWorld()
        {
            MS3DTile tile = map.PickFrom3DWorld();
            if (tile is MSChangeableBuilding && (tile as MSChangeableBuilding).State == MSChangeableBuildingState.BUYABLE)
            {
                string texturePath = "";
                Point sourcePoint = new Point();
                if (MSMouse.GetState().X < MSResolution.VirtualWidth / 2)
                {
                    if (MSMouse.GetState().Y < MSResolution.VirtualHeight / 2)
                    {
                        texturePath = "BuyDialog/pointerNW";
                        sourcePoint = new Point(MSMouse.GetState().X, MSMouse.GetState().Y);
                    }
                    else
                    {
                        texturePath = "BuyDialog/pointerSW";
                        sourcePoint = new Point(MSMouse.GetState().X, MSMouse.GetState().Y - 260);
                    }
                }
                else
                {
                    if (MSMouse.GetState().Y < MSResolution.VirtualHeight / 2)
                    {
                        texturePath = "BuyDialog/pointerNE";
                        sourcePoint = new Point(MSMouse.GetState().X - 260, MSMouse.GetState().Y);
                    }
                    else
                    {
                        texturePath = "BuyDialog/pointerSE";
                        sourcePoint = new Point(MSMouse.GetState().X - 260, MSMouse.GetState().Y - 260);
                    }
                }

                if (tile is MSAbandonedBuilding)
                {
                    BuyDialog = new MSAbandonedBuildingPicker
                    (
                        Game.Content.Load<Texture2D>(texturePath),
                        new Rectangle(sourcePoint.X, sourcePoint.Y, 260, 260),
                        tile as MSAbandonedBuilding,
                        spriteBatch,
                        Game
                    );
                }

                else if (tile is MSTower)
                {
                    if ((tile as MSTower).Stats is MSGeneralHelpCenterStats)
                    {
                        BuyDialog = new MSGeneralHelpCenterPicker
                        (
                            Game.Content.Load<Texture2D>(texturePath),
                            new Rectangle(sourcePoint.X, sourcePoint.Y, 260, 260),
                            tile as MSTower,
                            spriteBatch,
                            Game
                        );
                    }
                }

                AddComponent(BuyDialog);
            }
        }

        private Vector2 mouseMidHold;
        public override bool HandleMouseInput(MouseState oldMouseState, bool careIfMouseHasMoved)
        {
            MouseState newMouseState = MSMouse.GetState();

            if (base.HandleMouseInput(oldMouseState, careIfMouseHasMoved))
            {
                if (BuyDialog != null)
                {
                    RemoveComponent(BuyDialog);
                    BuyDialog.UnhighlightSelected();
                }
                BuyDialog = null;
                return false;
            }

            //Picking
            else if (!Paused && newMouseState.LeftButton == ButtonState.Released
                    && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                if (BuyDialog != null)
                {
                    RemoveComponent(BuyDialog);
                    BuyDialog.UnhighlightSelected();
                }
                BuyDialog = null;
                PickFrom3DWorld();
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
                if (mouseMidHold.Y < MoodSwing.GetInstance().GraphicsDevice.DisplayMode.Height / 2)
                    movement *= -1;

                Vector2 midVector = new Vector2((MoodSwing.GetInstance().GraphicsDevice.DisplayMode.Width / 2),
                    (MoodSwing.GetInstance().GraphicsDevice.DisplayMode.Height / 2));

                float distance = Vector2.Distance(midVector, mouseMidHold);

                movement *= 100;
                movement /= distance;
                camera.Rotate(movement);
                if (BuyDialog != null)
                {
                    RemoveComponent(BuyDialog);
                    BuyDialog.UnhighlightSelected();
                }
                BuyDialog = null;
            }
            else if (!Paused)
            {
                bool hasMoved = false;
                Vector2 shift = Vector2.Zero;

                //Camera movement using mouse
                if (newMouseState.X >= 0 && newMouseState.X <= 5)
                {
                    shift += new Vector2(1, 0);
                    hasMoved = true;
                }
                else if (newMouseState.X <= MoodSwing.GetInstance().GraphicsDevice.Viewport.Width &&
                    newMouseState.X >= MoodSwing.GetInstance().GraphicsDevice.Viewport.Width - 5)
                {
                    shift += new Vector2(-1, 0);
                    hasMoved = true;
                }

                if (newMouseState.Y >= 0 && newMouseState.Y <= 5)
                {
                    shift += new Vector2(0, -1);
                    hasMoved = true;
                }
                else if (newMouseState.Y <= MoodSwing.GetInstance().GraphicsDevice.Viewport.Height &&
                    newMouseState.Y >= MoodSwing.GetInstance().GraphicsDevice.Viewport.Height - 5)
                {
                    shift += new Vector2(0, 1);
                    hasMoved = true;
                }

                //camera movement using right mouse button.
                if (newMouseState.RightButton == ButtonState.Pressed &&
                    oldMouseState.RightButton == ButtonState.Released)
                {
                    mouseMidHold = new Vector2(newMouseState.X, newMouseState.Y);
                }
                else if (newMouseState.RightButton == ButtonState.Pressed &&
                    newMouseState.RightButton == ButtonState.Pressed)
                {
                    Vector2 delta2 = new Vector2(newMouseState.X - mouseMidHold.X, newMouseState.Y - mouseMidHold.Y);

                    if (delta2.X <= -20)
                    {
                        shift += new Vector2(1, 0);
                        hasMoved = true;
                    }
                    else if (delta2.X >= 20)
                    {
                        shift += new Vector2(-1, 0);
                        hasMoved = true;
                    }
                    if (delta2.Y <= -20)
                    {
                        shift += new Vector2(0, -1);
                        hasMoved = true;
                    }
                    else if (delta2.Y >= 20)
                    {
                        shift += new Vector2(0, 1);
                        hasMoved = true;
                    }
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
                    {
                        RemoveComponent(BuyDialog);
                        BuyDialog.UnhighlightSelected();
                    }
                    BuyDialog = null;
                }
            }
            return false;
        }

        public override bool HandleMouseInput(MouseState oldMouseState)
        {
            return this.HandleMouseInput(oldMouseState, true);
        }

        public override void HandleKeyboardInput(KeyboardState oldKeyboardState)
        {
            if (oldKeyboardState.IsKeyDown(Keys.T) && Keyboard.GetState().IsKeyUp(Keys.T))
            {
                MSCamera.GetInstance().BirdsEyeView();
            }
            base.HandleKeyboardInput(oldKeyboardState);
        }
    }
}
