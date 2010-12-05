using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MoodSwingGame
{
    public class MoodSwingScreen : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D texture;

        public MoodSwingScreen ( Texture2D t2D, Game g, SpriteBatch sb ) : base ( g )  {
            texture = t2D;
            spriteBatch = sb;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, new Rectangle(0, 0, 500, 500), Color.White);
            base.Draw(gameTime);
        }

        public void draw(GameTime gt)
        {
            Draw(gt);
        }
        public virtual MoodSwingScreen next()
        {
            return this;
        }

    }
}
