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
    public class MSImageHolder : MS2DComponent
    {
        private Texture2D image;
        private Vector2 scale;

        public MSImageHolder(float x, float y, float width, float height, Texture2D image, SpriteBatch spriteBatch, Game game)
            : this(new Vector2(x, y), new Vector2(width, height), image, spriteBatch, game) { }

        public MSImageHolder(Rectangle boundingRectangle, Texture2D image, SpriteBatch spriteBatch, Game game)
            : this(new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), image, spriteBatch, game) { }

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
