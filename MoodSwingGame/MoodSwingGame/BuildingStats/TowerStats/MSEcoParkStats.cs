using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSEcoParkStats : MSTowerStats
    {
        private static MSEcoParkStats instance;
        public static MSEcoParkStats GetInstance()
        {
            if (instance == null)
                instance = new MSEcoParkStats();
            return instance;
        }

        private MSEcoParkStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = true;
            globalEffectiveness = false;
        }
    }
}
