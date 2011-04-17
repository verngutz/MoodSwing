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
    /// MSGUITypable is an MSGUIClickable component that can handle keyboard input.
    /// MSGUITypable requires a facade that manages which instance is the one currently given keyboard focus.
    /// </summary>
    public abstract class MSGUITypable : MSGUIClickable
    {
        /// <summary>
        /// Gets or Sets whether this MSGUITypable currently has keyboard focus
        /// </summary>
        /// <value>true if it does, false otherwise</value>
        public bool HasKeyboardFocus { get; set; }


        protected MSGUITypable onTab;
        /// <summary>
        /// The MSGUITypable that will become in focus once this MSGUITypable receives a tab input.
        /// </summary>
        public MSGUITypable OnTab { get { return onTab; } }

        /// <summary>
        /// Gets or Sets this MSGUITypable's Tab signal.
        /// This will be true if this MSGUITypable receives a tab input that needs to be responded to.
        /// At other times, this will be false.
        /// Make sure to set this to false right after the tab signal has been responded to.
        /// </summary>
        public bool TabIsFired { set; get; }

        public MSGUITypable(Rectangle boundingRectangle, MSToolTip toolTip, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, shape, toolTip, spriteBatch, game) { HasKeyboardFocus = false; }

        /// <summary>
        /// Handles how this MSGUITypable should respond to keyboard input
        /// </summary>
        /// <param name="oldKeyboardState">the old KeyboardState of the Keyboard</param>
        public abstract void HandleKeyboardInput(KeyboardState oldKeyboardState);
    }
}
