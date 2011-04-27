using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSEFASchoolStats : MSTowerStats
    {
        private static MSEFASchoolStats instance;
        public static MSEFASchoolStats GetInstance()
        {
            if (instance == null)
                instance = new MSEFASchoolStats();
            return instance;
        }
        public virtual float GetBuildTime() { return 5; }
        private MSEFASchoolStats()
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
}
