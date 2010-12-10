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
    public class MSPanel : MS2DComponent
    {
        private Texture2D background;
        private List<MS2DComponent> elements;
        private Vector2 boundedPosition;
        private Vector2 boundedSize;
        private Color highlight;
        private Vector2 scale;

        public MSPanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, spriteBatch, game)
        {
            this.background = background;
            elements = new List<MS2DComponent>();

            boundedPosition = Position + new Vector2(leftPadding, topPadding);
            boundedSize = Size - new Vector2(leftPadding, topPadding) - new Vector2(rightPadding, bottomPadding);
            scale = Size / new Vector2(background.Width, background.Height);
            this.highlight = highlight;
        }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : this(background, position, size, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game)
        {
            this.background = background;
            elements = new List<MS2DComponent>();

            boundedPosition = Position + new Vector2(leftPadding, topPadding);
            boundedSize = Size - new Vector2(leftPadding, topPadding) - new Vector2(rightPadding, bottomPadding);
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
                    element.Position = boundedPosition + new Vector2((boundedSize.X - element.Size.X) / 2 , 0);
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
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(background, Position, null, highlight, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

            foreach(MS2DComponent element in elements)
            {
                element.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
