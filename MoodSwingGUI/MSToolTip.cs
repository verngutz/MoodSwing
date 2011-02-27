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
    public class MSToolTip : MSGUIUnclickable
    {
        private Texture2D background;
        private string text;
        private Color textColor;
        private SpriteFont spriteFont;
        private Vector2 textPosition;

        public MSToolTip(Texture2D background, Point position, string text, Color textColor, SpriteFont spriteFont, int topPadding, int bottomPadding, int leftPadding, int rightPadding, SpriteBatch spriteBatch, Game game)
            : base(new Rectangle(position.X, position.Y, 
                leftPadding 
                + (int)Math.Round(spriteFont.MeasureString(text).X) 
                + rightPadding, 
                topPadding 
                + (int)Math.Round(spriteFont.MeasureString(text).Y) 
                + bottomPadding), 
            spriteBatch, game)
        {
            this.background = background;
            this.text = text;
            this.textColor = textColor;
            this.spriteFont = spriteFont;

            textPosition = new Vector2(position.X + leftPadding, position.Y + topPadding);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Draw(background, boundingRectangle, Color.White);
            spriteBatch.DrawString(spriteFont, text, textPosition, textColor);
        }
            
    }
}
