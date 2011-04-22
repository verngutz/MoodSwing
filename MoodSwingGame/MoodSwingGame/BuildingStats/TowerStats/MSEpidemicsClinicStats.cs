using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSEpidemicsClinicStats : MSTowerStats
    {
        private static MSEpidemicsClinicStats instance;
        public static MSEpidemicsClinicStats GetInstance()
        {
            if (instance == null)
                instance = new MSEpidemicsClinicStats();
            return instance;
        }

        private MSEpidemicsClinicStats()
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
