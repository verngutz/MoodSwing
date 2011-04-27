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
using MoodSwingGame;
using System.IO;

namespace MoodSwingGame
{
    public class MSUnitHandler
    {
        public static bool CitizensEnabled { set; get; }

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
        private const int MAX_PROBABILITY = 10000;
        //probability constant that handles mob generation
        private const int MOB_RECRUIT_RATE = 3000;
        private const int MOB_RECRUIT_DISTANCE = 15;

        //list of citizens
        private List<MSUnit> units;
        public List<MSUnit> Units { get { return units; } }
        public bool IsLeaderBusy { get; set; }

        private MSUnitHandler()
        {
            units = new List<MSUnit>();
            IsLeaderBusy = false;
            mobTypeParam = new MSMobParam[9];
            for (int i = 0; i < 9; i++)
            {
                mobTypeParam[i] = new MSMobParam(getGoal(i));
            }
        }

        //something to remove. Make oneOnly = true if you want only one citizen to exist.
        //used for testing only

        private MSMobParam[] mobTypeParam;
        public void SetMobEnabled(MSMilleniumDevelopmentGoal mdg, bool enabled)
        {
            mobTypeParam[(int)mdg].IsEnabled = enabled;
        }

        private MSMobParam getMobParam(MSMilleniumDevelopmentGoal? mdg)
        {
            if (mdg == null) return mobTypeParam[8];
            return mobTypeParam[(int)mdg];
        }

        public string toString()
        {
            string toReturn = "";
            int volunteerCount = 0;
            foreach (MSUnit unit in this.units)
            {
                if (unit is MSVolunteeringCitizen) volunteerCount++;
            }

            toReturn += volunteerCount + "\n";
            foreach (MSMobParam mp in this.mobTypeParam)
            {
                toReturn += mp.timer + "\n";
            }
            return toReturn;
        }

        public void load(StreamReader sr, MSMap map)
        {
            int volunteerCount = Int32.Parse(sr.ReadLine());
            for (int i = 0; i < 0; i++)
            {
                this.VolunteerCitizen(map);
            }
            foreach (MSMobParam mp in this.mobTypeParam)
            {
                mp.timer = Int32.Parse(sr.ReadLine());
            }
        }
        private MSMilleniumDevelopmentGoal? getGoal(int index)
        {
            switch (index)
            {
                case 0:
                    return MSMilleniumDevelopmentGoal.CHILD_HEALTH;
                case 1:
                    return MSMilleniumDevelopmentGoal.EDUCATION;
                case 2:
                    return MSMilleniumDevelopmentGoal.ENVIRONMENT;
                case 3:
                    return MSMilleniumDevelopmentGoal.GENDER_EQUALITY;
                case 4:
                    return MSMilleniumDevelopmentGoal.GLOBAL_PARTNERSHIP;
                case 5:
                    return MSMilleniumDevelopmentGoal.HIV_AIDS;
                case 6:
                    return MSMilleniumDevelopmentGoal.MATERNAL_HEALTH;
                case 7:
                    return MSMilleniumDevelopmentGoal.POVERTY;
                default:
                    return null;
            }
        }


        public void TryForBaby(MSMap map)
        {
            List<MSUnit> mobbers = new List<MSUnit>();

            for (int i = 0; i < 9; i++)
            {
                MSMobParam mp = this.mobTypeParam[i];
                int rnd = MSRandom.random.Next(100);
                if (rnd < mp.getProbability())
                {
                    MSUnit person;

                    MSUnchangeableBuilding source = map.GetRandomCitizenSource();

                    MSUnchangeableBuilding sink;
                    do
                    {
                        sink = map.GetRandomCitizenSource();
                    } while (source == sink);

                    Vector2 start = new Vector2(source.Row, source.Column);
                    Vector2 end = new Vector2(sink.Row, sink.Column);

                    MSMilleniumDevelopmentGoal? mdg = this.getGoal(i);

                    if (mdg != null)
                    {
                        MSMilleniumDevelopmentGoal mobmdg = (MSMilleniumDevelopmentGoal)mdg;
                        person = new MSMobber(
                                map.MapArray[(int)start.X, (int)start.Y].Position + MSUnit.UNITZ_POSITION,
                                map.GetPath(start, MSDistrictHall.getInstance().TileCoordinate), map, mobmdg, 0);
                    }
                    else
                    {
                        Node path = map.GetPath(start, end);
                        person = new MSCitizen(
                            map.MapArray[(int)start.X, (int)start.Y].Position + MSUnit.UNITZ_POSITION,
                            path, map, true, 0);
                    }

                    if (person != null)
                    {
                        if (mdg != null) units.Add(person);
                        else mobbers.Add(person);
                    }
                }
            }

            if( mobbers.Count > 0 ) 
                units.Add(mobbers.ElementAt<MSUnit>(MSRandom.random.Next(mobbers.Count)));
        }

