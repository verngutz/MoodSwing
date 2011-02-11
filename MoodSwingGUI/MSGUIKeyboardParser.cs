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
    /// <summary>
    /// MSKeyboardParser helps MSGUITypable objects by providing a static parse method,
    /// which will parse XNA keyboard input into either ASCII characters as direct text input or
    /// into String representations of certain commands which can also be used as direct input,
    /// but is better suited to be used as a sentinel for performing certain form-based actions.
    /// </summary>
    public static class MSGUIKeyboardParser
    {
        public const string EXCLAMATION = "!";
        public const string AT = "@";
        public const string HASH = "#";
        public const string DOLLAR = "$";
        public const string PERCENT = "%";
        public const string CARET = "^";
        public const string AMPERSAND = "&";
        public const string ASTERISK = "*";

        public const string OPEN_PARENTHESIS = "(";
        public const string CLOSE_PARENTHESIS = ")";
        public const string OPEN_BRACKET = "[";
        public const string CLOSE_BRACKET = "]";
        public const string OPEN_BRACE = "{";
        public const string CLOSE_BRACE = "}";
        public const string OPEN_ANGLE = "<";
        public const string CLOSE_ANGLE = ">";

        public const string COMMA = ",";
        public const string PERIOD = ".";
        public const string QUESTION_MARK = "?";
        public const string SLASH = "/";
        public const string COLON = ":";
        public const string SEMICOLON = ";";
        public const string DOUBLE_QUOTE = "\"";
        public const string SINGLE_QUOTE = "\'";

        public const string SPACE = " ";

        public const string BACKSLASH = "\\";
        public const string PIPE = "|";

        public const string PLUS = "+";
        public const string EQUALS = "=";
        public const string MINUS = "-";
        public const string UNDERSCORE = "_";
        public const string TILDE = "~";
        public const string ACCENT_MARK = "`";

        public const string ENTER = "ENTER";
        public const string TAB = "TAB";

        public const string UP = "UP";
        public const string DOWN = "DOWN";
        public const string LEFT = "LEFT";
        public const string RIGHT = "RIGHT";
        public const string HOME = "HOME";
        public const string END = "END";
        public const string INSERT = "INS";
        public const string PAGE_UP = "PGUP";
        public const string PAGE_DOWN = "PGDN";

        public const string CONTROL = "CTRL";
        public const string ALT = "ALT";
        public const string DELETE = "DEL";

        public const string ESC = "ESC";

        public const string CAPS_LOCK = "CAPS";
        public const string SHIFT = "SHIFT";

        public const string BACKSPACE = "BAKCSPACE";

        private const int ALPHA_CAPITAL_OFFSET = 32;

        private const int KEYBOARD_HOLD_TIME = 120;
        private static int keyboardHoldCounter = 0;

        private static bool IsAlpha(Keys key)
        {
            return (key >= Keys.A && key <= Keys.Z);
        }

        private static bool IsNumeric(Keys key)
        {
            return ((key >= Keys.D0 && key <= Keys.D9) || (key >= Keys.NumPad0 && key <= Keys.NumPad9));
        }

        /// <summary>
        /// Parses XNA keyboard input into ASCII characters or String commands
        /// </summary>
        /// <param name="oldKeyboardState">the old KeyboardState of the Keyboard</param>
        /// <returns>a List of String representations defined by the MSGUIKeyboardParser constants</returns>
        public static List<String> ParseTextInput(KeyboardState oldKeyboardState)
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
                    }
                    else if (IsNumeric(key))
                    {
                        if (shiftOn)
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
                                if (System.Console.NumberLock) values.Add(PERIOD);
                                else values.Add(DELETE);
                                break;
                            case Keys.Insert:
                                values.Add(INSERT);
                                break;
                        }
                    }
                }
            }

            Keys lastKeyPressed = currentState.GetPressedKeys().Last<Keys>();
            if (oldKeyboardState.IsKeyDown(lastKeyPressed))
            {
                if (currentState.IsKeyUp(lastKeyPressed))
                    keyboardHoldCounter = 0;
                else if (currentState.IsKeyDown(lastKeyPressed))
                {
                    if (keyboardHoldCounter++ == KEYBOARD_HOLD_TIME)
                    {
                        keyboardHoldCounter = 0;
                        if (IsAlpha(lastKeyPressed))
                        {
                            if (alphaCapital)
                                values.Add(((char)(lastKeyPressed + ALPHA_CAPITAL_OFFSET)).ToString());
                            else
                                values.Add(((char)lastKeyPressed).ToString());
                        }
                        else if (IsNumeric(lastKeyPressed))
                        {
                            if (shiftOn)
                            {
                                switch (lastKeyPressed)
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
                                switch (lastKeyPressed)
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
                                        values.Add(((char)lastKeyPressed).ToString());
                                        break;
                                }

                        }
                        else
                        {
                            switch (lastKeyPressed)
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
                                    if (System.Console.NumberLock) values.Add(PERIOD);
                                    else values.Add(DELETE);
                                    break;
                                case Keys.Insert:
                                    values.Add(INSERT);
                                    break;
                            }
                        }
                    }
                }
            }

            return values;
        }
    }
}
