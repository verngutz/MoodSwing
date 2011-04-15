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
        protected bool povertyEffectiveness;
        protected bool educationEffectiveness;
        protected bool genderEqualityEffectiveness;
        protected bool childHealthEffectiveness;
        protected bool maternalHealthEffectiveness;
        protected bool hivAidsEffectiveness;
        protected bool environmentEffectiveness;
        protected bool globalEffectiveness;

        public virtual int GetVolunteerCost() { return 4; }
        public virtual int GetFundsCost() { return 200; }
        public virtual int GetRange() { return 2; }

        public bool GetEffectiveness(MSMilleniumDevelopmentGoal type)
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

    public class MSGeneralHelpCenterStats : MSTowerStats
    {
        private static MSGeneralHelpCenterStats instance;
        public static MSGeneralHelpCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSGeneralHelpCenterStats();
            return instance;
        }

        private MSGeneralHelpCenterStats()
        {
            povertyEffectiveness = true;
            educationEffectiveness = true;
            genderEqualityEffectiveness = true;
            childHealthEffectiveness = true;
            maternalHealthEffectiveness = true;
            hivAidsEffectiveness = true;
            environmentEffectiveness = true;
            globalEffectiveness = true;
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
            povertyEffectiveness = true;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
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
            povertyEffectiveness = false;
            educationEffectiveness = true;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
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
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = true;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
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
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = true;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
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
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = true;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
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
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = true;
            environmentEffectiveness = false;
            globalEffectiveness = false;
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
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = true;
            globalEffectiveness = false;
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
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = true;
        }
    }
}
