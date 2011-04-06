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
    /// MS2DComponent is a two-dimensional DrawableGameComponent with a bounding Rectangle
    /// </summary>
    public abstract class MS2DComponent : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        /// <summary>
        /// Gets the SpriteBatch that will draw this MS2DComponent
        /// </summary>
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        protected Rectangle boundingRectangle;
        /// <summary>
        /// Gets or Sets the bounding Rectangle of this MS2DComponent
        /// </summary>
        public virtual Rectangle BoundingRectangle
        {
            set { boundingRectangle = value; }
            get { return boundingRectangle; }
        }

        /// <summary>
        /// Gets or Sets the position of this MS2DComponent, in screen coordinates
        /// </summary>
        public virtual Vector2 Position 
        {
            set 
            { 
                BoundingRectangle = new Rectangle(
                    (int)Math.Round(value.X),
                    (int)Math.Round(value.Y),
                    boundingRectangle.Width,
                    boundingRectangle.Height);
            }
            get { return new Vector2(boundingRectangle.X, boundingRectangle.Y); }
        }

        /// <summary>
        /// Gets or Sets the size of this MS2DComponent, in pixels
        /// </summary>
        public virtual Vector2 Size 
        {
            set 
            { 
                BoundingRectangle = new Rectangle(
                    boundingRectangle.X,
                    boundingRectangle.Y,
                    (int)Math.Round(value.X),
                    (int)Math.Round(value.Y));
            }
            get { return new Vector2(boundingRectangle.Width, boundingRectangle.Height); }
        }

        public MS2DComponent(Rectangle boundingRectangle, SpriteBatch spriteBatch, Game game)
            : base(game)
        {
            BoundingRectangle = boundingRectangle;
            this.spriteBatch = spriteBatch;
        }

        public MS2DComponent(Vector2 position, Vector2 size, SpriteBatch spriteBatch, Game game)
            : base(game)
        {
            Position = position;
            Size = size;
            this.spriteBatch = spriteBatch;
        }
    }
}
