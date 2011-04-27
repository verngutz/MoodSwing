using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSBedNetsCenterStats : MSTowerStats
    {
        private static MSBedNetsCenterStats instance;
        public static MSBedNetsCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSBedNetsCenterStats();
            return instance;
        }

        public override float GetBuildTime() { return 5; }
        private MSBedNetsCenterStats()
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
