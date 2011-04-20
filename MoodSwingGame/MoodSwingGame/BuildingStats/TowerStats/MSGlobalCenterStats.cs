using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSGlobalCenterStats : MSTowerStats
    {
        private static MSGlobalCenterStats instance;
        public static MSGlobalCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSGlobalCenterStats();
            return instance;
        }

        private MSGlobalCenterStats()
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
