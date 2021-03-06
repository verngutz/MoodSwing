﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSFistulaTreatmentCenterStats : MSUltiBuildingStat
    {
        private static MSFistulaTreatmentCenterStats instance;
        public static MSFistulaTreatmentCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSFistulaTreatmentCenterStats();
            return instance;
        }
        public override int GetFundsCost()
        {
            return 450;
        }

        public override int GetVolunteerCost()
        {
            return 4;
        }

        private MSFistulaTreatmentCenterStats()
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
