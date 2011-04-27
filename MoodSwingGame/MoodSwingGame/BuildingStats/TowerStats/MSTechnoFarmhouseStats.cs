using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSTechnoFarmhouseStats : MSUltiBuildingStat
    {
        private static MSTechnoFarmhouseStats instance;
        public static MSTechnoFarmhouseStats GetInstance()
        {
            if (instance == null)
                instance = new MSTechnoFarmhouseStats();
            return instance;
        }
        public override float GetBuildTime() { return 5; }
        private MSTechnoFarmhouseStats()
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
