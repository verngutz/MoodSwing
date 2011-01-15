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
    public class MSOptionsScreen : MSScreen
    {
        private static MSOptionsScreen optionsScreen;
        public static MSOptionsScreen getInstance(MoodSwing game)
        {
            if (optionsScreen == null)
                optionsScreen = new MSOptionsScreen(game);
            return optionsScreen;
        }

        public MSScreen ReturnScreen { set; get; }

        private MSOptionsScreen(MoodSwing game)
            : base(game.Content.Load<Texture2D>("Options"), 150, 150, 150, 150, game.SpriteBatch, game) 
        {
            AddElement(
                new MSButton(
                    new MSLabel("Return", new Vector2(20, 10), new Vector2(60, 30), game.Content.Load<SpriteFont>("Temp"), Color.Black, SpriteBatch, game),
                    new ReturnFromOptionsScreen(),
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
