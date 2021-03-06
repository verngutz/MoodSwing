﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSWhiteScreensCenterStats : MSUltiBuildingStat
    {
        private static MSWhiteScreensCenterStats instance;
        public static MSWhiteScreensCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSWhiteScreensCenterStats();
            return instance;
        }
        private MSWhiteScreensCenterStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = true;
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
