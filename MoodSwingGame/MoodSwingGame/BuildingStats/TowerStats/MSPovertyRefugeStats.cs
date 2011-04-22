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
    }
}
