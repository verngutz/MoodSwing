using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSTrashToCashCenterStats : MSUltiBuildingStat
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
