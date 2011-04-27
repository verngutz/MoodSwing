using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSWomenCenterStats : MSTowerStats
    {
        private static MSWomenCenterStats instance;
        public static MSWomenCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSWomenCenterStats();
            return instance;
        }

        private MSWomenCenterStats()
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
            return 300;
        }

        public override int GetVolunteerCost()
        {
            return 3;
        }
    }
}
