﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSVaccinationCenterStats : MSTowerStats
    {
        private static MSVaccinationCenterStats instance;
        public static MSVaccinationCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSVaccinationCenterStats();
            return instance;
        }

        private MSVaccinationCenterStats()
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