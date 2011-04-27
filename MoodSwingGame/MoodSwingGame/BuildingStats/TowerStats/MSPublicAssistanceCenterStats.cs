﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSPublicAssistanceCenterStats : MSTowerStats
    {
        private static MSPublicAssistanceCenterStats instance;
        public static MSPublicAssistanceCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSPublicAssistanceCenterStats();
            return instance;
        }

        private MSPublicAssistanceCenterStats()
        {
            povertyEffectiveness = true;
            educationEffectiveness = true;
            genderEqualityEffectiveness = true;
            childHealthEffectiveness = true;
            maternalHealthEffectiveness = true;
            hivAidsEffectiveness = true;
            environmentEffectiveness = true;
            globalEffectiveness = true;
        }
    }
}