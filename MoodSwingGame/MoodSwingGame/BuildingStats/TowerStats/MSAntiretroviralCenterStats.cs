using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSAntiretroviralCenterStats : MSUltiBuildingStat
    {
        private static MSAntiretroviralCenterStats instance;
        public static MSAntiretroviralCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSAntiretroviralCenterStats();
            return instance;
        }

        private MSAntiretroviralCenterStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = true;
            environmentEffectiveness = false;
            globalEffectiveness = false;
        }
    }
}
