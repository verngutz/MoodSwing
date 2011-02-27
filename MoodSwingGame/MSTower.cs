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

        public MSTower( Model model, Texture2D texture, Effect effect, Vector3 position, int row, int column, MSTowerStats stats)
            : base(model, texture, effect, position, row, column)
        {
            this.stats = stats;
            capacity = stats.GetCapacity();
        }

        public MSVolunteer sentinel( MSMap map )
        {
            if (capacity > 0)
            {
                MSCitizen target = MSUnitHandler.GetInstance().GetTarget(Position, stats.GetRange());
                if (target != null)
                {
                    if (stats.GetEffectiveness(target.MDG) > MSRandom.random.Next(100))
                    {
                        capacity--;
                        target.state = MSCitizen.CitizenState.WAITING;

                        Node path1 = map.GetPath(new Vector2(Row, Column), target.TileCoordinate);
                        Node path2 = map.GetPath(target.TileCoordinate, new Vector2(Row, Column)).next;

                        MSVolunteer volunteer = new MSVolunteer(Game.Content.Load<Model>("person"),
                            Game.Content.Load<Texture2D>("MTextures/recruiter"),
                            Game.Content.Load<Effect>("Mood"),
                            Position + new Vector3(0, 0, 20), path1, path2, target as MSCitizen, this);
                        MSUnitHandler.GetInstance().AddVolunteer(volunteer);
                        MSMoodManager.GetInstance().takeHealth();
                        return volunteer;
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

        public int GetEffectiveness(MSTypes type)
        {
            switch (type)
            {
                case MSTypes.POVERTY:
                    return povertyEffectiveness;
                case MSTypes.EDUCATION:
                    return educationEffectiveness;
                case MSTypes.GENDER_EQUALITY:
                    return genderEqualityEffectiveness;
                case MSTypes.CHILD_HEALTH:
                    return childHealthEffectiveness;
                case MSTypes.MATERNAL_HEALTH:
                    return maternalHealthEffectiveness;
                case MSTypes.HIV_AIDS:
                    return hivAidsEffectiveness;
                case MSTypes.ENVIRONMENT:
                    return environmentEffectiveness;
                case MSTypes.GLOBAL_PARTNERSHIP:
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
            return 5;
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
            return 5;
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
            return 5;
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
            return 5;
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
            return 5;
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
            return 5;
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
