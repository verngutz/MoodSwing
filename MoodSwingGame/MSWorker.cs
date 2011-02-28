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
    public class MSWorker : MSCitizen
    {
        MSBuyableBuilding target;
        public MSWorker(Model model, Texture2D texture, Effect effect, Vector3 position, Node path, MSTypes mst, MSBuyableBuilding bldg )
            : base(model, texture, effect, position, path, CitizenState.SUPPRESSED, mst)
        {
            target = bldg;
        }

        public override void Walk(MS3DTile[,] mapArray)
        {
            base.Walk(mapArray);
        }

        public override bool IsThere()
        {
            if (base.IsThere())
            {
                target.AddWorkers();
                if (target.FutureSelf is MSVolunteerCenter)
                    MSUnitHandler.GetInstance().IsLeaderBusy = false;
                return true;
            }
            return false;
        }
    }
}
