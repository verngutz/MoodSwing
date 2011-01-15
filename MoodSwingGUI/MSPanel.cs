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
using MoodSwingCoreComponents;

namespace MoodSwingGUI
{
    public class MSPanel : MS2DClickable
    {
        private Texture2D background;
        private List<MS2DComponent> elements;
        public List<MS2DComponent> Elements
        {
            get { return elements; }
        }
        private List<MS2DClickable> clickableElements;
        public List<MS2DClickable> ClickableElements
        {
            get { return clickableElements; }
        }

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                boundedPosition -= base.Position;
                boundedPosition += value;
                base.Position = value;
            }
        }

        private Vector2 boundedPosition;
        private Vector2 boundedSize;
        private Color highlight;
        private Vector2 scale;

        public MSPanel(Texture2D background, float x, float y, float width, float height, SpriteBatch spriteBatch, Game game)
            : this(background, x, y, width, height, 0, 0, 0, 0, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, float x, float y, float width, float height, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, new Vector2(x, y), new Vector2(width, height), 0, 0, 0, 0, highlight, spriteBatch, game) { }

        public MSPanel(Texture2D background, float x, float y, float width, float height, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : this(background, x, y, width, height, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, float x, float y, float width, float height, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, new Vector2(x, y), new Vector2(width, height), topPadding, bottomPadding, leftPadding, rightPadding, highlight, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), 0, 0, 0, 0, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), topPadding, bottomPadding, leftPadding, rightPadding, spriteBatch, game) { }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, SpriteBatch spriteBatch, Game game)
            : this(background, position, size, 0, 0, 0, 0, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, position, size, 0, 0, 0, 0, highlight, spriteBatch, game) { }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : this(background, position, size, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : base(position, size, Shape.RECTANGULAR, spriteBatch, game)
        {
            this.background = background;
            elements = new List<MS2DComponent>();
            clickableElements = new List<MS2DClickable>();

            boundedPosition = Position + new Vector2(leftPadding, topPadding);
            boundedSize = Size - new Vector2(leftPadding, topPadding) - new Vector2(rightPadding, bottomPadding);

            if(background == null)
                scale = Vector2.Zero;
            else
                scale = Size / new Vector2(background.Width, background.Height);

            this.highlight = highlight;
        }

        public void AddElement(MS2DComponent element, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TOP_LEFT:
                    element.Position = boundedPosition;
                    break;
                case Alignment.TOP_CENTER:
                    element.Position = boundedPosition + new Vector2((boundedSize.X - element.Size.X) / 2, 0);

                    break;
                case Alignment.TOP_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, 0);
                    break;
                case Alignment.MIDDLE_LEFT:
                    element.Position = boundedPosition + new Vector2(0, (boundedSize.Y - element.Size.Y) / 2);
                    break;
                case Alignment.MIDDLE_CENTER:
                    element.Position = boundedPosition + (boundedSize - element.Size) / 2;
                    break;
                case Alignment.MIDDLE_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, (boundedSize.Y - element.Size.Y) / 2);
                    break;
                case Alignment.BOTTOM_LEFT:
                    element.Position = boundedPosition + new Vector2(0, boundedSize.Y - element.Size.Y);
                    break;
                case Alignment.BOTTOM_CENTER:
                    element.Position = boundedPosition + new Vector2((boundedSize.X - element.Size.X) / 2, boundedSize.Y - element.Size.Y);
                    break;
                case Alignment.BOTTOM_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, boundedSize.Y - element.Size.Y);
                    break;
            }
            elements.Add(element);
            if (element is MS2DClickable)
            {
                clickableElements.Add(element as MS2DClickable);
            }
        }

        public override bool CheckMouseClick(MouseState oldMouseState)
        {
            foreach (MS2DClickable element in clickableElements)
            {
                if (element.CheckMouseClick(oldMouseState))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Draw(GameTime gameTime)
        {
            if (background != null)
            {
                SpriteBatch.Draw(background, Position, null, highlight, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            }
            foreach(MS2DComponent element in elements)
            {
                element.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
