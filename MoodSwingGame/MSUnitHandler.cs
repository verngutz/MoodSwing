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

        private const int PROBABILITY = 200;
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
                Vector2 end;
                do
                {
                    end = map.GetRandomBuilding();
                } while (end == start);
                Node path = map.GetPath(start, end);

                if (rnd < MOB_PROBABILITY)
                    person = new MSCitizen(MoodSwing.getInstance().Content.Load<Model>("mob"),
                        MoodSwing.getInstance().Content.Load<Texture2D>("MTextures/mob"),
                        MoodSwing.getInstance().Content.Load<Effect>("Mood"),
                        (map.MapArray[(int)start.X, (int)start.Y] as MS3DComponent).Position + new Vector3(0, 0, 20),
                        map.GetPath(start, MSDistrictHall.getInstance().TileCoordinate),
                        MSCitizen.State.MOB);
                else
                    person = new MSCitizen(MoodSwing.getInstance().Content.Load<Model>("person"),
                        null,
                        MoodSwing.getInstance().Content.Load<Effect>("Mood"),
                        (map.MapArray[(int)start.X, (int)start.Y] as MS3DComponent).Position + new Vector3(0, 0, 20),
                        path, MSCitizen.State.CIVILIAN);

                citizens.Add(person);
                return person;
            }

            return null;
        }

        public void AddVolunteer(MSVolunteer volunteer)
        {
            citizens.Add(volunteer);
        }

        public MSCitizen GetTarget(Vector3 position, int range)
        {
            foreach (MSUnit unit in citizens)
            {
                if (unit is MSCitizen)
                {
                    MSCitizen citizen = unit as MSCitizen;
                    if (citizen.state == MSCitizen.State.MOB &&
                        Vector3.Distance(position, citizen.Position) <= range)
                    {
                        return unit as MSCitizen;
                    }
                }
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

                    if (rnd <= 3000 && citizen.state == MSCitizen.State.CIVILIAN && 
                        !(citizen is MSVolunteer))
                    {
                        foreach (MSUnit p in citizens)
                        {
                            if (p is MSCitizen && !(p is MSVolunteer) && (p as MSCitizen).state == MSCitizen.State.MOB)
                            {
                                if (Vector3.Distance(citizen.Position, (p as MSCitizen).Position) <= 5)
                                {
                                    citizen.Follow(p as MSCitizen);
                                    citizen.state = MSCitizen.State.MOB;
                                    citizen.changeModel("mob", "MTextures/mob");
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
                    list.Add(person as MSCitizen);
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
