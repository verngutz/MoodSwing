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
    public class MSTower : MSBuilding
    {
        private int capacity;
        private int range;
        public Vector2 TileCoordinate { get { return new Vector2(position.Y / MSMap.tileDimension, position.X / MSMap.tileDimension); } }
        public MSTower( Model model, Texture2D texture, Effect effect, Vector3 position )
            : base(model, texture, effect, position)
        {
            capacity = 2;
            range = 50;
        }

        public MSVolunteer sentinel( MSMap map )
        {
            MSCitizen target = MSUnitHandler.getInstance().GetTarget(Position, range);
            if (target != null && capacity > 0)
            {
                capacity--;
                target.state = MSCitizen.State.WAITING;
               
                Node path1 = map.GetPath(TileCoordinate, target.TileCoordinate);
                Node path2 = map.GetPath(target.TileCoordinate, TileCoordinate);

                MSVolunteer volunteer = new MSVolunteer(MoodSwing.getInstance().Content.Load<Model>("person"),
                    null,
                    MoodSwing.getInstance().Content.Load<Effect>("Mood"),
                    Position + new Vector3(0,0,20), path1, path2, target as MSCitizen, this);
                MSUnitHandler.getInstance().AddVolunteer(volunteer);
                return volunteer;
            }
            return null;
        }

        public void VolunteerReturned()
        {
            capacity++;
        }
    }
}
