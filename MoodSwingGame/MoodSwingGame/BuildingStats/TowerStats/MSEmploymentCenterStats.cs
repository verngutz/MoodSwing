using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSEmploymentCenterStats : MSUltiBuildingStat
    {
        private static MSEmploymentCenterStats instance;
        public static MSEmploymentCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSEmploymentCenterStats();
            return instance;
        }
        public virtual float GetBuildTime() { return 5; }
        private MSEmploymentCenterStats()
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
