using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSWhiteScreensCenterStats : MSUltiBuildingStat
    {
        private static MSWhiteScreensCenterStats instance;
        public static MSWhiteScreensCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSWhiteScreensCenterStats();
            return instance;
        }
        public virtual float GetBuildTime() { return 30; }
        private MSWhiteScreensCenterStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = true;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
        }
    }
}
