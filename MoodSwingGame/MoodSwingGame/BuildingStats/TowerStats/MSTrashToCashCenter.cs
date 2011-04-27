﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSTrashToCashCenterStats : MSTowerStats
    {
        private static MSTrashToCashCenterStats instance;
        public static MSTrashToCashCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSTrashToCashCenterStats();
            return instance;
        }

        private MSTrashToCashCenterStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = false;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = true;
            globalEffectiveness = false;
        }
    }
}