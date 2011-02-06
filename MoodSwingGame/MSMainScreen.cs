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
            MSPanel playGamePanel = new MSPanel(null, Vector2.Zero, new Vector2(100, 200), 0, 0, 0, 0, SpriteBatch, Game);
            AddElement(playGamePanel, Alignment.MIDDLE_CENTER);

            playGamePanel.AddElement(
                new MSButton(
                    null,
                    new OpenCityScreen(),
                    Vector2.Zero,
                    new Vector2(559, 60),
                    game.Content.Load<Texture2D>("startgame"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    SpriteBatch,
                    Color.White,
                    Shape.AMORPHOUS,
                    Game)
                    , Alignment.MIDDLE_CENTER);

            MSPanel miscPanel = new MSPanel(null, Vector2.Zero, new Vector2(100, 200), 0, 0, 0, 0, SpriteBatch, Game);
            AddElement(miscPanel, Alignment.BOTTOM_CENTER);

            miscPanel.AddElement(
                new MSButton(
                    null,
                    new OpenOptionsScreen(),
                    Vector2.Zero,
                    new Vector2(472, 60),
                    game.Content.Load<Texture2D>("options"),
                    game.Content.Load<Texture2D>("optionsclicked"),
                    game.Content.Load<Texture2D>("optionsclicked"),
                    SpriteBatch,
                    Color.White,
                    Shape.AMORPHOUS,
                    Game)
                    , Alignment.TOP_CENTER);

            

            miscPanel.AddElement(
                new MSButton(
                    null,
                    new Exit(), 
                    Vector2.Zero,
                    new Vector2(574, 60), 
                    game.Content.Load<Texture2D>("exit"), 
                    game.Content.Load<Texture2D>("exitclicked"), 
                    game.Content.Load<Texture2D>("exitclicked"), 
                    SpriteBatch, 
                    Color.White, 
                    Shape.AMORPHOUS, 
                    Game)
                    , Alignment.BOTTOM_CENTER);

            MSPanel coverPanel = new MSPanel(game.Content.Load<Texture2D>("CityView"), new Rectangle(50, 50, 500, 500), spriteBatch, game);
            AddElement(coverPanel, Alignment.MANUAL);
            MSTextField test = new MSTextField("", Vector2.Zero, new Vector2(250, 35), null, null, Game.Content.Load<SpriteFont>("Temp"), SpriteBatch, Game);
            AddElement(test, Alignment.MIDDLE_LEFT);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            CheckMouseClick((Game as MoodSwing).OldMouseState);
            CheckKeyboardInput((Game as MoodSwing).OldKeyboardState);
        }
    }
}
