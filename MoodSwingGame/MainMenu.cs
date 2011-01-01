using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MoodSwingGUI;
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

        private MainMenu(Texture2D bg, Game1 g, SpriteBatch sb)
            : base(bg, Color.White, sb, g)
        {
            
        }

        public static void INIT(Texture2D t2D, Game1 game, SpriteBatch sb)
        {
            mainMenu = new MainMenu(t2D, game, sb);
        }

        public void add( Texture2D unclicked, Texture2D clicked, Texture2D hovered ) {
            AddElement( new MSButton(null, new Exit(), new Vector2(50,50), new Vector2( 50, 50), unclicked,
                clicked, hovered, SpriteBatch, Color.White, Shape.RECTANGULAR, Game ), Alignment.TOP_LEFT );
        }

        public override void sentinel(KeyboardState oldKeyState, MouseState oldMouseState)
        {
            base.sentinel(oldKeyState, oldMouseState);
        }
    }
}
