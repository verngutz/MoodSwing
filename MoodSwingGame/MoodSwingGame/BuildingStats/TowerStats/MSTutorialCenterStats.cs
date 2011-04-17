using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSTutorialCenterStats : MSTowerStats
    {
        private static MSTutorialCenterStats instance;
        public static MSTutorialCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSTutorialCenterStats();
            return instance;
        }

        private MSTutorialCenterStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = true;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
        }
    }

}
