﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class MSWorker : MSUnit
    {
        protected override Model Model
        {
            get { return Game.Content.Load<Model>("personBump"); }
        }

        protected override Texture2D Texture
        {
            get { return Game.Content.Load<Texture2D>("MTextures/tao"); }
        }

        public override float Speed 
        { 
            get { return 0.65f; }
            set { throw new NotImplementedException(); }
        }

        private MSChangeableBuilding toBuild;

        public MSWorker(Vector3 position, Node path, MSChangeableBuilding to_build, MSMap map)
            : base(position, path, map, false)
        {
            toBuild = to_build;
        }

        public override void Walk(MS3DTile[,] mapArray, List<MSUnit> units)
        {
            base.Walk(mapArray, units);

            if (DestinationReached)
            {
                toBuild.AddWorkers();
                
            }
        }
    }
}
