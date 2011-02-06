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
    public abstract class MSGUITypable : MSGUIClickable
    {
        protected const string EXCLAMATION = "!";
        protected const string AT = "@";
        protected const string HASH = "#";
        protected const string DOLLAR = "$";
        protected const string PERCENT = "%";
        protected const string CARET = "^";
        protected const string AMPERSAND = "&";
        protected const string ASTERISK = "*";

        protected const string OPEN_PARENTHESIS = "(";
        protected const string CLOSE_PARENTHESIS = ")";
        protected const string OPEN_BRACKET = "[";
        protected const string CLOSE_BRACKET = "]";
        protected const string OPEN_BRACE = "{";
        protected const string CLOSE_BRACE = "}";
        protected const string OPEN_ANGLE = "<";
        protected const string CLOSE_ANGLE = ">";

        protected const string COMMA = ",";
        protected const string PERIOD = ".";
        protected const string QUESTION_MARK = "?";
        protected const string SLASH = "/";
        protected const string COLON = ":";
        protected const string SEMICOLON = ";";
        protected const string DOUBLE_QUOTE = "\"";
        protected const string SINGLE_QUOTE = "\'";

        protected const string SPACE = " ";

        protected const string BACKSLASH = "\\";
        protected const string PIPE = "|";

        protected const string PLUS = "+";
        protected const string EQUALS = "=";
        protected const string MINUS = "-";
        protected const string UNDERSCORE = "_";
        protected const string TILDE = "~";
        protected const string ACCENT_MARK = "`";

        protected const string ENTER = "ENTER";
        protected const string TAB = "TAB";

        protected const string UP = "UP";
        protected const string DOWN = "DOWN";
        protected const string LEFT = "LEFT";
        protected const string RIGHT = "RIGHT";
        protected const string HOME = "HOME";
        protected const string END = "END";
        protected const string INSERT = "INS";
        protected const string PAGE_UP = "PGUP";
        protected const string PAGE_DOWN = "PGDN";

        protected const string CONTROL = "CTRL";
        protected const string ALT = "ALT";
        protected const string DELETE = "DEL";

        protected const string ESC = "ESC";

        protected const string CAPS_LOCK = "CAPS";
        protected const string SHIFT = "SHIFT";

        protected const string BACKSPACE = "BAKCSPACE";

        private const int ALPHA_CAPITAL_OFFSET = 32;

        protected bool hasFocus;

        public MSGUITypable(Vector2 position, Vector2 size, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(position, size, shape, spriteBatch, game) { }

        public override bool CheckMouseClick(MouseState oldMouseState)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState.LeftButton == ButtonState.Pressed
                && oldMouseState.LeftButton == ButtonState.Released)
            {
                if (CollidesWithMouse()) hasFocus = true;
                else hasFocus = false;
            }
            return hasFocus;
        }

        public abstract bool CheckKeyboardInput(KeyboardState oldKeyboardState);

        private bool IsAlpha(Keys key)
        {
            return (key >= Keys.A && key <= Keys.Z);
        }

        private bool IsNumeric(Keys key)
        {
            return ((key >= Keys.D0 && key <= Keys.D9) || (key >= Keys.NumPad0 && key <= Keys.NumPad9));
        }

        protected List<String> ParseTextInput(KeyboardState oldKeyboardState)
        {
            List<String> values = new List<String>();
            KeyboardState currentState = Keyboard.GetState();
            bool shiftOn = currentState.IsKeyDown(Keys.LeftShift) || currentState.IsKeyDown(Keys.RightShift);
            bool alphaCapital = (!System.Console.CapsLock || shiftOn) && (System.Console.CapsLock || !shiftOn);

            foreach (Keys key in currentState.GetPressedKeys())
            {
                if (oldKeyboardState.IsKeyUp(key))
                {
                    if (IsAlpha(key))
                    {
                        if (alphaCapital)
                            values.Add(((char)(key + ALPHA_CAPITAL_OFFSET)).ToString());
                        else
                            values.Add(((char)key).ToString());
                        break;
                    }
                    else if (IsNumeric(key))
                    {
                        if(shiftOn)
                        {
                            switch (key)
                            {
                                case Keys.D0:
                                    values.Add(CLOSE_PARENTHESIS);
                                    break;
                                case Keys.D1:
                                    values.Add(EXCLAMATION);
                                    break;
                                case Keys.D2:
                                    values.Add(AT);
                                    break;
                                case Keys.D3:
                                    values.Add(HASH);
                                    break;
                                case Keys.D4:
                                    values.Add(DOLLAR);
                                    break;
                                case Keys.D5:
                                    values.Add(PERCENT);
                                    break;
                                case Keys.D6:
                                    values.Add(CARET);
                                    break;
                                case Keys.D7:
                                    values.Add(AMPERSAND);
                                    break;
                                case Keys.D8:
                                    values.Add(ASTERISK);
                                    break;
                                case Keys.D9:
                                    values.Add(OPEN_PARENTHESIS);
                                    break;
                            }
                        }
                        else
                            switch (key)
                            {
                                case Keys.NumPad0:
                                    if (System.Console.NumberLock) values.Add(INSERT);
                                    else goto default;
                                    break;
                                case Keys.NumPad1:
                                    if (System.Console.NumberLock) values.Add(END);
                                    else goto default;
                                    break;
                                case Keys.NumPad2:
                                    if (System.Console.NumberLock) values.Add(DOWN);
                                    else goto default;
                                    break;
                                case Keys.NumPad3:
                                    if (System.Console.NumberLock) values.Add(PAGE_DOWN);
                                    else goto default;
                                    break;
                                case Keys.NumPad4:
                                    if (System.Console.NumberLock) values.Add(LEFT);
                                    else goto default;
                                    break;
                                case Keys.NumPad6:
                                    if (System.Console.NumberLock) values.Add(RIGHT);
                                    else goto default;
                                    break;
                                case Keys.NumPad7:
                                    if (System.Console.NumberLock) values.Add(HOME);
                                    else goto default;
                                    break;
                                case Keys.NumPad8:
                                    if (System.Console.NumberLock) values.Add(UP);
                                    else goto default;
                                    break;
                                case Keys.NumPad9:
                                    if (System.Console.NumberLock) values.Add(PAGE_UP);
                                    else goto default;
                                    break;
                                default:
                                    values.Add(((char)key).ToString());
                                    break;
                            }

                    }
                    else
                    {
                        switch (key)
                        {
                            case Keys.Space:
                                values.Add(SPACE);
                                break;
                            case Keys.Enter:
                                values.Add(ENTER);
                                break;
                            case Keys.Tab:
                                values.Add(TAB);
                                break;
                            case Keys.Left:
                                values.Add(LEFT);
                                break;
                            case Keys.Right:
                                values.Add(RIGHT);
                                break;
                            case Keys.CapsLock:
                                values.Add(CAPS_LOCK);
                                break;
                            case Keys.LeftShift:
                            case Keys.RightShift:
                                values.Add(SHIFT);
                                break;
                            case Keys.Back:
                                values.Add(BACKSPACE);
                                break;
                            case Keys.Home:
                                values.Add(HOME);
                                break;
                            case Keys.End:
                                values.Add(END);
                                break;
                            case Keys.OemBackslash:
                                if (shiftOn) values.Add(BACKSLASH);
                                else values.Add(PIPE);
                                break;
                            case Keys.OemOpenBrackets:
                                if (shiftOn) values.Add(OPEN_BRACE);
                                else values.Add(OPEN_BRACKET);
                                break;
                            case Keys.OemCloseBrackets:
                                if (shiftOn) values.Add(CLOSE_BRACE);
                                else values.Add(CLOSE_BRACKET);
                                break;
                            case Keys.OemComma:
                                if (shiftOn) values.Add(OPEN_ANGLE);
                                else values.Add(COMMA);
                                break;
                            case Keys.OemPeriod:
                                if (shiftOn) values.Add(CLOSE_ANGLE);
                                else values.Add(PERIOD);
                                break;
                            case Keys.OemPipe:
                                if (shiftOn) values.Add(PIPE);
                                else values.Add(BACKSLASH);
                                break;
                            case Keys.OemQuestion:
                                if (shiftOn) values.Add(QUESTION_MARK);
                                else values.Add(SLASH);
                                break;
                            case Keys.OemQuotes:
                                if (shiftOn) values.Add(DOUBLE_QUOTE);
                                else values.Add(SINGLE_QUOTE);
                                break;
                            case Keys.OemSemicolon:
                                if (shiftOn) values.Add(COLON);
                                else values.Add(SEMICOLON);
                                break;
                            case Keys.OemTilde:
                                if (shiftOn) values.Add(TILDE);
                                else values.Add(ACCENT_MARK);
                                break;
                            case Keys.OemPlus:
                                if (shiftOn) values.Add(PLUS);
                                else values.Add(EQUALS);
                                break;
                            case Keys.OemMinus:
                                if (shiftOn) values.Add(UNDERSCORE);
                                else values.Add(MINUS);
                                break;
                            case Keys.Add:
                                values.Add(PLUS);
                                break;
                            case Keys.Subtract:
                                values.Add(MINUS);
                                break;
                            case Keys.Multiply:
                                values.Add(ASTERISK);
                                break;
                            case Keys.Divide:
                                values.Add(SLASH);
                                break;
                            case Keys.Decimal:
                                if(System.Console.NumberLock) values.Add(PERIOD);
                                else values.Add(DELETE);
                                break;
                            case Keys.Insert:
                                values.Add(INSERT);
                                break;
                        }
                    }
                }
            }
            return values;
        }
    }
}
