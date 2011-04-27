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
        public override int GetFundsCost()
        {
            return 450;
        }

        public override int GetVolunteerCost()
        {
            return 4;
        }
    }
}
