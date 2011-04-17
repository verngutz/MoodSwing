using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSHivAidsHealthCenterStats : MSTowerStats
    {
        private static MSHivAidsHealthCenterStats instance;
        public static MSHivAidsHealthCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSHivAidsHealthCenterStats();
            return instance;
        }

        private MSHivAidsHealthCenterStats()
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
