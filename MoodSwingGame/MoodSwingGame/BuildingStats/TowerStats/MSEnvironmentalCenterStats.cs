using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSEnvironmentalCenterStats : MSTowerStats
    {
        private static MSEnvironmentalCenterStats instance;
        public static MSEnvironmentalCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSEnvironmentalCenterStats();
            return instance;
        }
        private MSEnvironmentalCenterStats()
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
