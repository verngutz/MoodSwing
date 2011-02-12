using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoodSwingCoreComponents;
using MoodSwingGUI;

namespace MoodSwingGame
{
    public class MSMainScreen : MSScreen
    {
        private static MSMainScreen mainScreen;
        public static MSMainScreen getInstance()
        {
            if (mainScreen == null)
                mainScreen = new MSMainScreen(MoodSwing.getInstance());
            return mainScreen;
        }

        private MSMainScreen(MoodSwing game)
            : base(game.Content.Load<Texture2D>("gamescreenbg"), 200, 200, 200, 200, Color.White, game.SpriteBatch, game)
        {
            MSAnimatingButton startGameButton = new MSAnimatingButton(
                    null,
                    new OpenCityScreen(),
                    new Rectangle(0, 0, 559, 60),
                    game.Content.Load<Texture2D>("startgame"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    Color.White,
                    Shape.AMORPHOUS,
                    SpriteBatch,
                    Game);

            startGameButton.HoverPosition = new Shift50pxRight();
            startGameButton.UnhoverPosition = new Shift50pxLeft();

            /**
            MSAnimatingButton optionsButton = new MSAnimatingButton(
                    null,
                    new OpenOptionsScreen(),
                    new Rectangle(0, 0, 472, 60),
                    game.Content.Load<Texture2D>("options"),
                    game.Content.Load<Texture2D>("optionsclicked"),
                    game.Content.Load<Texture2D>("optionsclicked"),
                    Color.White,
                    Shape.AMORPHOUS,
                    SpriteBatch,
                    Game);
             */

            MSAnimatingButton exitButton = new MSAnimatingButton(
                    null,
                    new Exit(),
                    new Rectangle(0, 0, 574, 60),
                    game.Content.Load<Texture2D>("exit"), 
                    game.Content.Load<Texture2D>("exitclicked"), 
                    game.Content.Load<Texture2D>("exitclicked"), 
                    Color.White, 
                    Shape.AMORPHOUS,
                    SpriteBatch, 
                    Game);

            AddComponent(startGameButton, Alignment.TOP_CENTER);

            //AddComponent(optionsButton, Alignment.MIDDLE_CENTER);

            AddComponent(exitButton, Alignment.BOTTOM_CENTER);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleMouseInput((Game as MoodSwing).OldMouseState);
            HandleKeyboardInput((Game as MoodSwing).OldKeyboardState);
        }
    }
}
