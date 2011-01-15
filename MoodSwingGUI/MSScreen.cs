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
    public class MSScreen : MSPanel
    {
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
            : base(background, new Vector2(0, 0), new Vector2(game.GraphicsDevice.DisplayMode.Width, game.GraphicsDevice.DisplayMode.Height), topPadding, bottomPadding, leftPadding, rightPadding, spriteBatch, game) { }
    }
}
