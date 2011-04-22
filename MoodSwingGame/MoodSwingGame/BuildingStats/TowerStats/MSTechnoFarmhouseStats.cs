using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSTechnoFarmhouseStats : MSTowerStats
    {
        private static MSTechnoFarmhouseStats instance;
        public static MSTechnoFarmhouseStats GetInstance()
        {
            if (instance == null)
                instance = new MSTechnoFarmhouseStats();
            return instance;
        }

        private MSTechnoFarmhouseStats()
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
