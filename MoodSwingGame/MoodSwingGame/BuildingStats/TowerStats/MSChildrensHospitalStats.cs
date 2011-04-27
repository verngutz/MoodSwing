using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSChildrensHospitalStats : MSTowerStats
    {
        private static MSChildrensHospitalStats instance;
        public static MSChildrensHospitalStats GetInstance()
        {
            if (instance == null)
                instance = new MSChildrensHospitalStats();
            return instance;
        }
        private MSChildrensHospitalStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = false;
            genderEqualityEffectiveness = false;
            childHealthEffectiveness = true;
            maternalHealthEffectiveness = false;
            hivAidsEffectiveness = false;
            environmentEffectiveness = false;
            globalEffectiveness = false;
        }

        public override int GetFundsCost()
        {
            return 250;
        }

        public override int GetVolunteerCost()
        {
            return 2;
        }
    }
}
