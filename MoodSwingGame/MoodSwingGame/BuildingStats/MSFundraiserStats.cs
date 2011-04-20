using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSFundraiserStats : MSBuildingStats
    {
        private MSFundraiserStats() { }
        private static MSFundraiserStats instance;
        public static MSFundraiserStats GetInstance()
        {
            if (instance == null)
                instance = new MSFundraiserStats();
            return instance;
        }

        public int GetVolunteerCost() { return 2; }
        public int GetFundsCost() { return 100; }
    }
}
