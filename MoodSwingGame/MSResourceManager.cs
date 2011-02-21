using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public class MSResourceManager
    {
        public int Funds { get; set; }
        public int VolunteerCapacity { get; set; }
        public int TotalVolunteers { get; set; }
        public int IdleVolunteers { get; set; }

        public MSResourceManager(int initial_funds) 
        {
            Funds = initial_funds;
        }
    }
}
