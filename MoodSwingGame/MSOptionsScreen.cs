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
        public static MSOptionsScreen getInstance()
        {
            if (optionsScreen == null)
                optionsScreen = new MSOptionsScreen(MoodSwing.getInstance());
            return optionsScreen;
        }

        public MSScreen ReturnScreen { set; get; }

        private MSOptionsScreen(MoodSwing game)
            : base(game.Content.Load<Texture2D>("Options"), 150, 150, 150, 150, game.SpriteBatch, game) 
        {
            AddComponent(new MSButton(
                    null,
                    new ReturnFromOptionsScreen(),
                    new Rectangle(0, 0, 574, 60),
                    game.Content.Load<Texture2D>("exit"),
                    game.Content.Load<Texture2D>("exitclicked"),
                    game.Content.Load<Texture2D>("exit"),   
                    Color.White,
                    Shape.RECTANGULAR,
                    SpriteBatch,
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
