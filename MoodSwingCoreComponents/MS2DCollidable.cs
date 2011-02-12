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

        protected Texture2D collisionTexture;

        /// <summary>
        /// Gets the Texture that will be used for AMORPHOUS collision detection
        /// </summary>
        public Texture2D CollisionTexture { get { return collisionTexture; } }

        public MS2DCollidable(Rectangle boundingRectangle, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, spriteBatch, game)
        {
            this.shape = shape;
        }

        public MS2DCollidable(Vector2 position, Vector2 size, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game)
        {
            this.shape = shape;
        }

        /// <summary>
        /// Check if this MS2DCollidable collides with another MS2DCollidable.
        /// </summary>
        /// <param name="other">the other MS2DCollidable</param>
        /// <returns>true if this collides with other, false otherwise</returns>
        public bool CollidesWith(MS2DCollidable other)
        {
            if (BoundingRectangle.Intersects(other.BoundingRectangle))
            {
                if (Shape == Shape.RECTANGULAR && other.Shape == Shape.RECTANGULAR)
                    return true;
                else if(Shape == Shape.CIRCULAR && other.Shape == Shape.CIRCULAR)
                    return Vector2.Distance(Position + Size / 2, other.Position + other.Size / 2) <= Size.X / 2 + other.Size.X / 2;
                else
                {
                    RenderTarget2D renderTarget = new RenderTarget2D(Game.GraphicsDevice, other.BoundingRectangle.Width, other.BoundingRectangle.Height, 1, SurfaceFormat.Color);
                    Game.GraphicsDevice.SetRenderTarget(0, renderTarget);
                    Game.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);
                    spriteBatch.Begin();
                    spriteBatch.Draw(CollisionTexture,
                        new Rectangle(0, 0, other.BoundingRectangle.Width, other.BoundingRectangle.Height),
                        new Rectangle(
                            other.BoundingRectangle.X - BoundingRectangle.X, 
                            other.BoundingRectangle.Y - BoundingRectangle.Y, 
                            other.BoundingRectangle.Width, 
                            other.BoundingRectangle.Height),
                        Color.White);
                    spriteBatch.End();
                    Game.GraphicsDevice.SetRenderTarget(0, null);
                    int numPixels = other.BoundingRectangle.Width * other.BoundingRectangle.Height;
                    Color[] collisionColorInformation = new Color[numPixels];
                    renderTarget.GetTexture().GetData<Color>(0,
                        new Rectangle(0, 0, other.BoundingRectangle.Width, other.BoundingRectangle.Height),
                        collisionColorInformation, 0, numPixels);

                    foreach (Color c in collisionColorInformation)
                        if (c.A != 0)
                            return true;

                    return false;
                }
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
            if (BoundingRectangle.Contains(p))
            {
                switch (Shape)
                {
                    case Shape.RECTANGULAR:
                        return true;
                    case Shape.CIRCULAR:
                        return Vector2.DistanceSquared(Position + Size / 2, new Vector2(p.X, p.Y)) <= Size.X * Size.X;
                    case Shape.AMORPHOUS:
                        RenderTarget2D renderTarget = new RenderTarget2D(Game.GraphicsDevice, 1, 1, 1, SurfaceFormat.Color);
                        Game.GraphicsDevice.SetRenderTarget(0, renderTarget);
                        Game.GraphicsDevice.Clear(ClearOptions.Target, Color.TransparentWhite, 0, 0);
                        spriteBatch.Begin();
                        spriteBatch.Draw(CollisionTexture,
                            new Rectangle(0, 0, 1, 1),
                            new Rectangle(p.X - BoundingRectangle.X, p.Y - BoundingRectangle.Y, 1, 1),
                            Color.White);
                        spriteBatch.End();
                        Game.GraphicsDevice.SetRenderTarget(0, null);
                        Color[] collisionColorInformation = new Color[1];
                        renderTarget.GetTexture().GetData<Color>(0, new Rectangle(0, 0, 1, 1), collisionColorInformation, 0, 1);
                        System.Console.WriteLine(collisionColorInformation[0].A);
                        if (collisionColorInformation[0].A != 0)                           
                            return true;

                        else return false;
                }
            }
            return false;
        }
    }
}
