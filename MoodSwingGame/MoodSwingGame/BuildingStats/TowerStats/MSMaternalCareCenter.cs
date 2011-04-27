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
        public virtual float GetBuildTime() { return 5; }
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
    }
}
