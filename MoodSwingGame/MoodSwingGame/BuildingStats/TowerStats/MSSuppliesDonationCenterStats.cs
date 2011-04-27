using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSSuppliesDonationCenterStats : MSTowerStats
    {
        private static MSSuppliesDonationCenterStats instance;
        public static MSSuppliesDonationCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSSuppliesDonationCenterStats();
            return instance;
        }
        public virtual float GetBuildTime() { return 5; }
        private MSSuppliesDonationCenterStats()
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
