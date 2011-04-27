using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSSuppliesDonationCenterStats : MSUltiBuildingStat
    {
        private static MSSuppliesDonationCenterStats instance;
        public static MSSuppliesDonationCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSSuppliesDonationCenterStats();
            return instance;
        }
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

        public override int GetFundsCost()
        {
            return 450;
        }

        public override int GetVolunteerCost()
        {
            return 4;
        }
    }
}
