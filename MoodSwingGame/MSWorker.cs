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
        protected override Model Model
        {
            get { return Game.Content.Load<Model>("person"); }
        }

        protected override Effect Effect
        {
            get { return Game.Content.Load<Effect>("Mood"); }
        }

        protected override Texture Texture
        {
            get { return Game.Content.Load<Texture2D>("MTextures/tao"); }
        }

        protected override float Speed
        {
            get { return 0.5f; }
        }

        private MSBuyableBuilding toBuild;

        public MSWorker(Vector3 position, Node path, MSBuyableBuilding to_build, MSMap map)
            : base(position, path, map, false)
        {
            toBuild = to_build;
        }

        public override void Walk(MS3DTile[,] mapArray)
        {
            base.Walk(mapArray);

            if (DestinationReached)
            {
                toBuild.AddWorkers();
                
            }
        }
    }
}
