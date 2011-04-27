using System;
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
        public virtual float GetBuildTime() { return 3; }
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

        public override int GetFundsCost()
        {
            return 50;
        }

        public override int GetVolunteerCost()
        {
            return 1;
        }
    }
}
