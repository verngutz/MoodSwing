using System;
using System.Collections.Generic;
using System.Linq;
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
    /// MSButton represents a drawable, clickable, action-attachable button.
    /// </summary>
    public class MSButton : MSGUITypable
    {
        protected Texture2D clickedTexture;
        protected Texture2D hoveredTexture;
        protected Texture2D unhoveredTexture;

        private Color highlight;

        /// <summary>
        /// Gets the overlay Color of this MSButton
        /// </summary>
        public Color Highlight { get { return highlight; } }

        private MSResizingLabel label;
        protected MSAction action;

        /// <summary>
        /// Gets and sets the bounding Rectangle of this MSButton
        /// If there is an MSLabel associated with this MSButton, its position is automatically adjusted.
        /// </summary>
        public override Rectangle BoundingRectangle
        {
            get
            {
                return base.BoundingRectangle;
            }
            set
            {
                base.BoundingRectangle = value;
                if (label != null)
                    label.Position += new Vector2(value.X, value.Y);
            }
        }

        /// <summary>
        /// Constructs an MSButton with no highlight.
        /// </summary>
        /// <param name="label">Add text to this MSButton with an MSLabel. Use null if you don't need to add text. Make sure to position the MSLabel relative to the upper-left corner of this MSButton.</param>
        /// <param name="action">will be executed when this MSButton is clicked</param>
        /// <param name="boundingRectangle">the bounding Rectangle of this MSButton</param>
        /// <param name="unhovered">will be drawn when this MSButton is unhovered</param>
        /// <param name="clicked">will be drawn when this MSButton is pressed and hovered</param>
        /// <param name="hovered">will be drawn when this MSButton is released and hovered</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSButton</param>
        /// <param name="shape">the Shape of this MSButton<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">the Game where this MSButton will be used</param>
        public MSButton(MSResizingLabel label, MSAction action, Rectangle boundingRectangle, Texture2D unhovered, Texture2D clicked, Texture2D hovered, MSToolTip toolTip, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(label, action, boundingRectangle, unhovered, clicked, hovered, Color.White, toolTip, shape, spriteBatch, game) { }

        /// <summary>
        /// Constructs an MSButton.
        /// </summary>
        /// <param name="label">Add text to this MSButton with an MSLabel. Use null if you don't need to add text. Make sure to position the MSLabel relative to the upper-left corner of this MSButton.</param>
        /// <param name="action">will be executed when this MSButton is clicked</param>
        /// <param name="boundingRectangle">the bounding Rectangle of thisMSButton</param>
        /// <param name="unhovered">will be drawn when this MSButton is unhovered</param>
        /// <param name="clicked">will be drawn when this MSButton is pressed and hovered</param>
        /// <param name="hovered">will be drawn when this MSButton is released and hovered</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSButton</param>
        /// <param name="highlight">the Color to overlay this MSButton with</param>
        /// <param name="shape">the Shape of this MSButton<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">the Game where this MSButton will be used</param>
        public MSButton(MSResizingLabel label, MSAction action, Rectangle boundingRectangle, Texture2D unhovered, Texture2D clicked, Texture2D hovered, Color highlight, MSToolTip toolTip, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, toolTip, shape, spriteBatch, game)
        {
            unhoveredTexture = unhovered;
            clickedTexture = clicked;
            hoveredTexture = hovered;
            this.highlight = highlight;
            collisionTexture = unhovered;
            if (label != null)
            {
                this.label = label;
                this.label.Position += Position;
            }
            this.action = action;
            this.shape = shape;
            UnHover();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Draw(collisionTexture, BoundingRectangle, highlight);
            if (label != null)
                label.Draw(gameTime);
        }

        public override void HandleKeyboardInput(KeyboardState oldKeyboardState)
        {
            if (HasFocus)
            {
                KeyboardState currentState = Keyboard.GetState();
                if (currentState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter))
                {
                    LeftClick();
                }
                else if (currentState.IsKeyUp(Keys.Enter))
                {
                    if (oldKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        UnLeftClick();
                    }
                }
            }
        }

        public override void LeftClick()
        {
            collisionTexture = clickedTexture;
            CurrentState = MSGUIClickableState.LEFTCLICKED;
        }

        public void UnLeftClickNoAction()
        {
            collisionTexture = hoveredTexture;
            CurrentState = MSGUIClickableState.UNLEFTCLICKED;
        }

        public override void UnLeftClick()
        {
            collisionTexture = hoveredTexture;
            CurrentState = MSGUIClickableState.UNLEFTCLICKED;
            action.PerformAction(Game);
        }

        public override void Hover()
        {
            collisionTexture = hoveredTexture;
            CurrentState = MSGUIClickableState.HOVERED;
        }

        public override void UnHover()
        {
            collisionTexture = unhoveredTexture;
            CurrentState = MSGUIClickableState.UNHOVERED;
        }

        public override void MiddleClick() { }
        public override void UnMiddleClick() { }
        public override void RightClick() { }
        public override void UnRightClick() { }
    }
}
