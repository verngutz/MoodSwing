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
using MoodSwingCoreComponents;

namespace MoodSwingGUI
{
    public class MSWrappingLabel : MSLabel
    {
        /// <summary>
        /// Gets or sets the text stored in this MSLabel
        /// </summary>
        public override String Text
        {
            set
            {
                text = value;
                if (rightBoundary == null)
                {
                    displayedText = value;
                }
                else
                {
                    displayedText = "";
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (boundedPosition.X + spriteFont.MeasureString(displayedText).X + spriteFont.MeasureString(value.ElementAt<Char>(i).ToString()).X > rightBoundary)
                        {
                            if (bottomBoundary != null && boundedPosition.Y + spriteFont.MeasureString(displayedText).Y + spriteFont.MeasureString(value.ElementAt<Char>(i).ToString()).Y > bottomBoundary)
                            {
                                break;
                            }
                            displayedText += "\n";
                        }
                        displayedText += value.ElementAt<Char>(i);
                    }
                }
                BoundingRectangle = new Rectangle
                (
                    BoundingRectangle.X,
                    BoundingRectangle.Y,
                    (int)spriteFont.MeasureString(displayedText).X + leftPadding + rightPadding,
                    (int)spriteFont.MeasureString(displayedText).Y + topPadding + bottomPadding
                );
            }
            get { return text; }
        }

        private String displayedText;

        private int? rightBoundary;
        private int? bottomBoundary;

        private Texture2D background;

        private int topPadding;
        private int bottomPadding;
        private int leftPadding;
        private int rightPadding;

        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                boundedPosition -= base.Position;
                boundedPosition += value;
                base.Position = value;
            }
        }

        private Vector2 boundedPosition;
        private Vector2 boundedSize;

        /// <summary>
        /// Constructs a new MSLabel.
        /// </summary>
        /// <param name="text">the text that this MSLabel will display</param>
        /// <param name="boundingRectangle">the bounding Rectangle of this MSLabel</param>
        /// <param name="spriteFont">the SpriteFont used to draw the text</param>
        /// <param name="color">the text color of this MSLabel</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSLabel</param>
        /// <param name="game">the Game where this MSLabel is used</param>
        public MSWrappingLabel(Point position, String text, SpriteFont sprite_font, Color text_color, Texture2D background, int? rightBoundary, int? bottomBoundary, SpriteBatch spriteBatch, Game game)
            : this(position, text, sprite_font, text_color, background, 0, 0, 0, 0, rightBoundary, bottomBoundary, spriteBatch, game) { }

        /// <summary>
        /// Constructs a new MSLabel.
        /// </summary>
        /// <param name="text">the text that this MSLabel will display</param>
        /// <param name="boundingRectangle">the bounding Rectangle of this MSLabel</param>
        /// <param name="spriteFont">the SpriteFont used to draw the text</param>
        /// <param name="color">the text color of this MSLabel</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSLabel</param>
        /// <param name="game">the Game where this MSLabel is used</param>
        public MSWrappingLabel(Point position, String text, SpriteFont sprite_font, Color text_color, Texture2D background, int topPadding, int bottomPadding, int leftPadding, int rightPadding, int? rightBoundary, int? bottomBoundary, SpriteBatch spriteBatch, Game game)
            : base(text, new Rectangle(position.X, position.Y, 0, 0), sprite_font, text_color, spriteBatch, game)
        {
            this.spriteFont = sprite_font;
            this.textColor = text_color;
            this.background = background;

            this.topPadding = topPadding;
            this.bottomPadding = bottomPadding;
            this.leftPadding = leftPadding;
            this.rightPadding = rightPadding;

            this.rightBoundary = rightBoundary;
            this.bottomBoundary = bottomBoundary;

            boundedPosition = Position + new Vector2(leftPadding, topPadding);
            boundedSize = Size - new Vector2(leftPadding, topPadding) - new Vector2(rightPadding, bottomPadding);
            Text = text;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (background != null)
                SpriteBatch.Draw(background, BoundingRectangle, Color.White);
            SpriteBatch.DrawString(spriteFont, displayedText, boundedPosition, textColor);
        }
    }
}
