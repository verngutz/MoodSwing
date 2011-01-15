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
            : base(game.Content.Load<Texture2D>("MainMenu"), 150, 150, 150, 150, Color.White, game.SpriteBatch, game)
        {
            MSPanel playGamePanel = new MSPanel(null, Vector2.Zero, new Vector2(100, 200), 0, 0, 0, 0, SpriteBatch, Game);
            AddElement(playGamePanel, Alignment.MIDDLE_CENTER);

            playGamePanel.AddElement(
                new MSButton(
                    new MSLabel("City", new Vector2(20, 10), new Vector2(60, 30), game.Content.Load<SpriteFont>("Temp"), Color.Black, SpriteBatch, game),
                    new OpenCityScreen(),
                    Vector2.Zero,
                    new Vector2(100, 50),
                    game.Content.Load<Texture2D>("Button"),
                    game.Content.Load<Texture2D>("ButtonClicked"),
                    game.Content.Load<Texture2D>("ButtonHover"),
                    SpriteBatch,
                    Color.White,
                    Shape.RECTANGULAR,
                    Game)
                    , Alignment.MIDDLE_CENTER);

            MSPanel miscPanel = new MSPanel(null, Vector2.Zero, new Vector2(100, 200), 0, 0, 0, 0, SpriteBatch, Game);
            AddElement(miscPanel, Alignment.BOTTOM_CENTER);

            miscPanel.AddElement(
                new MSButton(
                    new MSLabel("Options", new Vector2(20, 10), new Vector2(60, 30), game.Content.Load<SpriteFont>("Temp"), Color.Black, SpriteBatch, game),
                    new OpenOptionsScreen(),
                    Vector2.Zero,
                    new Vector2(100, 50),
                    game.Content.Load<Texture2D>("Button"),
                    game.Content.Load<Texture2D>("ButtonClicked"),
                    game.Content.Load<Texture2D>("ButtonHover"),
                    SpriteBatch,
                    Color.White,
                    Shape.RECTANGULAR,
                    Game)
                    , Alignment.TOP_CENTER);

            miscPanel.AddElement(
                new MSButton(
                    new MSLabel("Exit", new Vector2(20, 10), new Vector2(60, 30), game.Content.Load<SpriteFont>("Temp"), Color.Black, SpriteBatch, game), 
                    new Exit(), 
                    Vector2.Zero,
                    new Vector2(100, 50), 
                    game.Content.Load<Texture2D>("Button"), 
                    game.Content.Load<Texture2D>("ButtonClicked"), 
                    game.Content.Load<Texture2D>("ButtonHover"), 
                    SpriteBatch, 
                    Color.White, 
                    Shape.RECTANGULAR, 
                    Game)
                    , Alignment.BOTTOM_CENTER);

            
        }

        public override void Update(GameTime gameTime)
        {
            CheckMouseClick((Game as MoodSwing).OldMouseState);
            base.Update(gameTime);
        }
    }
}
