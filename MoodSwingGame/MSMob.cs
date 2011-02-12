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
    public class MSMob : MSUnit
    {
        private List<MSUnit> minions;

        private MSCitizen headMob;

        public MSMob( Model m, Vector3 position, Node p )
        {
            minions = new List<MSUnit>();
            headMob = new MSCitizen(m, position, p);
            minions.Add( headMob );
        }

        public void Add(MSCitizen unit)
        {
            unit.Follow(headMob);
            minions.Add(unit);
        }

        public void Walk(MSTile[,] map)
        {
            headMob.Walk(map);
        }

        public bool IsThere()
        {
            //bool toReturn = true;
            List<MSUnit> toReplace = new List<MSUnit>();
            foreach (MSUnit unit in minions)
            {
                if (unit.IsThere())
                    toReplace.Add(unit);
            }

            foreach (MSUnit unit in toReplace)
            {
                minions.Remove(unit);
            }
            return headMob.IsThere();
        }

        public MS3DComponent Get3DComponent()
        {
            return headMob;
        }
    }
}
