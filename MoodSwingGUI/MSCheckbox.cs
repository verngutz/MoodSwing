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
    /// MSCheckbox represents a checkbox that has two discrete states, ticked and unticked.
    /// </summary>
    public class MSCheckbox : MSGUIClickable
    {
        private bool isTicked;
        /// <summary>
        /// Gets or sets the tick marker for this MSCheckbox
        /// </summary>
        /// <value>true if this MSCheckbox is ticked, false otherwise</value>
        public bool IsTicked
        {
            get { return isTicked; }
            set
            {
                isTicked = value;
                if (isTicked) current = ticked;
                else current = unticked;

                boundingRectangle = current.BoundingRectangle;
                shape = current.Shape;
                collisionTexture = current.CollisionTexture;
                toolTip = current.ToolTip;
            }
        }

        private MSButton unticked;
        private MSButton ticked;

        private MSButton current;
        /// <summary>
        /// Gets the MSButton representing the current state of this MSCheckbox
        /// </summary>
        public MSButton Current { get { return current; } }

        /// <summary>
        /// Creates an MSCheckbox using two MSButtons
        /// </summary>
        /// <param name="unticked">an MSButton representing the unticked state of this MSCheckbox and which will become invisible once this MSCheckbox is ticked</param>
        /// <param name="ticked">an MSButton representing the ticked state of this MSCheckbox and which will become invisible once this MSCheckbox is unticked</param>
        /// <param name="isTicked">true if this MSCheckbox is initially ticked, false otherwise</param>
        public MSCheckbox(MSButton unticked, MSButton ticked, bool isTicked)
            : base(unticked.BoundingRectangle, unticked.Shape, unticked.ToolTip, unticked.SpriteBatch, unticked.Game) 
        {
            this.unticked = unticked;
            this.ticked = ticked;
            IsTicked = isTicked;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Draw(current.CollisionTexture, current.BoundingRectangle, current.Highlight);
        }

        public override void LeftClick()
        {
            current.LeftClick();
        }

        public override void UnLeftClick()
        {
            current.UnLeftClick();
            IsTicked = !isTicked;
        }

        public override void Hover()
        {
            current.Hover();
        }

        public override void UnHover()
        {
            current.UnHover();
        }

        public override void MiddleClick() { }
        public override void UnMiddleClick() { }
        public override void RightClick() { }
        public override void UnRightClick() { }
    }
}

