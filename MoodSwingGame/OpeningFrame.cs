using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MoodSwingGame
{
    public class OpeningFrame : MoodSwingScreen
    {
        private static OpeningFrame openingFrame = null;

        public static OpeningFrame OPENING_FRAME
        {
            get {
                return openingFrame; 
            }
        }

        private OpeningFrame(Texture2D bg, Game1 g, SpriteBatch sb )
            : base(bg, Color.White, sb, g )
        {

        }

        public override MoodSwingScreen next()
        {
            return MainMenu.MAIN_MENU;
        }

        public static void INIT(Texture2D t2D, Game1 game, SpriteBatch sb)
        {
            openingFrame = new OpeningFrame(t2D, game, sb);
        }
    }
}
