using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSMaternalCareCenterStats : MSUltiBuildingStat
    {
        private static MSMaternalCareCenterStats instance;
        public static MSMaternalCareCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSMaternalCareCenterStats();
            return instance;
        }
        public override float GetBuildTime() { return 30; }
        private MSMaternalCareCenterStats()
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
