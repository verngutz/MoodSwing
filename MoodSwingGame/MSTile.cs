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
using MoodSwingGUI;


namespace MoodSwingGame
{
    public class MSTile : MS2DComponent
    {

        private Texture2D texture;
        public MSTile( Vector2 position, Vector2 size, SpriteBatch spriteBatch, Texture2D t)
            : base(position, size, spriteBatch, MoodSwing.getInstance())
        {
            texture = t;
            MSLevel level;

        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(texture, Position, Color.White);
            base.Draw(gameTime);
        }
    }
}
