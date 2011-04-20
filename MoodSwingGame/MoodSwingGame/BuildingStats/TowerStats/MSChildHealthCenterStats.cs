using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
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
}
