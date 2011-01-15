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
    /// MSImageHolder holds a Texture2D and may be added to an MSPanel
    /// <seealso cref="MoodSwingGUI.MSPanel"/>
    /// </summary>
    public class MSImageHolder : MS2DComponent
    {
        private Texture2D image;
        private Vector2 scale;

        /// <summary>
        /// Create an MSImageHolder with the given parameters
        /// </summary>
        /// <param name="x">the x-position, in screen coordinates, corresponding to where the upper-left corner of this MSImageHolder should be</param>
        /// <param name="y">the y-position, in screen coordinates, corresponding to where the upper-left corner of this MSImageHolder should be</param>
        /// <param name="width">width in pixels of this MSHolder</param>
        /// <param name="height">height in pixels of this MSHolder</param>
        /// <param name="image">will be displayed in this MSImageHolder</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSImageHolder</param>
        /// <param name="game">the Game where this MSImageHolder will be used</param>
        public MSImageHolder(float x, float y, float width, float height, Texture2D image, SpriteBatch spriteBatch, Game game)
            : this(new Vector2(x, y), new Vector2(width, height), image, spriteBatch, game) { }

        /// <summary>
        /// Create an MSImageHolder with the given parameters
        /// </summary>
        /// <param name="boundingRectangle">the Rectangle that encloses this MSImageHolder</param>
        /// <param name="image">will be displayed in this MSImageHolder</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSImageHolder</param>
        /// <param name="game">the Game where this MSImageHolder will be used</param>
        public MSImageHolder(Rectangle boundingRectangle, Texture2D image, SpriteBatch spriteBatch, Game game)
            : this(new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), image, spriteBatch, game) { }

        /// <summary>
        /// Create an MSImageHolder with the given parameters
        /// </summary>
        /// <param name="position">represents the position of this MSImageHolder in screen coordinates</param>
        /// <param name="size">represents the size of this MSImageHolder in pixels</param>
        /// <param name="image">will be displayed in this MSImageHolder</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSImageHolder</param>
        /// <param name="game">the Game where this MSImageHolder will be used</param>
        public MSImageHolder(Vector2 position, Vector2 size, Texture2D image, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game)
        {
            this.image = image;
            scale = Size / new Vector2(image.Width, image.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(image, Position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            base.Draw(gameTime);
        }
    }
}
