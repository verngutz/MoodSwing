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
    public class MSTextField : MSGUITypable
    {
        private String storedText;
        private String drawnText;
        public String Text 
        {
            get { return storedText; }
            set
            {
                foreach (char c in value.ToCharArray())
                {
                    Input(c.ToString());
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

        private int drawnTextStartIndex;
        private int drawnTextEndIndex;

        public bool Editable { set; get; }
        private int cursorIndex;
        private Vector2 cursorPosition;
        private int cursorBlinkCounter;
        private const int CURSOR_BLINK_PERIOD = 30;
        private const string CURSOR = "|";

        private SpriteFont spriteFont;
        private Color color;

        private MSAction onEnter;
        private MSTextField onTab;

        public MSTextField(String text, int x, int y, int width, int height, MSAction onEnter, MSTextField onTab, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, x, y, width, height, onEnter, onTab, spriteFont, Color.Black, spriteBatch, game) { }

        public MSTextField(String text, int x, int y, int width, int height, MSAction onEnter, MSTextField onTab, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : this(text, new Vector2(x, y), new Vector2(width, height), onEnter, onTab, spriteFont, color, spriteBatch, game) { }

        public MSTextField(String text, Rectangle boundingRectangle, MSAction onEnter, MSTextField onTab, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, boundingRectangle, onEnter, onTab, spriteFont, Color.Black, spriteBatch, game) { }

        public MSTextField(String text, Rectangle boundingRectangle, MSAction onEnter, MSTextField onTab, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : this(text, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), onEnter, onTab, spriteFont, color, spriteBatch, game) { }

        public MSTextField(String text, Vector2 position, Vector2 size, MSAction onEnter, MSTextField onTab, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, position, size, onEnter, onTab, spriteFont, Color.Black, spriteBatch, game) { }

        public MSTextField(String text, Vector2 position, Vector2 size, MSAction onEnter, MSTextField onTab, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : base(position, size, Shape.RECTANGULAR, spriteBatch, game)
        {
            this.spriteFont = spriteFont;
            fontScale = Size.Y / spriteFont.MeasureString(CURSOR).Y;
            cursorIndex = 0;
            cursorBlinkCounter = 0;
            cursorPosition = new Vector2(Position.X - spriteFont.MeasureString(CURSOR).X * 5 * fontScale / 12, Position.Y);
            Editable = true;
            storedText = "";
            drawnText = "";
            Text = text;
            this.color = color;

            this.onEnter = onEnter;
            this.onTab = onTab;

            drawnTextStartIndex = 0;
            drawnTextEndIndex = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Draw(Game.Content.Load<Texture2D>("DistrictView"), new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);
            if (Editable && hasFocus && cursorBlinkCounter++ % CURSOR_BLINK_PERIOD < CURSOR_BLINK_PERIOD / 2)
                spriteBatch.DrawString(spriteFont, CURSOR, cursorPosition, color, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);

            SpriteBatch.DrawString(spriteFont, "Cursor index:" + cursorIndex, new Vector2(0, 0), Color.White, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
            SpriteBatch.DrawString(spriteFont, "Drawn text start index:" + drawnTextStartIndex, new Vector2(0, 50), Color.White, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
            SpriteBatch.DrawString(spriteFont, "Drawn text end index:" + drawnTextEndIndex, new Vector2(0, 100), Color.White, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
            SpriteBatch.DrawString(spriteFont, drawnText, Position, color, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
        }

        public override bool CheckKeyboardInput(KeyboardState oldKeyboardState)
        {
            if (Editable && hasFocus)
            {
                foreach (String s in ParseTextInput(oldKeyboardState))
                {
                    if (s.Equals(LEFT))
                        ShiftCursorLeft();

                    else if (s.Equals(RIGHT))
                        ShiftCursorRight();

                    else if (s.Equals(BACKSPACE))
                        RemoveLast();

                    else if (s.Equals(HOME))
                        GoHome();

                    else if (s.Equals(END))
                        GoToEnd();

                    else if (s.Equals(ENTER))
                    {
                        if (onEnter != null)
                        {
                            onEnter.PerformAction(Game);
                        }
                    }

                    else if (s.Equals(TAB))
                    {
                        if (onTab != null)
                        {
                            hasFocus = false;
                            onTab.hasFocus = true;
                        }
                    }

                    else if (s.Equals(CAPS_LOCK) 
                        || s.Equals(SHIFT)
                        || s.Equals(CONTROL)
                        || s.Equals(ALT)
                        || s.Equals(DELETE)
                        || s.Equals(PAGE_UP)
                        || s.Equals(PAGE_DOWN)
                        || s.Equals(INSERT)
                        || s.Equals(UP)
                        || s.Equals(DOWN)
                        || s.Equals(ESC)) { }

                    else
                        Input(s);
                }
            }
            return hasFocus;
        }

        private void Input(String s)
        {
            storedText = storedText.Insert(cursorIndex, s);
            drawnTextEndIndex++;
            drawnText = storedText.Substring(drawnTextStartIndex, drawnTextEndIndex - drawnTextStartIndex);
            ShiftCursorRight();
        }

        private void RemoveLast()
        {
            if (cursorIndex > 0)
            {
                ShiftCursorLeft();
                storedText = storedText.Remove(cursorIndex, 1);
                drawnText = storedText.Substring(drawnTextStartIndex, drawnTextEndIndex - drawnTextStartIndex);
            }
        }

        private void ShiftCursorLeft()
        {
            if (cursorIndex > 0)
            {
                cursorPosition -= new Vector2(spriteFont.MeasureString(storedText.Substring(cursorIndex - 1, 1)).X * fontScale, 0);
                cursorIndex--;
                if (cursorIndex < drawnTextStartIndex)
                {
                    drawnTextStartIndex--;
                    drawnText = storedText.Substring(drawnTextStartIndex, drawnTextEndIndex - drawnTextStartIndex);
                }
                while (spriteFont.MeasureString(drawnText).X * fontScale > Size.X)
                {
                    cursorPosition += new Vector2(spriteFont.MeasureString(storedText.Substring(drawnTextStartIndex, 1)).X * fontScale, 0);
                    drawnTextEndIndex--;
                    drawnText = storedText.Substring(drawnTextStartIndex, drawnTextEndIndex - drawnTextStartIndex);
                }
            }
        }

        private void ShiftCursorRight()
        {
            if (cursorIndex < storedText.Length)
            {
                cursorPosition += new Vector2(spriteFont.MeasureString(storedText.Substring(cursorIndex, 1)).X * fontScale, 0);
                cursorIndex++;
                if (cursorIndex > drawnTextEndIndex)
                {
                    drawnTextEndIndex++;
                    drawnText = storedText.Substring(drawnTextStartIndex, drawnTextEndIndex - drawnTextStartIndex);
                }
                while (spriteFont.MeasureString(drawnText).X * fontScale > Size.X)
                {
                    cursorPosition -= new Vector2(spriteFont.MeasureString(storedText.Substring(drawnTextStartIndex, 1)).X * fontScale, 0);
                    drawnTextStartIndex++;
                    drawnText = storedText.Substring(drawnTextStartIndex, drawnTextEndIndex - drawnTextStartIndex);
                }
            }
        }

        private void GoHome()
        {
            while (cursorIndex > 0) ShiftCursorLeft();
        }

        private void GoToEnd()
        {
            while (cursorIndex < storedText.Length) ShiftCursorRight();
        }
    }
}

