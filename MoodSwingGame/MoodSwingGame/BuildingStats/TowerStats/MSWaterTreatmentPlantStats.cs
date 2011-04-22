using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSWaterTreatmentPlantStats : MSTowerStats
    {
        private static MSWaterTreatmentPlantStats instance;
        public static MSWaterTreatmentPlantStats GetInstance()
        {
            if (instance == null)
                instance = new MSWaterTreatmentPlantStats();
            return instance;
        }

        private MSWaterTreatmentPlantStats()
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
