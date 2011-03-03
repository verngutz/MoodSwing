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
    /// MSGUIClickable is a GUI component that is both drawable and clickable
    /// </summary>
    public abstract class MSGUIClickable : MS2DClickable, MSGUIObject
    {
        public enum MSGUIClickableState 
        { 
            LEFTCLICKED, 
            UNLEFTCLICKED, 
            RIGHTCLICKED, 
            UNRIGHTCLICKED, 
            MIDDLECLICKED,
            UNMIDDLECLICKED,
            HOVERED, 
            UNHOVERED
        }
        public MSGUIClickableState CurrentState { set; get; }

        protected MSToolTip toolTip;
        public MSToolTip ToolTip { get { return toolTip; } }

        public MSGUIClickable(Rectangle boundingRectangle, Shape shape, MSToolTip toolTip, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, shape, spriteBatch, game) 
        {
            this.toolTip = toolTip;
        }

        /// <summary>
        /// What to do when this MSGUIClickable has been left-clicked.
        /// </summary>
        public abstract void LeftClick();

        /// <summary>
        /// What to do when this MSGUIClickable has been middle-clicked.
        /// </summary>
        public abstract void MiddleClick();

        /// <summary>
        /// What to do when this MSGUIClickable has been right-clicked.
        /// </summary>
        public abstract void RightClick();

        /// <summary>
        /// What to do when this MSGUIClickable has been hovered.
        /// </summary>
        public abstract void Hover();

        /// <summary>
        /// What to do when this MSGUIClickable has previously been hovered but now isn't.
        /// </summary>
        public abstract void UnHover();

        /// <summary>
        /// What to do when this MSGUIClickable has previously been left-clicked and is now released.
        /// </summary>
        public abstract void UnLeftClick();

        /// <summary>
        /// What to do when this MSGUIClickable has previously been right-clicked and is now released.
        /// </summary>
        public abstract void UnRightClick();

        /// <summary>
        /// What to do when this MSGUIClickable has previously been middle-clicked and is now released.
        /// </summary>
        public abstract void UnMiddleClick();
    }
}
