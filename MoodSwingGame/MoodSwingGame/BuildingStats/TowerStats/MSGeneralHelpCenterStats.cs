using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSGeneralHelpCenterStats : MSTowerStats
    {
        private static MSGeneralHelpCenterStats instance;
        public static MSGeneralHelpCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSGeneralHelpCenterStats();
            return instance;
        }

        private MSGeneralHelpCenterStats()
        {
            povertyEffectiveness = true;
            educationEffectiveness = true;
            genderEqualityEffectiveness = true;
            childHealthEffectiveness = true;
            maternalHealthEffectiveness = true;
            hivAidsEffectiveness = true;
            environmentEffectiveness = true;
            globalEffectiveness = true;
        }
    }
}
