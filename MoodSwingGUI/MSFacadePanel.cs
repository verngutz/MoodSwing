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
    public abstract class MSFacadePanel : MSPanel
    {
        protected MSGUIClickable currentHovered;
        protected MSGUITypable currentKeyboardFocused;

        private bool hasMouseFocus;

        public bool HasFocus
        {
            get { return hasMouseFocus && HasKeyboardFocus; }
            set
            {
                hasMouseFocus = value;
                HasKeyboardFocus = value;
            }
        }

        public MSFacadePanel(Texture2D background, Rectangle boundingRectangle, MSToolTip toolTip, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, Color.White, toolTip, shape, spriteBatch, game) { }

        public MSFacadePanel(Texture2D background, Rectangle boundingRectangle, Color highlight, MSToolTip toolTip, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, toolTip, shape, spriteBatch, game) { }

        public MSFacadePanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, MSToolTip toolTip, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, toolTip, shape, spriteBatch, game) { }

        public MSFacadePanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, MSToolTip toolTip, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, highlight, toolTip, shape, spriteBatch, game) 
        {
            HasFocus = true;
        }

        public virtual bool HandleMouseInput(MouseState oldMouseState, bool careIfMouseHasMoved)
        {
            if (hasMouseFocus)
            {
                MouseState currentMouseState = MSMouse.GetState();
                if (currentMouseState != oldMouseState || !careIfMouseHasMoved)
                {
                    bool hasHovered = false;
                    foreach (MSGUIClickable component in ClickableComponents.Reverse<MSGUIClickable>())
                    {
                        if (component.Visible)
                        {
                            if (component.CollidesWithMouse())
                            {
                                if (currentHovered != component)
                                {
                                    if (currentHovered != null) currentHovered.UnHover();
                                    currentHovered = component;
                                    currentHovered.Hover();
                                }
                                hasHovered = true;
                                break;
                            }
                        }
                    }
                    if (!hasHovered)
                    {
                        if (currentHovered != null)
                        {
                            currentHovered.UnHover();
                            currentHovered = null;
                        }
                    }
                    if (currentHovered != null)
                    {
                        if (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                        {
                            currentHovered.LeftClick();
                        }
                        else if (currentMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (currentHovered is MSGUITypable)
                            {
                                if (currentKeyboardFocused != null) currentKeyboardFocused.HasKeyboardFocus = false;
                                currentKeyboardFocused = (MSGUITypable)currentHovered;
                                currentKeyboardFocused.HasKeyboardFocus = true;
                            }
                            currentHovered.UnLeftClick();
                            currentHovered = null;
                            return true;
                        }

                        if (currentMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
                        {
                            currentHovered.RightClick();
                        }
                        else if (currentMouseState.RightButton == ButtonState.Released && oldMouseState.RightButton == ButtonState.Pressed)
                        {
                            currentHovered.UnRightClick();
                            return true;
                        }

                        if (currentMouseState.MiddleButton == ButtonState.Pressed && oldMouseState.MiddleButton == ButtonState.Released)
                        {
                            currentHovered.MiddleClick();
                        }
                        else if (currentMouseState.MiddleButton == ButtonState.Released && oldMouseState.MiddleButton == ButtonState.Pressed)
                        {
                            currentHovered.UnMiddleClick();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public virtual bool HandleMouseInput(MouseState oldMouseState)
        {
            return this.HandleMouseInput(oldMouseState, true);
        }

        public override void HandleKeyboardInput(KeyboardState oldKeyboardState)
        {
            if (currentKeyboardFocused != null)
                currentKeyboardFocused.HandleKeyboardInput(oldKeyboardState);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (currentHovered != null && currentHovered.ToolTip != null)
                currentHovered.ToolTip.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (currentKeyboardFocused != null && currentKeyboardFocused.TabIsFired)
            {
                currentKeyboardFocused.HasKeyboardFocus = false;
                currentKeyboardFocused.TabIsFired = false;
                currentKeyboardFocused = currentKeyboardFocused.OnTab;
                currentKeyboardFocused.HasKeyboardFocus = true;
            }
        }

        public void ResetHovers()
        {
            if (currentHovered != null)
            {
                currentHovered.UnHover();
                currentHovered = null;
            }
        }
    }
}
