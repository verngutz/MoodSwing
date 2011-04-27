using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSGeneralRefugeStats : MSTowerStats
    {
        private static MSGeneralRefugeStats instance;
        public static MSGeneralRefugeStats GetInstance()
        {
            if (instance == null)
                instance = new MSGeneralRefugeStats();
            return instance;
        }
        private MSGeneralRefugeStats()
        {
            povertyEffectiveness = true;
            educationEffectiveness = true;
            genderEqualityEffectiveness = true;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
        }

        public override int GetFundsCost()
        {
            return 200;
        }

        public override int GetVolunteerCost()
        {
            return 2;
        }
    }
}
