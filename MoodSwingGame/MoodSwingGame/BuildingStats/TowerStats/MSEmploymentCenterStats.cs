﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSEmploymentCenterStats : MSUltiBuildingStat
    {
        private static MSEmploymentCenterStats instance;
        public static MSEmploymentCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSEmploymentCenterStats();
            return instance;
        }
        private MSEmploymentCenterStats()
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
