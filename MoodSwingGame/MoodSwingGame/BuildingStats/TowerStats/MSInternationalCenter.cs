﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSInternationalCenterStats : MSTowerStats
    {
        private static MSInternationalCenterStats instance;
        public static MSInternationalCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSInternationalCenterStats();
            return instance;
        }
        private MSInternationalCenterStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = true;
        }

        public override int GetFundsCost()
        {
            return 200;
        }

        public override int GetVolunteerCost()
        {
            return 2;
        }
    }
}
