using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// An MS2DCollidable that is RECTANGULAR in Shape makes use of rectangular collision detection.
    /// An MS2DCollidable that is CIRCULAR in Shape makes use of circular collision detection.
    /// An MS2DCollidable that is AMORPHOUS in Shape makes use of pixel-by-pixel collision detection.
    /// </summary>
    public enum Shape { RECTANGULAR, CIRCULAR, AMORPHOUS }

    /// <summary>
    /// MS2DCollidable is an MS2DComponent that has a Shape that can be used for switch-casing collision detection.
    /// </summary>
    public abstract class MS2DCollidable : MS2DComponent
    {
        protected Shape shape;

        /// <summary>
        /// Gets the Shape of this MS2DCollidable. This is used for collision detection.
        /// <seealso cref="MoodSwingCoreComponents.Shape"/>
        /// </summary>
        public Shape Shape { get { return shape; } }

        public MS2DCollidable(Vector2 position, Vector2 size, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game)
        {
            this.shape = shape;
        }

        /// <summary>
        /// Check if this MS2DCollidable collides with another MS2DCollidable. Both must be of the same shape.
        /// </summary>
        /// <param name="other">the other MS2DCollidable</param>
        /// <returns>true if this collides with other, false otherwise</returns>
        public bool CollidesWith(MS2DCollidable other)
        {
            switch (Shape)
            {
                case Shape.RECTANGULAR:
                    return !(other.Position.X < Position.X || other.Position.X > Position.X + Size.X || other.Position.Y < Position.Y || other.Position.Y > Position.Y + Size.Y);
                case Shape.CIRCULAR:
                    return Vector2.Distance(Position + Size / 2, other.Position + other.Size / 2) <= Size.X;
                case Shape.AMORPHOUS:
                    throw new NotImplementedException();
            }
            return false;
        }

        /// <summary>
        /// Check whether Point p is within the bounds of this MS2DCollidable
        /// </summary>
        /// <param name="p">the Point to check</param>
        /// <returns>true if this encloses p, false otherwise</returns>
        public bool EnclosesPoint(Point p)
        {
            switch (Shape)
            {
                case Shape.RECTANGULAR:
                    return !(p.X < Position.X || p.X > Position.X + Size.X || p.Y < Position.Y || p.Y > Position.Y + Size.Y);
                case Shape.CIRCULAR:
                    return Vector2.Distance(Position + Size / 2, new Vector2(p.X, p.Y)) <= Size.X / 2;
                case Shape.AMORPHOUS:
                    throw new NotImplementedException();
            }
            return false;
        }
    }
}
