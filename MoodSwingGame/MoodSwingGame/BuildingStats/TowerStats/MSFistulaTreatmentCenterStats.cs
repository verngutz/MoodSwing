using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSFistulaTreatmentCenterStats : MSTowerStats
    {
        private static MSFistulaTreatmentCenterStats instance;
        public static MSFistulaTreatmentCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSFistulaTreatmentCenterStats();
            return instance;
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
