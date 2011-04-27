using System;
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
        private double rand = MSRandom.random.NextDouble();
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

        public override float Speed 
        { 
            get { return 0.65f; }
            set { throw new NotImplementedException(); }
        }

        private MSChangeableBuilding toBuild;

        public MSWorker(Vector3 position, Node path, MSChangeableBuilding to_build, MSMap map, float initRotation)
            : base(position, path, map, false, initRotation)
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
