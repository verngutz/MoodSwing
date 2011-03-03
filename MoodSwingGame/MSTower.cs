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
        private MSTowerStats stats;
        private int capacity;

        public MSTower( Model model, Texture2D texture, Effect effect, Vector3 position, float rotation, int row, int column, MSTowerStats stats)
            : base(model, texture, effect, position, rotation, row, column, MSMap.tallheight)
        {
            this.stats = stats;
            capacity = stats.GetCapacity();
        }

        public MSVolunteer sentinel(MSMap map, MSUnitHandler unitHandler)
        {
            if (capacity > 0)
            {
                for (int i = 0; i < unitHandler.Units.Count; i++)
                {
                    Vector2 position1 = new Vector2
                    (
                        position.X + MSMap.tileDimension / 2,
                        position.Y + MSMap.tileDimension / 2
                    );

                    Vector2 position2 = new Vector2
                    (
                        unitHandler.Units[i].Position.X, 
                        unitHandler.Units[i].Position.Y
                    );
                    
                    if (unitHandler.Units[i] is MSMobber)
                    {
                        Vector2 tileCoords = unitHandler.Units[i].TileCoordinate;
                        MS3DTile tile = (unitHandler.Units[i] as MSMobber).Map.MapArray[(int)tileCoords.X, (int)tileCoords.Y];
                        if (tile is MSRoad &&
                            Vector2.Distance(position1, position2) <= stats.GetRange())
                        {
                            MSMilleniumDevelopmentGoal goal = (unitHandler.Units[i] as MSMobber).Concern;
                            if (stats.GetEffectiveness(goal) > MSRandom.random.Next(100))
                            {
                                capacity--;

                                unitHandler.Units[i] = new MSCitizen
                                (
                                    unitHandler.Units[i].Position,
                                    unitHandler.Units[i].Path,
                                    unitHandler.Units[i].Map,
                                    false
                                );

                                unitHandler.Units[i].IsStopped = true;

                                Node path1 = map.GetPath(new Vector2(Row, Column), unitHandler.Units[i].TileCoordinate);
                                Node path2 = map.GetPath(unitHandler.Units[i].TileCoordinate, new Vector2(Row, Column)).next;

                                MSVolunteer volunteer = new MSVolunteer
                                (
                                    Position + new Vector3(0, 0, 20),
                                    path1,
                                    path2,
                                    unitHandler.Units[i],
                                    this,
                                    map
                                );

                                MSUnitHandler.GetInstance().AddUnit(volunteer);
                                MSMoodManager.GetInstance().TakeHealth();
                                MSMoodManager.GetInstance().AddMDGScore(goal);
                                return volunteer;
                            }
                        }
                        
                    }
                }
            }
            return null;
        }

        public void VolunteerReturned()
        {
            capacity++;
        }
    }

    public abstract class MSTowerStats
    {
        protected int povertyEffectiveness;
        protected int educationEffectiveness;
        protected int genderEqualityEffectiveness;
        protected int childHealthEffectiveness;
        protected int maternalHealthEffectiveness;
        protected int hivAidsEffectiveness;
        protected int environmentEffectiveness;
        protected int globalEffectiveness;

        public abstract int GetCapacity();
        public abstract int GetRange();

        public int GetEffectiveness(MSMilleniumDevelopmentGoal type)
        {
            switch (type)
            {
                case MSMilleniumDevelopmentGoal.POVERTY:
                    return povertyEffectiveness;
                case MSMilleniumDevelopmentGoal.EDUCATION:
                    return educationEffectiveness;
                case MSMilleniumDevelopmentGoal.GENDER_EQUALITY:
                    return genderEqualityEffectiveness;
                case MSMilleniumDevelopmentGoal.CHILD_HEALTH:
                    return childHealthEffectiveness;
                case MSMilleniumDevelopmentGoal.MATERNAL_HEALTH:
                    return maternalHealthEffectiveness;
                case MSMilleniumDevelopmentGoal.HIV_AIDS:
                    return hivAidsEffectiveness;
                case MSMilleniumDevelopmentGoal.ENVIRONMENT:
                    return environmentEffectiveness;
                case MSMilleniumDevelopmentGoal.GLOBAL_PARTNERSHIP:
                    return globalEffectiveness;
            }
            throw new ArgumentException();
        }
    }

    public class MSFoodCenterStats : MSTowerStats
    {
        private static MSFoodCenterStats instance;
        public static MSFoodCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSFoodCenterStats();
            return instance;
        }

        public override int GetCapacity()
        {
            return 4;
        }

        public override int GetRange()
        {
            return (3*MSMap.tileDimension)/2;
        }

        private MSFoodCenterStats()
        {
            povertyEffectiveness = 100;
            educationEffectiveness = 1;
            genderEqualityEffectiveness = 1;
            childHealthEffectiveness = 1;
            maternalHealthEffectiveness = 1;
            hivAidsEffectiveness = 1;
            environmentEffectiveness = 1;
            globalEffectiveness = 1;
        }
    }

    public class MSTutorialCenterStats : MSTowerStats
    {
        private static MSTutorialCenterStats instance;
        public static MSTutorialCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSTutorialCenterStats();
            return instance;
        }

        public override int GetCapacity()
        {
            return 4;
        }

        public override int GetRange()
        {
            return (3 * MSMap.tileDimension) / 2;
        }

        private MSTutorialCenterStats()
        {
            povertyEffectiveness = 1;
            educationEffectiveness = 100;
            genderEqualityEffectiveness = 1;
            childHealthEffectiveness = 1;
            maternalHealthEffectiveness = 1;
            hivAidsEffectiveness = 1;
            environmentEffectiveness = 1;
            globalEffectiveness = 1;
        }
    }

    public class MSWomensOrgStats : MSTowerStats
    {
        private static MSWomensOrgStats instance;
        public static MSWomensOrgStats GetInstance()
        {
            if (instance == null)
                instance = new MSWomensOrgStats();
            return instance;
        }

        public override int GetCapacity()
        {
            return 4;
        }

        public override int GetRange()
        {
            return (3 * MSMap.tileDimension) / 2;
        }

        private MSWomensOrgStats()
        {
            povertyEffectiveness = 1;
            educationEffectiveness = 1;
            genderEqualityEffectiveness = 100;
            childHealthEffectiveness = 1;
            maternalHealthEffectiveness = 1;
            hivAidsEffectiveness = 1;
            environmentEffectiveness = 1;
            globalEffectiveness = 1;
        }
    }

    public class MSHealthCenterStats : MSTowerStats
    {
        private static MSHealthCenterStats instance;
        public static MSHealthCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSHealthCenterStats();
            return instance;
        }

        public override int GetCapacity()
        {
            return 2;
        }

        public override int GetRange()
        {
            return (3 * MSMap.tileDimension) / 2;
        }

        private MSHealthCenterStats()
        {
            povertyEffectiveness = 1;
            educationEffectiveness = 1;
            genderEqualityEffectiveness = 1;
            childHealthEffectiveness = 100;
            maternalHealthEffectiveness = 100;
            hivAidsEffectiveness = 100;
            environmentEffectiveness = 1;
            globalEffectiveness = 1;
        }
    }

    public class MSEcoParkStats : MSTowerStats
    {
        private static MSEcoParkStats instance;
        public static MSEcoParkStats GetInstance()
        {
            if (instance == null)
                instance = new MSEcoParkStats();
            return instance;
        }

        public override int GetCapacity()
        {
            return 4;
        }

        public override int GetRange()
        {
            return (3 * MSMap.tileDimension) / 2;
        }

        private MSEcoParkStats()
        {
            povertyEffectiveness = 1;
            educationEffectiveness = 1;
            genderEqualityEffectiveness = 1;
            childHealthEffectiveness = 1;
            maternalHealthEffectiveness = 1;
            hivAidsEffectiveness = 1;
            environmentEffectiveness = 100;
            globalEffectiveness = 1;
        }
    }

    public class MSGlobalCenterStats : MSTowerStats
    {
        private static MSGlobalCenterStats instance;
        public static MSGlobalCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSGlobalCenterStats();
            return instance;
        }

        public override int GetCapacity()
        {
            return 4;
        }

        public override int GetRange()
        {
            return (3 * MSMap.tileDimension) / 2;
        }

        private MSGlobalCenterStats()
        {
            povertyEffectiveness = 1;
            educationEffectiveness = 1;
            genderEqualityEffectiveness = 1;
            childHealthEffectiveness = 1;
            maternalHealthEffectiveness = 1;
            hivAidsEffectiveness = 1;
            environmentEffectiveness = 1;
            globalEffectiveness = 100;
        }
    }
}
