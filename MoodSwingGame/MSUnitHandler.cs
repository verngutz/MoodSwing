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
    public class MSUnitHandler
    {
        private static MSUnitHandler unitHandler = null;

        public static MSUnitHandler getInstance()
        {
            if (unitHandler == null)
                unitHandler = new MSUnitHandler();
            return unitHandler;
        }

        //probability constant that handles unit generation
        private const int PROBABILITY = 500;
        //probability constant that handles mob generation
        private const int MOB_PROBABILITY = 10;
        //list of citizens
        private List<MSUnit> citizens;

        private MSUnitHandler() 
        {
            citizens = new List<MSUnit>();
        }

        //something to remove. Make oneOnly = true if you want only one citizen to exist.
        //used for testing only
        private bool oneOnly = false;
        private bool checkOne = false;
        public MSUnit TryForBaby( MSMap map )
        {
            int rnd = MSRandom.random.Next(5000);

            if (oneOnly && checkOne)
                return null;
            if (rnd < PROBABILITY)
            {
                checkOne = true;
                MSUnit person;
                Vector2 start = map.GetRandomBuilding();
                Vector2 end = map.GetRandomBuilding();
                Node path = map.GetPath(start, end);

                if (rnd < MOB_PROBABILITY)
                    person = new MSCitizen(MoodSwing.getInstance().Content.Load<Model>("mob"),
                        (map.MapArray[(int)start.X, (int)start.Y] as MS3DComponent).Position + new Vector3(0, 0, 20),
                        map.GetPath(start, MSDistrictHall.getInstance().TileCoordinate),
                        true);
                else
                    person =  new MSCitizen(MoodSwing.getInstance().Content.Load<Model>("person"), 
                        (map.MapArray[(int)start.X, (int)start.Y] as MS3DComponent).Position + new Vector3(0, 0, 20),
                        path,false);

                citizens.Add(person);
                return person;
            }

            return null;
        }

        public List<MS3DComponent> Update(MSMap map)
        {
            List<MS3DComponent> list = new List<MS3DComponent>();

            foreach (MSUnit person in citizens)
            {
                if (person is MSCitizen)
                {
                    MSCitizen citizen = person as MSCitizen;
                    int rnd = MSRandom.random.Next(5000);

                    if (rnd <= 3000 && !citizen.IsMobbing)
                    {
                        foreach (MSUnit p in citizens)
                        {
                            if (p is MSCitizen && (p as MSCitizen).IsMobbing)
                            {
                                if (Vector3.Distance(citizen.Position, p.Get3DComponent().Position) <= 5)
                                {
                                    citizen.Follow(p as MSCitizen);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            foreach (MSUnit person in citizens)
            {
                if( !person.IsThere() )
                    person.Walk(map.MapArray);
                else
                    list.Add(person.Get3DComponent());
            }

            foreach (MSCitizen person in list)
            {
                citizens.Remove(person);
                person.Dispose();
            }
            return list;
        }

        
        
    }
}
