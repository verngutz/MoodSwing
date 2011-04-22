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
            unitHandler.MOB_WAVE_PROBABILITY = 7;
            unitHandler.MOB_STABLE_PROBABILITY = 0;
            unitHandler.MOB_MDG_OPTIONS = 1;
            return unitHandler;
        }
        //probability constant that handles unit generation
        private const int INITIAL_BIRTH_RATE = 100;
        private const int MAX_PROBABILITY = 10000;
        private const int MAX_MOB_PROBABILITY = 100;
        //probability constant that handles mob generation
        private int MOB_WAVE_PROBABILITY = 7;
        private int MOB_STABLE_PROBABILITY = 0;
        private const int MOB_RECRUIT_RATE = 3000;
        private const int MOB_RECRUIT_DISTANCE = 15;
        //probability of kind of mdg in mob
        private int MOB_MDG_OPTIONS = 1;
        private const int MOB_MDG_DELAY = 4;

        //list of citizens
        private List<MSUnit> units;
        public List<MSUnit> Units { get { return units; } }
        public bool IsLeaderBusy { get; set; }
        private float birthRate;

        private MSUnitHandler() 
        {
            units = new List<MSUnit>();
            IsLeaderBusy = false;
            prevCheckpoint = 0 ;
            IsRelativelyPeaceful = true;
            birthRate = INITIAL_BIRTH_RATE;
        }

        //something to remove. Make oneOnly = true if you want only one citizen to exist.
        //used for testing only
        private bool oneOnly = false;
        private bool checkOne = false;

        private int prevCheckpoint;
        private bool IsRelativelyPeaceful;

        public MSUnit TryForBaby( MSMap map, int gameTime )
        {

            int timeDiff = gameTime - prevCheckpoint;
            if ( (timeDiff >= 30 &&  !IsRelativelyPeaceful) ||
                 (timeDiff >= 20 && IsRelativelyPeaceful) )
            {
                IsRelativelyPeaceful = !IsRelativelyPeaceful;
                prevCheckpoint = gameTime;
                if (IsRelativelyPeaceful &&
                    MOB_WAVE_PROBABILITY + (MOB_WAVE_PROBABILITY+"").Length <= MAX_MOB_PROBABILITY)
                {
                    if (birthRate < MAX_PROBABILITY)  birthRate += 0.05f;
                    MOB_WAVE_PROBABILITY += (MOB_WAVE_PROBABILITY + "").Length;
                }
                MOB_MDG_OPTIONS++;

            }

            int mob_probability = MOB_WAVE_PROBABILITY ;
            if (IsRelativelyPeaceful) mob_probability = MOB_STABLE_PROBABILITY;

            int rnd = MSRandom.random.Next(MAX_PROBABILITY);

            if (oneOnly && checkOne)
                return null;
            if (rnd < birthRate)
            {
                if (birthRate < MAX_PROBABILITY)
                    birthRate += 0.01f;

                checkOne = true;
                MSUnit person;

                MSUnchangeableBuilding source = map.GetRandomCitizenSource();
                Vector2 start = new Vector2(source.Row, source.Column);

                MSUnchangeableBuilding sink;
                do
                {
                    sink = map.GetRandomCitizenSource();
                } while (source == sink);
                Vector2 end = new Vector2(sink.Row, sink.Column);
                
                Node path = map.GetPath(start, end);

                if (rnd < mob_probability)
                {
                    int typeRnd = ((MSRandom.random.Next(MOB_MDG_OPTIONS))/MOB_MDG_DELAY)%8;
                    MSMilleniumDevelopmentGoal mobmdg;
                    if (typeRnd == 0)
                        mobmdg = MSMilleniumDevelopmentGoal.POVERTY;
                    else if (typeRnd == 1)
                        mobmdg = MSMilleniumDevelopmentGoal.EDUCATION;
                    else if (typeRnd == 2)
                        mobmdg = MSMilleniumDevelopmentGoal.GENDER_EQUALITY;
                    else if (typeRnd == 3)
                        mobmdg = MSMilleniumDevelopmentGoal.CHILD_HEALTH;
                    else if (typeRnd == 4)
                        mobmdg = MSMilleniumDevelopmentGoal.MATERNAL_HEALTH;
                    else if (typeRnd == 5)
                        mobmdg = MSMilleniumDevelopmentGoal.HIV_AIDS;
                    else if (typeRnd == 6)
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

        public void SendVolunteer(MSMap map, MSUnit unit, MSTower bldg )
        {
            Node path1 = map.GetPath(new Vector2(bldg.Row, bldg.Column), unit.TileCoordinate);
            Node path2 = map.GetPath(unit.TileCoordinate, new Vector2(bldg.Row, bldg.Column)).next;

            MSVolunteer volunteer = new MSVolunteer
            (
                bldg.Position + MSUnit.UNITZ_POSITION,
                path1,
                path2,
                unit,
                bldg,
                map
            );

            MSUnitHandler.GetInstance().AddUnit(volunteer);
        }

        public void SendWorkers( MSMap map, MSChangeableBuilding bldg, int qty)
        {
            MSVolunteerCenter center = map.GetNearestVolunteerCenter(bldg);
            Node path = map.GetPath(center.TileCoordinate, bldg.TileCoordinate);

            for (int i = 0; i < qty; i++)
            {
                MSWorker worker = new MSWorker(center.Position + MSUnit.UNITZ_POSITION, path, bldg, map);
                units.Add(worker);
            }
        }
        public void VolunteerCitizen( MSMap map )
        {
            MS3DTile bldg = map.GetRandomCitizenSource();
            MS3DTile center = map.GetNearestVolunteerCenter(bldg);
            Node path = map.GetPath(bldg.TileCoordinate, center.TileCoordinate);
            MSVolunteeringCitizen v = new MSVolunteeringCitizen(bldg.Position + MSUnit.UNITZ_POSITION, path, map);
            units.Add(v);
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
