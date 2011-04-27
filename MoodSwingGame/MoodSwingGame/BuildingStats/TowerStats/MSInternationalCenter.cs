using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSInternationalCenterStats : MSTowerStats
    {
        private static MSInternationalCenterStats instance;
        public static MSInternationalCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSInternationalCenterStats();
            return instance;
        }
        public virtual float GetBuildTime() { return 5; }
        private MSInternationalCenterStats()
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
