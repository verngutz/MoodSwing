using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSPovertyRefugeStats : MSTowerStats
    {
        private static MSPovertyRefugeStats instance;
        public static MSPovertyRefugeStats GetInstance()
        {
            if (instance == null)
                instance = new MSPovertyRefugeStats();
            return instance;
        }
        public override float GetBuildTime() { return 5; }
        private MSPovertyRefugeStats()
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
            return 300;
        }

        public override int GetVolunteerCost()
        {
            return 3;
        }
    }
}
