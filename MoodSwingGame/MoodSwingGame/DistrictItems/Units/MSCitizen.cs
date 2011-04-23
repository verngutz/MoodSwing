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
    public class MSCitizen : MSUnit
    {
        protected override Model Model 
        { 
            get { return Game.Content.Load<Model>("personBump"); } 
        }

        protected override Texture2D Texture
        {
            get { return Game.Content.Load<Texture2D>("MTextures/tao"); }
        }

        protected override float Speed { get { return 0.35f; } }

        public MSCitizen(Vector3 position, Node path, MSMap map, bool is_mobbable, float initRotation)
            : base(position, path, map, is_mobbable, initRotation) { }
    }
}
