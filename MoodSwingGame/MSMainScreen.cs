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
            : base(game.Content.Load<Texture2D>("gamescreenbg"), 150, 150, 150, 150, Color.White, game.SpriteBatch, game)
        {
            new MSButton(
                    null,
                    new OpenCityScreen(),
                    new Rectangle(0, 0, 559, 60),
                    game.Content.Load<Texture2D>("startgame"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    SpriteBatch,
                    Color.White,
                    Shape.AMORPHOUS,
                    Game);
            AddComponent(new MSButton(
                    null,
                    new OpenCityScreen(),
                    new Rectangle(0, 0, 559, 60),
                    game.Content.Load<Texture2D>("startgame"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    SpriteBatch,
                    Color.White,
                    Shape.AMORPHOUS,
                    Game)
                    , Alignment.TOP_CENTER);

            AddComponent(new MSButton(
                    null,
                    new OpenOptionsScreen(),
                    new Rectangle(0, 0, 472, 60),
                    game.Content.Load<Texture2D>("options"),
                    game.Content.Load<Texture2D>("optionsclicked"),
                    game.Content.Load<Texture2D>("optionsclicked"),
                    SpriteBatch,
                    Color.White,
                    Shape.AMORPHOUS,
                    Game)
                    , Alignment.MIDDLE_CENTER);

            AddComponent(new MSButton(
                    null,
                    new Exit(),
                    new Rectangle(0, 0, 574, 60),
                    game.Content.Load<Texture2D>("exit"), 
                    game.Content.Load<Texture2D>("exitclicked"), 
                    game.Content.Load<Texture2D>("exitclicked"), 
                    SpriteBatch, 
                    Color.White, 
                    Shape.AMORPHOUS, 
                    Game)
                    , Alignment.BOTTOM_CENTER);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleMouseInput((Game as MoodSwing).OldMouseState);
            HandleKeyboardInput((Game as MoodSwing).OldKeyboardState);
        }
    }
}
