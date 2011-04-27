using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSImmunizationOutreachStats : MSUltiBuildingStat
    {
        private static MSImmunizationOutreachStats instance;
        public static MSImmunizationOutreachStats GetInstance()
        {
            if (instance == null)
                instance = new MSImmunizationOutreachStats();
            return instance;
        }

        private MSImmunizationOutreachStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = true;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
        }
    }
}
