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
    /// MSImageHolder is an MSGUIUnclickable component that displays a picture using a Texture2D. The image is automatically scaled to fit the bounding Rectangle.
    /// <seealso cref="MoodSwingGUI.MSPanel"/>
    /// </summary>
    public class MSImageHolder : MSGUIUnclickable
    {
        private Texture2D image;

        /// <summary>
        /// Constructs an MSImageHolder
        /// </summary>
        /// <param name="boundingRectangle">the bounding Rectangle of this MSImageHolder</param>
        /// <param name="image">will be displayed in this MSImageHolder</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSImageHolder</param>
        /// <param name="game">the Game where this MSImageHolder will be used</param>
        public MSImageHolder(Rectangle boundingRectangle, Texture2D image, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, spriteBatch, game)
        {
            this.image = image;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Draw(image, BoundingRectangle, Color.White);
        }
    }
}
