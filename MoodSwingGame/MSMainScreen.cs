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
        private static MSMainScreen mainMenu;
        public static MSMainScreen getInstance(MoodSwing game)
        {
            if (mainMenu == null)
                mainMenu = new MSMainScreen(game.Content.Load<Texture2D>("MainMenu"), game, game.SpriteBatch);
            return mainMenu;
        }

        private MSMainScreen(Texture2D background, MoodSwing game, SpriteBatch spriteBatch)
            : base(background, 150, 150, 150, 150, Color.White, spriteBatch, game)
        {
            AddElement(
                new MSButton(
                    null, 
                    new Exit(), 
                    Vector2.Zero,
                    new Vector2(100, 50), 
                    game.Content.Load<Texture2D>("Exit"), 
                    game.Content.Load<Texture2D>("ExitHover"), 
                    game.Content.Load<Texture2D>("ExitClicked"), 
                    SpriteBatch, 
                    Color.White, 
                    Shape.RECTANGULAR, 
                    Game)
                    , Alignment.BOTTOM_CENTER);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            CheckMouseClick((Game as MoodSwing).OldMouseState);
            base.Update(gameTime);
        }
    }
}
