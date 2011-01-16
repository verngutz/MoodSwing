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
    public class MSTextField : MS2DComponent
    {
        private String storedText;
        private String drawnText;
        public String Text 
        {
            get { return storedText; }
            set
            {
                storedText = value;
                char[] splitted = value.ToCharArray();
                for (int i = 0; i < splitted.Length; i++)
                {
                    drawnText += splitted[i];
                    lineWidth += spriteFont.MeasureString(splitted[i].ToString()).X;
                    if (lineWidth > Size.X)
                        drawnText = drawnText.Substring(1);
                }
            }
        }
        private SpriteFont spriteFont;
        private Color color;
        private float lineWidth;

         public MSTextField(String text, int x, int y, int width, int height, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, x, y, width, height, spriteFont, Color.Black, spriteBatch, game) { }

        public MSTextField(String text, int x, int y, int width, int height, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : this(text, new Vector2(x, y), new Vector2(width, height), spriteFont, color, spriteBatch, game) { }

        public MSTextField(String text, Rectangle boundingRectangle, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, boundingRectangle, spriteFont, Color.Black, spriteBatch, game) { }

        public MSTextField(String text, Rectangle boundingRectangle, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : this(text, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), spriteFont, color, spriteBatch, game) { }

        public MSTextField(String text, Vector2 position, Vector2 size, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, position, size, spriteFont, Color.Black, spriteBatch, game) { }

        public MSTextField(String text, Vector2 position, Vector2 size, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game)
        {
            this.spriteFont = spriteFont;
            Text = text;
            this.color = color;
            lineWidth = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.DrawString(spriteFont, drawnText, Position, color);
            base.Draw(gameTime);
        }
    }
}
