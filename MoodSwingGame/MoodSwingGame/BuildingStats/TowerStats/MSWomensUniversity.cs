using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSWomensUniversityStats : MSUltiBuildingStat
    {
        private static MSWomensUniversityStats instance;
        public static MSWomensUniversityStats GetInstance()
        {
            if (instance == null)
                instance = new MSWomensUniversityStats();
            return instance;
        }
        public virtual float GetBuildTime() { return 30; }
        private MSWomensUniversityStats()
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
}
