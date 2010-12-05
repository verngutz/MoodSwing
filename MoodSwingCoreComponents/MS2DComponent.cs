using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace MoodSwingGUI
{
    public abstract class MS2DComponent : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;

        public Vector2 Position { set; get; }

        public Vector2 Size { set; get; }
      
        public MS2DComponent(Vector2 position, Vector2 size, SpriteBatch spriteBatch, Game game)
            : base(game)
        {
            Position = position;
            Size = size;
            this.spriteBatch = spriteBatch;
        }
        public MS2DComponent(Rectangle boundingRectangle, SpriteBatch spriteBatch, Game game)
            : base(game)
        {
            Position = new Vector2(boundingRectangle.X, boundingRectangle.Y);
            Size = new Vector2(boundingRectangle.Width, boundingRectangle.Height);
            this.spriteBatch = spriteBatch;
        }
    }
}
