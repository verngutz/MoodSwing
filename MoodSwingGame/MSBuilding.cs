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
    public class MSBuilding : MSTile
    {
        public MSBuilding(Vector2 position, Vector2 size, SpriteBatch spriteBatch, Texture2D t)
            : base(position, size, spriteBatch, t)
        {

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteBatch.Draw(texture, Position, Color.White);
            base.Draw(gameTime);
        }
    }
}
