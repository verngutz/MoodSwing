﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSEFASchoolStats : MSUltiBuildingStat
    {
        private static MSEFASchoolStats instance;
        public static MSEFASchoolStats GetInstance()
        {
            if (instance == null)
                instance = new MSEFASchoolStats();
            return instance;
        }
        private MSEFASchoolStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = true;
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
