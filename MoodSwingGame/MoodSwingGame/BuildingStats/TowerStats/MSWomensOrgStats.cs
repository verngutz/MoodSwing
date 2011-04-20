using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
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
}
