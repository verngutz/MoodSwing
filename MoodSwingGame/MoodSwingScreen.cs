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
    public class MoodSwingScreen : MSPanel
    {

        public MoodSwingScreen ( Texture2D bg, Color highlight, SpriteBatch sb, Game g ) :
            base(bg, new Vector2(0, 0), 
            new Vector2(g.GraphicsDevice.DisplayMode.Width, g.GraphicsDevice.DisplayMode.Height),
            0.0f, 0.0f, 0.0f, 0.0f, sb, g)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        
        public virtual MoodSwingScreen next()
        {
            return this;
        }
        public virtual void sentinel( KeyboardState oldKeyState, MouseState oldMouseState ) {
            foreach (MS2DComponent ms2dc in Elements)
            {
                if (ms2dc is MSButton)
                    (ms2dc as MSButton).chechMouseToButtonCollision(oldMouseState);
            }
        }


    }
}
