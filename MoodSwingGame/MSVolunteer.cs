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
    public class MSVolunteer : MSCitizen
    {

        private MSCitizen target;
        private MSTower office;
        private bool targetLocked;
        Node pathBack;
        public MSVolunteer( Model model, Texture2D texture, Effect effect, Vector3 position, Node path1,
            Node path2, MSCitizen t, MSTower o)
            : base(model, texture, effect, position, path1, MSCitizen.CitizenState.SUPPRESSED, MSTypes.GENERAL)
        {
            target = t;
            office = o;
            targetLocked = false;
            pathBack = path2;
        }

        public override void Walk(MS3DTile[,] mapArray)
        {
            base.Walk(mapArray);
        }

        public override bool IsThere()
        {
            if (base.IsThere() && !targetLocked)
            {
                targetLocked = true;
                Path = pathBack;
                target.Follow(this);
                target.changeModel("person", "MTextures/tao");
                target.SetState(CitizenState.SUPPRESSED);
            }
            else if (base.IsThere() && targetLocked && target.IsThere() )
            {
                office.VolunteerReturned();
                return true;
            }
            return false;
        }
    }
}
