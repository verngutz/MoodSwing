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
    public class MSRoad : MS3DTile
    {
        public MSRoad(Model model, Texture2D texture, Effect effect, Vector3 position, float rotation)
            : base(model, texture, effect, position)
        {
            world = Matrix.CreateRotationZ(rotation) * world;
        }

    
    }
}
