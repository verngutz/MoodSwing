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
        private double rand;
        protected override Model Model 
        { 
            get 
            {
                if (rand >= 0.5)
                    return Game.Content.Load<Model>("TaoBabae");
                else
                    return Game.Content.Load<Model>("TaoLalaki");
            } 
        }

        protected override Texture2D Texture
        {
            get { return null; }
        }

        private float speed = 0.35f;
        public override float Speed 
        { 
            get { return speed; }
            set { speed = value; }
        }

        public MSCitizen(Vector3 position, Node path, MSMap map, bool is_mobbable, float initRotation)
            : base(position, path, map, is_mobbable, initRotation) 
        {
            rand = MSRandom.random.NextDouble();
        }
    }
}
