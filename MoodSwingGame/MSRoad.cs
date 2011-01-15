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

namespace MoodSwingGame
{
    public class MSRoad : MSTile
    {

        private float rotation;
        public MSRoad(Vector2 position, Vector2 size, SpriteBatch spriteBatch, Texture2D t, float rotate)
            : base(position, size, spriteBatch, t)
        {
            rotation = rot;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteBatch.Draw( texture, Position, null, Color.White, rotation, Vector2.Zero, 1, SpriteEffects.None, 0);
            base.Draw(gameTime);
        }
    }
}
