﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSApartmentStats : MSUltiBuildingStat
    {
        private static MSApartmentStats instance;
        public static MSApartmentStats GetInstance()
        {
            if (instance == null)
                instance = new MSApartmentStats();
            return instance;
        }
        public virtual float GetBuildTime() { return 30; }
        private MSApartmentStats()
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
