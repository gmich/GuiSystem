﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;


namespace GuiSystem.Input
{
    public static class KeyboardUtils
    {

        #region Fields

        private static Dictionary<Keys, CharPair> keyMap = new Dictionary<Keys, CharPair>();

        #endregion

        #region Initialization

        static KeyboardUtils()
        {
            InitializeKeyMap();
        }

        /// <summary>
        /// Initialize character map.
        /// </summary>
        private static void InitializeKeyMap()
        {
            // First row of US keyboard.
            AddKeyMap(Keys.OemTilde, "`~");
            AddKeyMap(Keys.D1, "1!");
            AddKeyMap(Keys.D2, "2@");
            AddKeyMap(Keys.D3, "3#");
            AddKeyMap(Keys.D4, "4$");
            AddKeyMap(Keys.D5, "5%");
            AddKeyMap(Keys.D6, "6^");
            AddKeyMap(Keys.D7, "7&");
            AddKeyMap(Keys.D8, "8*");
            AddKeyMap(Keys.D9, "9(");
            AddKeyMap(Keys.D0, "0)");
            AddKeyMap(Keys.OemMinus, "-_");
            AddKeyMap(Keys.OemPlus, "=+");

            // Second row of US keyboard.
            AddKeyMap(Keys.OemOpenBrackets, "[{");
            AddKeyMap(Keys.OemCloseBrackets, "]}");
            AddKeyMap(Keys.OemPipe, "\\|");

            // Third row of US keyboard.
            AddKeyMap(Keys.OemSemicolon, ";:");
            AddKeyMap(Keys.OemQuotes, "'\"");
            AddKeyMap(Keys.OemComma, ",<");
            AddKeyMap(Keys.OemPeriod, ".>");
            AddKeyMap(Keys.OemQuestion, "/?");

            // Keypad keys of US keyboard.
            AddKeyMap(Keys.NumPad1, "1");
            AddKeyMap(Keys.NumPad2, "2");
            AddKeyMap(Keys.NumPad3, "3");
            AddKeyMap(Keys.NumPad4, "4");
            AddKeyMap(Keys.NumPad5, "5");
            AddKeyMap(Keys.NumPad6, "6");
            AddKeyMap(Keys.NumPad7, "7");
            AddKeyMap(Keys.NumPad8, "8");
            AddKeyMap(Keys.NumPad9, "9");
            AddKeyMap(Keys.NumPad0, "0");
            AddKeyMap(Keys.Add, "+");
            AddKeyMap(Keys.Divide, "/");
            AddKeyMap(Keys.Multiply, "*");
            AddKeyMap(Keys.Subtract, "-");
            AddKeyMap(Keys.Decimal, ".");
        }
        
        #endregion

        #region Helper Class

        /// <summary>
        /// Character pair class that holds normal character and character with shift key pressed
        /// </summary>
        private class CharPair
        {
            private readonly char normalChar;
            private readonly char? shiftChar;

            public CharPair(char normalChar, char? shiftChar)
            {
                this.normalChar = normalChar;
                this.shiftChar = shiftChar;
            }

            public char NormalChar
            {
                get { return normalChar; }
            }
            public char? ShiftChar
            {
                get { return shiftChar; }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a character from key information.
        /// </summary>
        /// <param name="key">Pressing key</param>
        /// <param name="shiftKeyPressed">Is shift key pressed?</param>
        /// <param name="character">Converted character from key input.</param>
        /// <returns>Returns true when it gets a character</returns>
        public static bool KeyToString(Keys key, bool shiftKeyPressed,  out char character)
        {
            bool result = false;
            character = ' ';
            CharPair charPair;

            if ((Keys.A <= key && key <= Keys.Z) || key == Keys.Space)
            {
                // Use as is if it is A～Z, or Space key.
                character = (shiftKeyPressed) ? (char)key : Char.ToLower((char)key);
                result = true;
            }
            else if (keyMap.TryGetValue(key, out charPair))
            {
                // Otherwise, convert by key map.
                if (!shiftKeyPressed)
                {
                    character = charPair.NormalChar;
                    result = true;
                }
                else if (charPair.ShiftChar.HasValue)
                {
                    character = charPair.ShiftChar.Value;
                    result = true;
                }
            }

            return result;
        }


        /// <summary>
        ///　
        /// </summary>
        /// <param name="key">Keyboard key/param>
        /// <param name="charPair">
        /// Character, If it is two characters, first character is for not holding the shift key,
        /// and the second character for holding the shift key.</param>
        public static void AddKeyMap(Keys key, string charPair)
        {
            char char1 = charPair[0];
            char? char2 = null;
            if (charPair.Length > 1)
            {
                char2 = charPair[1];
            }
            keyMap.Add(key, new CharPair(char1, char2));
        }

        #endregion

    }

}
