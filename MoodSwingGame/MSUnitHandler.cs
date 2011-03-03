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

        public static MSUnitHandler GetInstance()
        {
            if (unitHandler == null)
                unitHandler = new MSUnitHandler();
            return unitHandler;
        }

        public static MSUnitHandler Restart()
        {
            unitHandler = new MSUnitHandler();
            return unitHandler;
        }
        //probability constant that handles unit generation
        private const int INITIAL_BIRTH_RATE = 100;
        private const int MAX_PROBABILITY = 5000;
        //probability constant that handles mob generation
        private const int MOB_PROBABILITY = 5;
        private const int MOB_RECRUIT_RATE = 3000;
        private const int MOB_RECRUIT_DISTANCE = 5;

        //list of citizens
        private List<MSUnit> units;
        public List<MSUnit> Units { get { return units; } }
        public bool IsLeaderBusy { get; set; }
        private float birthRate;

        private MSUnitHandler() 
        {
            units = new List<MSUnit>();
            IsLeaderBusy = false;
            birthRate = INITIAL_BIRTH_RATE;
        }

        //something to remove. Make oneOnly = true if you want only one citizen to exist.
        //used for testing only
        private bool oneOnly = false;
        private bool checkOne = false;
        public MSUnit TryForBaby( MSMap map )
        {
            int rnd = MSRandom.random.Next(MAX_PROBABILITY);

            if (oneOnly && checkOne)
                return null;
            if (rnd < birthRate)
            {
                if (birthRate < MAX_PROBABILITY)
                    birthRate += 0.01f;

                checkOne = true;
                MSUnit person;

                MSUnbuyableBuilding source = map.GetRandomCitizenSource();
                Vector2 start = new Vector2(source.Row, source.Column);

                MSUnbuyableBuilding sink;
                do
                {
                    sink = map.GetRandomCitizenSource();
                } while (source == sink);
                Vector2 end = new Vector2(sink.Row, sink.Column);
                
                Node path = map.GetPath(start, end);

                if (rnd < MOB_PROBABILITY)
                {
                    MSMilleniumDevelopmentGoal mobmdg;
                    if (rnd > MOB_PROBABILITY * 7 / 8)
                        mobmdg = MSMilleniumDevelopmentGoal.POVERTY;

                    else if (rnd > MOB_PROBABILITY * 6 / 8)
                        mobmdg = MSMilleniumDevelopmentGoal.EDUCATION;

                    else if (rnd > MOB_PROBABILITY * 5 / 8)
                        mobmdg = MSMilleniumDevelopmentGoal.GENDER_EQUALITY;

                    else if (rnd > MOB_PROBABILITY * 4 / 8)
                        mobmdg = MSMilleniumDevelopmentGoal.CHILD_HEALTH;

                    else if (rnd > MOB_PROBABILITY * 3 / 8)
                        mobmdg = MSMilleniumDevelopmentGoal.MATERNAL_HEALTH;

                    else if (rnd > MOB_PROBABILITY * 2 / 8)
                        mobmdg = MSMilleniumDevelopmentGoal.HIV_AIDS;

                    else if (rnd > MOB_PROBABILITY * 1 / 8)
                        mobmdg = MSMilleniumDevelopmentGoal.ENVIRONMENT;

                    else
                        mobmdg = MSMilleniumDevelopmentGoal.GLOBAL_PARTNERSHIP;

                    person = new MSMobber(
                        map.MapArray[(int)start.X, (int)start.Y].Position + MSUnit.UNITZ_POSITION,
                        map.GetPath(start, MSDistrictHall.getInstance().TileCoordinate), map, mobmdg);
                }
                else
                    person = new MSCitizen(
                        map.MapArray[(int)start.X, (int)start.Y].Position + MSUnit.UNITZ_POSITION,
                        path, map, true);

                units.Add(person);
                return person;
            }

            return null;
        }

        public void AddUnit(MSUnit unit)
        {
            units.Add(unit);
        }

        public void VolunteerRandomCitizen( MSMap map )
        {
            List<MSCitizen> toGet = new List<MSCitizen>();
            foreach (MSUnit unit in units)
                if (unit is MSCitizen && unit.IsMobbable)
                    toGet.Add(unit as MSCitizen);
            
            if (toGet.Count == 0) return;
            else
            {
                MSCitizen cit = toGet.ElementAt<MSCitizen>(MSRandom.random.Next(toGet.Count));
                MSVolunteerCenter center = map.GetNearestVolunteerCenter(cit.TileCoordinate);
                Node path = map.GetPath(cit.TileCoordinate, center.TileCoordinate);
                MSVolunteeringCitizen v = new MSVolunteeringCitizen(cit.Position, path, map);
                units.Remove(cit);
                units.Add(v);
            }
        }

        public void Update(MSMap map)
        {
            List<MSUnit> toRemove = new List<MSUnit>();

            foreach (MSUnit unit in units)
            {
                if (!unit.DestinationReached)
                    unit.Walk(map.MapArray);

                else
                    toRemove.Add(unit);
            }

            foreach (MSUnit person in toRemove)
            {
                units.Remove(person);
                person.Dispose();
            }

            for (int i = 0; i < units.Count; i++ )
            {
                int rnd = MSRandom.random.Next(MAX_PROBABILITY);

                if (rnd <= MOB_RECRUIT_RATE && units[i].IsMobbable)
                {
                    foreach (MSUnit p in units)
                    {
                        if (p is MSMobber)
                        {
                            if (Vector3.Distance(units[i].Position, p.Position) <= MOB_RECRUIT_DISTANCE)
                            {
                                units[i] = new MSMobber(units[i].Position, p.Path, map, (p as MSMobber).Concern);
                                units[i].Follow(p);
                                break;

                            }
                        }
                    }
                }
            }
        }
    }
}
