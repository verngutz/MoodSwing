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
        private float topPadding;
        private float bottomPadding;
        private float leftPadding;
        private float rightPadding;
        private Color highlight;

        public MSPanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, spriteBatch, game)
        {
            this.background = background;
            elements = new List<MS2DComponent>();

            this.topPadding = topPadding;
            this.bottomPadding = bottomPadding;
            this.leftPadding = leftPadding;
            this.rightPadding = rightPadding;

            this.highlight = Color.White;
        }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, spriteBatch, game)
        {
            this.background = background;
            elements = new List<MS2DComponent>();

            this.topPadding = topPadding;
            this.bottomPadding = bottomPadding;
            this.leftPadding = leftPadding;
            this.rightPadding = rightPadding;

            this.highlight = highlight;
        }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game)
        {
            this.background = background;
            elements = new List<MS2DComponent>();

            this.topPadding = topPadding;
            this.bottomPadding = bottomPadding;
            this.leftPadding = leftPadding;
            this.rightPadding = rightPadding;

            this.highlight = Color.White;
        }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game)
        {
            this.background = background;
            elements = new List<MS2DComponent>();

            this.topPadding = topPadding;
            this.bottomPadding = bottomPadding;
            this.leftPadding = leftPadding;
            this.rightPadding = rightPadding;

            this.highlight = highlight;
        }

        public void AddElement(MS2DComponent element, Alignment alignment, bool resize)
        {
            switch (alignment)
            {
                case Alignment.TOP_LEFT:
                    element.Position = Position + new Vector2(leftPadding, topPadding);
                    if(resize)
                        element.Size = Position - new Vector2(rightPadding, bottomPadding) - element.Position;
                    break;
                case Alignment.TOP_CENTER:
                    element.Position = Position + new Vector2(Size.X / 2, topPadding);
                    if(resize)
                        element.Size = Position - new Vector2(rightPadding, bottomPadding) - element.Position;
                        break;
                default:
                    if(resize)
                        element.Size = ;
                    break;
            }
            elements.Add(element);
        }
    }
}
