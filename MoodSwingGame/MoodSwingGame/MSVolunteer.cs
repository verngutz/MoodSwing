﻿using System;
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
    public class MSVolunteer : MSUnit
    {
        protected override Model Model
        {
            get { return Game.Content.Load<Model>("person"); }
        }

        protected override Effect Effect
        {
            get { return Game.Content.Load<Effect>("Mood"); }
        }

        protected override Texture2D Texture
        {
            get { return Game.Content.Load<Texture2D>("MTextures/tao"); }
        }

        protected override float Speed { get { return 0.65f; } }

        private MSUnit target;
        private MSTower office;
        private bool isGoingToMobber;
        private Node pathToOffice;

        public MSVolunteer(Vector3 position, Node path_to_mobber, Node path_to_office, MSUnit target, MSTower office, MSMap map)
            : base(position, path_to_mobber, map, false)
        {
            this.target = target;
            this.office = office;
            this.isGoingToMobber = true;
            this.pathToOffice = path_to_office;
        }

        public override void Walk(MS3DTile[,] map_array)
        {
 	        base.Walk(map_array);

            if (DestinationReached && isGoingToMobber)
            {
                isGoingToMobber = false;
                Path = pathToOffice;
                target.IsStopped = false;
                target.Follow(this);
            }
            else if (DestinationReached && !isGoingToMobber)
            {
                office.VolunteerReturned();
            }
        }
    }
}