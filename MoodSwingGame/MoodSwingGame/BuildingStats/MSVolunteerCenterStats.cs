using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSVolunteerCenterStats : MSBuildingStats
    {
        private MSVolunteerCenterStats() { }
        private static MSVolunteerCenterStats instance;
        public static MSVolunteerCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSVolunteerCenterStats();
            return instance;
        }

        public int GetVolunteerCost() { return 0; }
        public int GetFundsCost() { return 250; }
        public float GetBuildTime() { return 5; }
    }
}
