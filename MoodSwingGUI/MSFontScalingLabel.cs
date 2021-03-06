﻿using System;
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
    /// <summary>
    /// MSLabel is an MSGUIUnclickable component that displays text with the use of a SpriteFont. The text is always scaled to fit the bounding Rectangle of this MSLabel.
    /// </summary>
    public class MSFontScalingLabel : MSLabel
    {
        /// <summary>
        /// Gets or sets the text stored in this MSLabel
        /// </summary>
        public override String Text
        {
            set
            {
                fontScale = Size / spriteFont.MeasureString(value);
                text = value;
            }
            get { return text; }
        }

        private Vector2 fontScale;

        /// <summary>
        /// Constructs a new MSLabel with default Color.Black text.
        /// </summary>
        /// <param name="text">the text that this MSLabel will display</param>
        /// <param name="boundingRectangle">the bounding Rectangle of this MSLabel</param>
        /// <param name="spriteFont">the SpriteFont used to draw the text</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSLabel</param>
        /// <param name="game">the Game where this MSLabel is used</param>
        public MSFontScalingLabel(String text, Rectangle boundingRectangle, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, boundingRectangle, spriteFont, Color.Black, spriteBatch, game) { }

        /// <summary>
        /// Constructs a new MSLabel.
        /// </summary>
        /// <param name="text">the text that this MSLabel will display</param>
        /// <param name="boundingRectangle">the bounding Rectangle of this MSLabel</param>
        /// <param name="spriteFont">the SpriteFont used to draw the text</param>
        /// <param name="color">the text color of this MSLabel</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSLabel</param>
        /// <param name="game">the Game where this MSLabel is used</param>
        public MSFontScalingLabel(String text, Rectangle boundingRectangle, SpriteFont spriteFont, Color text_color, SpriteBatch spriteBatch, Game game)
            : base(text, boundingRectangle, spriteFont, text_color, spriteBatch, game)
        {
            this.spriteFont = spriteFont;
            Text = text;
            TextColor = text_color;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch.DrawString(spriteFont, Text, Position, TextColor, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
        }
    }
}
