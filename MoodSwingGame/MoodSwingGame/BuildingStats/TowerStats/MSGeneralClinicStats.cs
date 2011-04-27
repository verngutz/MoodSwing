using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSGeneralClinicStats : MSTowerStats
    {
        private static MSGeneralClinicStats instance;
        public static MSGeneralClinicStats GetInstance()
        {
            if (instance == null)
                instance = new MSGeneralClinicStats();
            return instance;
        }
        public virtual float GetBuildTime() { return 5; }
        private MSGeneralClinicStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = true;
            maternalHealthEffectiveness = true;
            hivAidsEffectiveness = true;
            environmentEffectiveness = false;
            globalEffectiveness = false;
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
