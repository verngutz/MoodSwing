using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
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
}
