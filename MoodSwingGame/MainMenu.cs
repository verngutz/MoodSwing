using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MoodSwingGame
{
    public class MainMenu : MoodSwingScreen
    {
        private static MainMenu mainMenu = null;
        public static MainMenu MAIN_MENU
        {
            get {
                return mainMenu; 
            }
        }

        private MainMenu(Texture2D texture, Game1 g, SpriteBatch sb)
            : base(texture, g, sb)
        {

        }

        public static void INIT(Texture2D t2D, Game1 game, SpriteBatch sb)
        {
            mainMenu = new MainMenu(t2D, game, sb);
        }
    }
}
