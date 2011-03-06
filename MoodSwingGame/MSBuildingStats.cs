using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public interface MSBuildingStats
    {
        int GetVolunteerCost();
        int GetFundsCost();
    }

    public class MSVolunteerCenterStats : MSBuildingStats
    {
        private MSVolunteerCenterStats() { }
        private static MSVolunteerCenterStats instance;
        public static MSVolunteerCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSVolunteerCenterStats();
            return instance;
        }

        public int GetVolunteerCost() { return 0; }
        public int GetFundsCost() { return 250; }
    }

    public class MSFundraiserStats : MSBuildingStats
    {
        private MSFundraiserStats() { }
        private static MSFundraiserStats instance;
        public static MSFundraiserStats GetInstance() 
        {
            if (instance == null)
                instance = new MSFundraiserStats();
            return instance; 
        }

        public int GetVolunteerCost() { return 2; }
        public int GetFundsCost() { return 100; }
    }

    public abstract class MSTowerStats : MSBuildingStats
    {
        protected int povertyEffectiveness;
        protected int educationEffectiveness;
        protected int genderEqualityEffectiveness;
        protected int childHealthEffectiveness;
        protected int maternalHealthEffectiveness;
        protected int hivAidsEffectiveness;
        protected int environmentEffectiveness;
        protected int globalEffectiveness;

        public virtual int GetVolunteerCost() { return 4; }
        public virtual int GetFundsCost() { return 200; }
        public virtual int GetRange() { return 2; }

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

        private MSFoodCenterStats()
        {
            povertyEffectiveness = 100;
            educationEffectiveness = 0;
            genderEqualityEffectiveness = 0;
            childHealthEffectiveness = 0;
            maternalHealthEffectiveness = 0;
            hivAidsEffectiveness = 0;
            environmentEffectiveness = 0;
            globalEffectiveness = 0;
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

        private MSTutorialCenterStats()
        {
            povertyEffectiveness = 0;
            educationEffectiveness = 100;
            genderEqualityEffectiveness = 0;
            childHealthEffectiveness = 0;
            maternalHealthEffectiveness = 0;
            hivAidsEffectiveness = 0;
            environmentEffectiveness = 0;
            globalEffectiveness = 0;
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

        private MSWomensOrgStats()
        {
            povertyEffectiveness = 0;
            educationEffectiveness = 0;
            genderEqualityEffectiveness = 100;
            childHealthEffectiveness = 0;
            maternalHealthEffectiveness = 0;
            hivAidsEffectiveness = 0;
            environmentEffectiveness = 0;
            globalEffectiveness = 0;
        }
    }

    public class MSChildHealthCenterStats : MSTowerStats
    {
        private static MSChildHealthCenterStats instance;
        public static MSChildHealthCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSChildHealthCenterStats();
            return instance;
        }

        private MSChildHealthCenterStats()
        {
            povertyEffectiveness = 0;
            educationEffectiveness = 0;
            genderEqualityEffectiveness = 0;
            childHealthEffectiveness = 100;
            maternalHealthEffectiveness = 0;
            hivAidsEffectiveness = 0;
            environmentEffectiveness = 0;
            globalEffectiveness = 0;
        }
    }

    public class MSMaternalHealthCenterStats : MSTowerStats
    {
        private static MSMaternalHealthCenterStats instance;
        public static MSMaternalHealthCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSMaternalHealthCenterStats();
            return instance;
        }

        private MSMaternalHealthCenterStats()
        {
            povertyEffectiveness = 0;
            educationEffectiveness = 0;
            genderEqualityEffectiveness = 0;
            childHealthEffectiveness = 0;
            maternalHealthEffectiveness = 100;
            hivAidsEffectiveness = 0;
            environmentEffectiveness = 0;
            globalEffectiveness = 0;
        }
    }

    public class MSHivAidsHealthCenterStats : MSTowerStats
    {
        private static MSHivAidsHealthCenterStats instance;
        public static MSHivAidsHealthCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSHivAidsHealthCenterStats();
            return instance;
        }

        private MSHivAidsHealthCenterStats()
        {
            povertyEffectiveness = 0;
            educationEffectiveness = 0;
            genderEqualityEffectiveness = 0;
            childHealthEffectiveness = 0;
            maternalHealthEffectiveness = 0;
            hivAidsEffectiveness = 100;
            environmentEffectiveness = 0;
            globalEffectiveness = 0;
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

        private MSEcoParkStats()
        {
            povertyEffectiveness = 0;
            educationEffectiveness = 0;
            genderEqualityEffectiveness = 0;
            childHealthEffectiveness = 0;
            maternalHealthEffectiveness = 0;
            hivAidsEffectiveness = 0;
            environmentEffectiveness = 100;
            globalEffectiveness = 0;
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
