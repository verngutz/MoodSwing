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
    public class MSVolunteeringCitizen : MSCitizen
    {
        protected override float Speed { get { return 0.65f; } }

        public MSVolunteeringCitizen(Vector3 position, Node path, MSMap map)
            : base(position, path, map, false)
        {

        }

        public override void Walk(MS3DTile[,] mapArray)
        {
            base.Walk(mapArray);

            if (DestinationReached)
                MSResourceManager.GetInstance().IdleVolunteers++;
        }
    }
}
