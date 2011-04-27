using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSAntiretroviralCenterStats : MSUltiBuildingStat
    {
        private static MSAntiretroviralCenterStats instance;
        public static MSAntiretroviralCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSAntiretroviralCenterStats();
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
        private MSAntiretroviralCenterStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = true;
            environmentEffectiveness = false;
            globalEffectiveness = false;
        }
    }
}
