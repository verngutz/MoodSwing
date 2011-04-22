using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSWorldWithoutBoundariesStats : MSTowerStats
    {
        private static MSWorldWithoutBoundariesStats instance;
        public static MSWorldWithoutBoundariesStats GetInstance()
        {
            if (instance == null)
                instance = new MSWorldWithoutBoundariesStats();
            return instance;
        }

        private MSWorldWithoutBoundariesStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = true;
        }
    }
}
