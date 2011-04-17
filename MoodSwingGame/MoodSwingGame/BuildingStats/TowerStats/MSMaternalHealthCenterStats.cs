using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSMaternalHealthCenterStats : MSTowerStats
    {
        private static MSMaternalHealthCenterStats instance;
        public static MSMaternalHealthCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSMaternalHealthCenterStats();
            return instance;
        }

        private MSMaternalHealthCenterStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = true;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
        }
    }
}
