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
    public enum Shape { RECTANGULAR, CIRCULAR }
    public abstract class MS2DClickable : MS2DComponent
    {
        private Shape shape;
        public Shape Shape { get { return shape; } }
      
        public MS2DClickable(Vector2 position, Vector2 size, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game)
        {
            this.shape = shape;
        }

        public bool CollidesWithMouse(MouseState currentMouseState)
        {
            switch (Shape)
            {
                case Shape.RECTANGULAR:
                    return !(currentMouseState.X < Position.X || currentMouseState.X > Position.X + Size.X || currentMouseState.Y < Position.Y || currentMouseState.Y > Position.Y + Size.Y);
                case Shape.CIRCULAR:
                    return (Vector2.Distance(Position + Size / 2, new Vector2(currentMouseState.X, currentMouseState.Y)) <= Size.X / 2);
            }
            return false;
        }

        public abstract bool CheckMouseClick(MouseState oldMouseState);
    }
}
