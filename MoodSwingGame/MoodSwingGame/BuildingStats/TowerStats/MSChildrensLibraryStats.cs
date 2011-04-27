using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSChildrensLibraryStats : MSUltiBuildingStat
    {
        private static MSChildrensLibraryStats instance;
        public static MSChildrensLibraryStats GetInstance()
        {
            if (instance == null)
                instance = new MSChildrensLibraryStats();
            return instance;
        }
        private MSChildrensLibraryStats()
        {
            povertyEffectiveness = false;
            educationEffectiveness = true;
            genderEqualityEffectiveness = false;
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
