using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSWomensHealthCenterStats : MSTowerStats
    {
        private static MSWomensHealthCenterStats instance;
        public static MSWomensHealthCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSWomensHealthCenterStats();
            return instance;
        }

        private MSWomensHealthCenterStats()
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
        public override int GetFundsCost()
        {
            return 250;
        }

        public override int GetVolunteerCost()
        {
            return 2;
        }
    }
}
