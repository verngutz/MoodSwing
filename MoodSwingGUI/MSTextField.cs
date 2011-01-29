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
    public class MSTextField : MSGUIFocusable
    {
        private String storedText;
        private String drawnText;
        public String Text 
        {
            get { return storedText; }
            set
            {
                storedText = value;

                lineWidth = 0;
                drawnSubstringStartIndex = 0;
                drawnSubstringLength = 0;
                for (int i = 0; i < value.Length; i++)
                {
                    lineWidth += spriteFont.MeasureString(value.Substring(i, 1)).X * fontScale;
                    if (lineWidth > Size.X)
                        drawnSubstringStartIndex += 1;
                    else
                        drawnSubstringLength += 1;
                }
                drawnText = value.Substring(drawnSubstringStartIndex, drawnSubstringLength);
                if (Editable)
                {
                    cursorIndex = value.Length - 1;
                    cursorPosition += new Vector2(spriteFont.MeasureString(drawnText).X * fontScale, 0);
                }   
            }
        }

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                cursorPosition += value;
            }
        }

        private float fontScale;

        private float lineWidth;
        private int drawnSubstringStartIndex;
        private int drawnSubstringLength;

        public bool Editable { set; get; }
        private int cursorIndex;
        private Vector2 cursorPosition;
        private int cursorBlinkCounter;
        private const int CURSOR_BLINK_PERIOD = 30;
        private const string CURSOR = "_";

        private SpriteFont spriteFont;
        private Color color;

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
            : base(position, size, Shape.RECTANGULAR, spriteBatch, game)
        {
            this.spriteFont = spriteFont;
            fontScale = Size.Y / spriteFont.MeasureString(CURSOR).Y;
            cursorIndex = 0;
            cursorBlinkCounter = 0;
            cursorPosition = position;
            Editable = true;
            Text = text;
            this.color = color;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(Game.Content.Load<Texture2D>("Button"), new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);
            if (Editable && hasFocus && cursorBlinkCounter++ % CURSOR_BLINK_PERIOD < CURSOR_BLINK_PERIOD / 2)
                spriteBatch.DrawString(spriteFont, CURSOR, cursorPosition, color, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);

            SpriteBatch.DrawString(spriteFont, drawnText, Position, color, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (hasFocus)
            {
                KeyboardState currentState = Keyboard.GetState();
                if (currentState.IsKeyDown(Keys.LeftShift) || currentState.IsKeyDown(Keys.RightShift))
                {
                }
                else
                {
                }
            }
            base.Update(gameTime);
        }
    }
}
