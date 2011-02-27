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

        //probability constant that handles unit generation
        private const int INITIAL_BIRTH_RATE = 100;
        private const int MAX_PROBABILITY = 5000;
        //probability constant that handles mob generation
        private const int MOB_PROBABILITY = 20;
        private const int MOB_RECRUIT_RATE = 3000;
        private const int MOB_RECRUIT_DISTANCE = 5;

        //list of citizens
        private List<MSUnit> citizens;
        public List<MSUnit> Citizens { get { return citizens; } }
        private float birthRate;

        private MSUnitHandler() 
        {
            citizens = new List<MSUnit>();
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
                    MSTypes mobmdg = MSTypes.GENERAL;
                    String mobtexture = "MTextures/tao";
                    if (rnd > MOB_PROBABILITY * 7 / 8)
                    {
                        mobmdg = MSTypes.POVERTY;
                        mobtexture = "MTextures/mob_A";
                    }
                    else if (rnd > MOB_PROBABILITY * 6 / 8)
                    {
                        mobmdg = MSTypes.EDUCATION;
                        mobtexture = "MTextures/mob_B";
                    }
                    else if (rnd > MOB_PROBABILITY * 5 / 8)
                    {
                        mobmdg = MSTypes.GENDER_EQUALITY;
                        mobtexture = "MTextures/mob_C";
                    }
                    else if (rnd > MOB_PROBABILITY * 4 / 8)
                    {
                        mobmdg = MSTypes.CHILD_HEALTH;
                        mobtexture = "MTextures/mob_D";
                    }
                    else if (rnd > MOB_PROBABILITY * 3 / 8)
                    {
                        mobmdg = MSTypes.MATERNAL_HEALTH;
                        mobtexture = "MTextures/mob_E";
                    }
                    else if (rnd > MOB_PROBABILITY * 2 / 8)
                    {
                        mobmdg = MSTypes.HIV_AIDS;
                        mobtexture = "MTextures/mob_F";
                    }
                    else if (rnd > MOB_PROBABILITY * 1 / 8)
                    {
                        mobmdg = MSTypes.ENVIRONMENT;
                        mobtexture = "MTextures/mob_G";
                    }
                    else
                    {
                        mobmdg = MSTypes.GLOBAL_PARTNERSHIP;
                        mobtexture = "MTextures/mob_H";
                    }
                    person = new MSCitizen(MoodSwing.getInstance().Content.Load<Model>("mob"),
                        MoodSwing.getInstance().Content.Load<Texture2D>(mobtexture),
                        MoodSwing.getInstance().Content.Load<Effect>("Mood"),
                        (map.MapArray[(int)start.X, (int)start.Y] as MS3DComponent).Position + new Vector3(0, 0, 20),
                        map.GetPath(start, MSDistrictHall.getInstance().TileCoordinate),
                        MSCitizen.State.MOB, mobmdg);
                }
                else
                    person = new MSCitizen(MoodSwing.getInstance().Content.Load<Model>("person"),
                        MoodSwing.getInstance().Content.Load<Texture2D>("MTextures/tao"),
                        MoodSwing.getInstance().Content.Load<Effect>("Mood"),
                        (map.MapArray[(int)start.X, (int)start.Y] as MS3DComponent).Position + new Vector3(0, 0, 20),
                        path, MSCitizen.State.CIVILIAN, MSTypes.GENERAL);

                citizens.Add(person);
                return person;
            }

            return null;
        }

        public void AddVolunteer(MSVolunteer volunteer)
        {
            citizens.Add(volunteer);
        }

        public void AddWorker(MSWorker worker)
        {
            citizens.Add(worker);
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

        public List<MSCitizen> Update(MSMap map)
        {
            List<MSCitizen> list = new List<MSCitizen>();

            foreach (MSUnit person in citizens)
            {
                if (person is MSCitizen)
                {
                    MSCitizen citizen = person as MSCitizen;
                    int rnd = MSRandom.random.Next(MAX_PROBABILITY);

                    if (rnd <= MOB_RECRUIT_RATE && citizen.state == MSCitizen.State.CIVILIAN && 
                        !(citizen is MSVolunteer))
                    {
                        foreach (MSUnit p in citizens)
                        {
                            if (p is MSCitizen && !(p is MSVolunteer) && (p as MSCitizen).state == MSCitizen.State.MOB)
                            {
                                if (Vector3.Distance(citizen.Position, (p as MSCitizen).Position) <= MOB_RECRUIT_DISTANCE)
                                {
                                    citizen.Follow(p as MSCitizen);
                                    citizen.state = MSCitizen.State.MOB;
                                    citizen.MDG = (p as MSCitizen).MDG;
                                    citizen.changeModel("mob", (p as MSCitizen).MDG);
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
