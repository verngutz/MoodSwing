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
    public class MSLabel : MSGUIComponent
    {
        private String text;
        public String Text
        {
            set
            {
                fontScale = Size / spriteFont.MeasureString(value);
                text = value;
            }
            get { return text; }
        }

        private SpriteFont spriteFont;
        private Color color;
        private Vector2 fontScale;

        public MSLabel(String text, int x, int y, int width, int height, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, x, y, width, height, spriteFont, Color.Black, spriteBatch, game) { }

        public MSLabel(String text, int x, int y, int width, int height, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : this(text, new Vector2(x, y), new Vector2(width, height), spriteFont, color, spriteBatch, game) { }

        public MSLabel(String text, Rectangle boundingRectangle, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, boundingRectangle, spriteFont, Color.Black, spriteBatch, game) { }

        public MSLabel(String text, Rectangle boundingRectangle, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : this(text, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), spriteFont, color, spriteBatch, game) { }

        public MSLabel(String text, Vector2 position, Vector2 size, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, position, size, spriteFont, Color.Black, spriteBatch, game) { }

        public MSLabel(String text, Vector2 position, Vector2 size, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game)
        {
            this.spriteFont = spriteFont;
            Text = text;
            this.color = color;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.DrawString(spriteFont, Text, Position, color, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
            base.Draw(gameTime);
        }
    }
}
