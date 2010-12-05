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

namespace MoodSwingGUI
{
    public abstract class MS2DComponent : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        public MS2DComponent(SpriteBatch spriteBatch, Game game)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
        }
    }
}
