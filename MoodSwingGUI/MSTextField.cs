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
        private String text;

        /// <summary>
        /// Gets or Sets the String stored in this MSTextField
        /// </summary>
        public String Text 
        {
            get { return text; }
            set
            {
                if (Editable)
                {
                    foreach (char c in value.ToCharArray())
                    {
                        InsertAfterCursor(c.ToString());
                    }
                }
                else
                {
                    text = value.Substring(0, Math.Min(value.Length, maxTextLength));
                }
            }
        }

        public override Rectangle BoundingRectangle
        {
            get
            {
                return base.BoundingRectangle;
            }
            set
            {
                base.BoundingRectangle = value;
                cursorPosition += Position;
            }
        }

        private int maxTextLength;

        /// <summary>
        /// Gets or Sets whether this MSTextField is editable or not
        /// </summary>
        public bool Editable { set; get; }

        private int cursorIndex;
        private Vector2 cursorPosition;
        private int cursorBlinkCounter;
        private const int CURSOR_BLINK_PERIOD = 30;
        private const string CURSOR = "|";

        private SpriteFont spriteFont;
        private float fontScale;
        private Color color;

        private MSAction onEnter;

        public MSTextField(String text, Rectangle boundingRectangle, MSAction onEnter, MSGUITypable onTab, int maxTextLength, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : this(text, boundingRectangle, onEnter, onTab, maxTextLength, spriteFont, Color.Black, spriteBatch, game) { }

        /// <summary>
        /// Constructs and MSTextField from the given parameters
        /// </summary>
        /// <param name="text">the initial text this MSTextField contains</param>
        /// <param name="boundingRectangle">the bounding Rectangle of this MSTextField</param>
        /// <param name="onEnter">the MSAction to perform when this MSTextField is focused and the Enter key has been pressed</param>
        /// <param name="onTab">the MSGUITypable that will be given the keyboard focus when this MSTextField is focused and the Tab key has been pressed</param>
        /// <param name="maxTextLength">the maximum length of the text that this MSTextField can contain</param>
        /// <param name="spriteFont">the SpriteFont that will be used to draw the text on this MSTextField</param>
        /// <param name="color">the text color of this MSTextField</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSTextField</param>
        /// <param name="game">the Game where this MSTextField will be used</param>
        public MSTextField(String text, Rectangle boundingRectangle, MSAction onEnter, MSGUITypable onTab, int maxTextLength, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, null, Shape.RECTANGULAR, spriteBatch, game)
        {
            this.spriteFont = spriteFont;
            fontScale = boundingRectangle.Height / spriteFont.MeasureString(CURSOR).Y;
            cursorIndex = 0;
            cursorBlinkCounter = 0;
            cursorPosition = new Vector2(boundingRectangle.X - spriteFont.MeasureString(CURSOR).X * 5 * fontScale / 12, boundingRectangle.Y);
            Editable = false;
            text = "";
            this.maxTextLength = maxTextLength;
            Text = text;
            this.color = color;

            this.onEnter = onEnter;
            this.onTab = onTab;
            TabIsFired = false;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch.DrawString(spriteFont, text, Position, color, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
            if (Editable && HasFocus && cursorBlinkCounter++ % CURSOR_BLINK_PERIOD < CURSOR_BLINK_PERIOD / 2)
                spriteBatch.DrawString(spriteFont, CURSOR, cursorPosition, color, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
        }

        public override void HandleKeyboardInput(KeyboardState oldKeyboardState)
        {
            if (Editable && HasFocus)
            {
                foreach (String s in MSGUIKeyboardParser.ParseTextInput(oldKeyboardState))
                {
                    if (s.Equals(MSGUIKeyboardParser.LEFT))
                        ShiftCursorLeft();

                    else if (s.Equals(MSGUIKeyboardParser.RIGHT))
                        ShiftCursorRight();

                    else if (s.Equals(MSGUIKeyboardParser.BACKSPACE))
                        RemoveBeforeCursor();

                    else if (s.Equals(MSGUIKeyboardParser.HOME))
                        GoHome();

                    else if (s.Equals(MSGUIKeyboardParser.END))
                        GoToEnd();

                    else if (s.Equals(MSGUIKeyboardParser.ENTER))
                    {
                        if (onEnter != null)
                            onEnter.PerformAction(Game);
                    }

                    else if (s.Equals(MSGUIKeyboardParser.TAB))
                    {
                        if (onTab != null)
                            TabIsFired = true;
                    }

                    else if (s.Equals(MSGUIKeyboardParser.CAPS_LOCK)
                        || s.Equals(MSGUIKeyboardParser.SHIFT)
                        || s.Equals(MSGUIKeyboardParser.CONTROL)
                        || s.Equals(MSGUIKeyboardParser.ALT)
                        || s.Equals(MSGUIKeyboardParser.DELETE)
                        || s.Equals(MSGUIKeyboardParser.PAGE_UP)
                        || s.Equals(MSGUIKeyboardParser.PAGE_DOWN)
                        || s.Equals(MSGUIKeyboardParser.INSERT)
                        || s.Equals(MSGUIKeyboardParser.UP)
                        || s.Equals(MSGUIKeyboardParser.DOWN)
                        || s.Equals(MSGUIKeyboardParser.ESC)) { }

                    else
                        InsertAfterCursor(s);
                }
            }
        }

        private void InsertAfterCursor(String s)
        {
            if (text.Length < maxTextLength)
            {
                text = text.Insert(cursorIndex, s);
                ShiftCursorRight();
            }
        }

        private void RemoveBeforeCursor()
        {
            if (cursorIndex > 0)
            {
                ShiftCursorLeft();
                text = text.Remove(cursorIndex, 1);
            }
        }

        private void ShiftCursorLeft()
        {
            if (cursorIndex > 0)
            {
                cursorPosition -= new Vector2(spriteFont.MeasureString(text.Substring(cursorIndex - 1, 1)).X * fontScale, 0);
                cursorIndex--;
            }
        }

        private void ShiftCursorRight()
        {
            if (cursorIndex < text.Length)
            {
                cursorPosition += new Vector2(spriteFont.MeasureString(text.Substring(cursorIndex, 1)).X * fontScale, 0);
                cursorIndex++;
            }
        }

        private void GoHome()
        {
            while (cursorIndex > 0) ShiftCursorLeft();
        }

        private void GoToEnd()
        {
            while (cursorIndex < text.Length) ShiftCursorRight();
        }

        public override void UnLeftClick()
        {
            HasFocus = true;
        }

        public override void LeftClick() { }
        public override void MiddleClick() { }
        public override void UnMiddleClick() { }
        public override void RightClick() { }
        public override void UnRightClick() { }
        public override void Hover() { }
        public override void UnHover() { }
    }
}

