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
        public virtual float GetBuildTime() { return 5; }
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
    }
}
