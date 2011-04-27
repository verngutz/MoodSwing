using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSWomenPoliticiansCenterStats : MSUltiBuildingStat
    {
        private static MSWomenPoliticiansCenterStats instance;
        public static MSWomenPoliticiansCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSWomenPoliticiansCenterStats();
            return instance;
        }

        private MSWomenPoliticiansCenterStats()
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
    }
}
