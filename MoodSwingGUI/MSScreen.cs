using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoodSwingCoreComponents;

namespace MoodSwingGUI
{
    /// <summary>
    /// MSScreen is an MSPanel that covers the entire computer screen.
    /// </summary>
    public abstract class MSScreen : MSPanel
    {
        private MSGUIClickable currentHovered;
        private MSGUITypable currentFocused;

        /// <summary>
        /// Creates an MSScreen with the given parameters
        /// </summary>
        /// <param name="background">the background texture of this MSScreen</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSScreen</param>
        /// <param name="game">the Game where this MSScreen will be used</param>
        public MSScreen(Texture2D background, SpriteBatch spriteBatch, Game game)
            : this(background, 0, 0, 0, 0, Color.White, spriteBatch, game) { }

        /// <summary>
        /// Creates an MSScreen with the given parameters
        /// </summary>
        /// <param name="background">the background texture of this MSScreen</param>
        /// <param name="highlight">the Color to overlay this MSScreen with</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSScreen</param>
        /// <param name="game">the Game where this MSScreen will be used</param>
        public MSScreen(Texture2D background, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, 0, 0, 0, 0, highlight, spriteBatch, game) { }

        /// <summary>
        /// Creates an MSScreen with the given parameters
        /// </summary>
        /// <param name="background">the background texture of this MSScreen</param>
        /// <param name="topPadding">the padding (or margin) on the top portion of this MSScreen, in pixels</param>
        /// <param name="bottomPadding">the padding (or margin) on the bottom portion of this MSScreen, in pixels</param>
        /// <param name="leftPadding">the padding(or margin) on the left portion of this MSScreen, in pixels</param>
        /// <param name="rightPadding">the padding (or margin) on the right portion of this MSScreen, in pixels</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSScreen</param>
        /// <param name="game">the Game where this MSScreen will be used</param>
        public MSScreen(Texture2D background, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : this(background, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, spriteBatch, game) { }

        /// <summary>
        /// Creates an MSScreen with the given parameters
        /// </summary>
        /// <param name="background">the background texture of this MSScreen</param>
        /// <param name="topPadding">the padding (or margin) on the top portion of this MSScreen, in pixels</param>
        /// <param name="bottomPadding">the padding (or margin) on the bottom portion of this MSScreen, in pixels</param>
        /// <param name="leftPadding">the padding(or margin) on the left portion of this MSScreen, in pixels</param>
        /// <param name="rightPadding">the padding (or margin) on the right portion of this MSScreen, in pixels</param>
        /// <param name="highlight">the Color to overlay this MSScreen with</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSScreen</param>
        /// <param name="game">the Game where this MSScreen will be used</param>
        public MSScreen(Texture2D background, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : base(background, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), topPadding, bottomPadding, leftPadding, rightPadding, Shape.RECTANGULAR, spriteBatch, game) { }

        public virtual void HandleMouseInput(MouseState oldMouseState)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState != oldMouseState)
            {
                bool hasHovered = false;
                foreach (MSGUIClickable component in ClickableComponents)
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
                            if (currentFocused != null) currentFocused.HasFocus = false;
                            currentFocused = (MSGUITypable)currentHovered;
                            currentFocused.HasFocus = true;
                        }
                        currentHovered.UnLeftClick();
                        currentHovered = null;
                    }

                    if (currentMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
                    {
                        currentHovered.RightClick();
                    }
                    else if (currentMouseState.RightButton == ButtonState.Released && oldMouseState.RightButton == ButtonState.Pressed)
                    {
                        currentHovered.UnRightClick();
                    }

                    if (currentMouseState.MiddleButton == ButtonState.Pressed && oldMouseState.MiddleButton == ButtonState.Released)
                    {
                        currentHovered.MiddleClick();
                    }
                    else if (currentMouseState.MiddleButton == ButtonState.Released && oldMouseState.MiddleButton == ButtonState.Pressed)
                    {
                        currentHovered.UnMiddleClick();
                    }
                }
            }
        }

        public override void HandleKeyboardInput(KeyboardState oldKeyboardState)
        {
            if(currentFocused != null)
                currentFocused.HandleKeyboardInput(oldKeyboardState);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (currentFocused != null && currentFocused.TabIsFired)
            {
                currentFocused.HasFocus = false;
                currentFocused.TabIsFired = false;
                currentFocused = currentFocused.OnTab;
                currentFocused.HasFocus = true;
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
