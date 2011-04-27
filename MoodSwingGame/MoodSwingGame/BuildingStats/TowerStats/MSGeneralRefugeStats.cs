using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSGeneralRefugeStats : MSTowerStats
    {
        private static MSGeneralRefugeStats instance;
        public static MSGeneralRefugeStats GetInstance()
        {
            if (instance == null)
                instance = new MSGeneralRefugeStats();
            return instance;
        }
        public virtual float GetBuildTime() { return 5; }
        private MSGeneralRefugeStats()
        {
            povertyEffectiveness = true;
            educationEffectiveness = true;
            genderEqualityEffectiveness = true;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
        }
    }
}