        public void AddUnit(MSUnit unit)
        {
            units.Add(unit);
        }

        public void SendVolunteer(MSMap map, MSUnit unit, MSTower office)
        {
            Node path1 = map.GetPath(new Vector2(office.Row, office.Column), unit.TileCoordinate);

            Node pathEnd = path1;
            while (pathEnd.next != null)
            {
                pathEnd = pathEnd.next;
            }
            pathEnd.next = unit.Path;

            MSVolunteer volunteer = new MSVolunteer
            (
                office.Position + MSUnit.UNITZ_POSITION,
                path1,
                unit,
                office,
                map,
                0
            );

            MSUnitHandler.GetInstance().AddUnit(volunteer);
        }

        public void SendWorkers(MSMap map, MSChangeableBuilding bldg, int qty)
        {
            MSVolunteerCenter center = map.GetNearestVolunteerCenter(bldg);
            Node path = map.GetPath(center.TileCoordinate, bldg.TileCoordinate);

            for (int i = 0; i < qty; i++)
            {
                MSWorker worker = new MSWorker(center.Position + MSUnit.UNITZ_POSITION, path, bldg, map, 0);
                units.Add(worker);
            }
        }
        public void VolunteerCitizen(MSMap map)
        {
            MS3DTile bldg = map.GetRandomCitizenSource();
            MS3DTile center = map.GetNearestVolunteerCenter(bldg);
            Node path = map.GetPath(bldg.TileCoordinate, center.TileCoordinate);
            MSVolunteeringCitizen v = new MSVolunteeringCitizen(bldg.Position + MSUnit.UNITZ_POSITION, path, map, 0);
            units.Add(v);
        }

        public void Update(MSMap map)
        {
            List<MSUnit> toRemove = new List<MSUnit>();
            foreach (MSMobParam mp in this.mobTypeParam)
            {
                mp.incrTimer();
            }
            for (int i = 0; i < units.Count; i++)
            {
                MSUnit unit = units[i];
                if (!unit.DestinationReached)
                    unit.Walk(map.MapArray, units);

                else
                    toRemove.Add(unit);
            }

            foreach (MSUnit person in toRemove)
            {
                units.Remove(person);
                person.Dispose();
            }

            for (int i = 0; i < units.Count; i++)
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
                                units[i] = new MSMobber(units[i].Position, p.Path, map, (p as MSMobber).Concern, p.Rotation);
                                units[i].Follow(p);
                                break;

                            }
                        }
                    }
                }
            }
        }
    }

    class MSMobParam
    {
        public int timer;
        public bool IsPaused { set; get; }
        public bool IsEnabled { set; get; }
        MSParametricCurve equation;

        public MSMobParam(MSMilleniumDevelopmentGoal? mdg)
        {
            timer = 0;
            IsPaused = false;
            IsEnabled = true;

            switch (mdg)
            {
                case MSMilleniumDevelopmentGoal.CHILD_HEALTH:
                    equation = new ChildHealthCurve();
                    break;
                case MSMilleniumDevelopmentGoal.EDUCATION:
                    equation = new EducationCurve();
                    break;
                case MSMilleniumDevelopmentGoal.ENVIRONMENT:
                    equation = new EnvironmentCurve();
                    break;
                case MSMilleniumDevelopmentGoal.GENDER_EQUALITY:
                    equation = new GenderEqualityCurve();
                    break;
                case MSMilleniumDevelopmentGoal.GLOBAL_PARTNERSHIP:
                    equation = new GlobalPartnershipCurve();
                    break;
                case MSMilleniumDevelopmentGoal.HIV_AIDS:
                    equation = new HivAidsCurve();
                    break;
                case MSMilleniumDevelopmentGoal.MATERNAL_HEALTH:
                    equation = new MaternalHealthCurve();
                    break;
                case MSMilleniumDevelopmentGoal.POVERTY:
                    equation = new PovertyCurve();
                    break;
                default:
                    equation = new NormalCitizenCurve();
                    break;
            }

        }

        public void incrTimer()
        {
            if (!IsPaused && timer + 1 <= Int32.MaxValue) timer++;
        }

        public int getProbability()
        {
            if (!IsEnabled) return 0;
            return equation.X(timer/60);
        }
    }
}
