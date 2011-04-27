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

        public override int GetFundsCost()
        {
            return 450;
        }

        public override int GetVolunteerCost()
        {
            return 4;
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
