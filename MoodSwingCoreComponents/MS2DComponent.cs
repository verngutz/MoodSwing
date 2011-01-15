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

namespace MoodSwingCoreComponents
{
    /// <summary>
    /// MS2DComponent is a two-dimensional DrawableGameComponent with an (x, y) position, an (x, y) size.
    /// </summary>
    public abstract class MS2DComponent : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        /// <summary>
        /// Gets and sets the position of this MS2DComponent, in screen coordinates
        /// </summary>
        /// <value>X corresponds to the x-position, Y corresponds to the y-position</value>
        public virtual Vector2 Position { set; get; }

        /// <summary>
        /// Gets and sets the size of this MS2DComponent, in pixels
        /// </summary>
        /// <value>X corresponds to the width, Y corresponds to the height</value>
        public virtual Vector2 Size { set; get; }
      
        public MS2DComponent(Vector2 position, Vector2 size, SpriteBatch spriteBatch, Game game)
            : base(game)
        {
            Position = position;
            Size = size;
            this.spriteBatch = spriteBatch;
        }
    }
}
