using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using MoodSwingCoreComponents;
using MoodSwingGUI;

namespace MoodSwingGame
{
    public class MSResourceManager : GameComponent
    {
        public const int TOWER_COST = 200;
        public const int VOLUNTEER_CENTER_COST = 250;
        public const int VOLUNTEER_CENTER_GAIN = 10; 
        public const int VOLUNTEER_GENERATION_TIME = 100;

        private int volunteerGenerationCounter;

        public int Funds { get; set; }
        public int VolunteerCapacity { get; set; }
        public int TotalVolunteers { get; set; }
        public int IdleVolunteers { get; set; }

        public MSResourceManager(int initial_funds, Game game) : base(game) 
        {
            Funds = initial_funds;
            VolunteerCapacity = 0;
            TotalVolunteers = 0;
            IdleVolunteers = 0;
            volunteerGenerationCounter = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (TotalVolunteers < VolunteerCapacity)
            {
                if (volunteerGenerationCounter++ > VOLUNTEER_GENERATION_TIME)
                {
                    int volunteerGeneration = MSRandom.random.Next(VolunteerCapacity + 1);
                    if (volunteerGeneration > TotalVolunteers)
                    {
                        TotalVolunteers++;
                        IdleVolunteers++;
                    }
                    volunteerGenerationCounter = 0;
                }
            }
        }
    }
}
