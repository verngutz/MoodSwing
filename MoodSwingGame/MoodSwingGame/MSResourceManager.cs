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
        private static MSResourceManager resourceManager = null;
        public static MSResourceManager GetInstance()
        {
            if (resourceManager == null) resourceManager = new MSResourceManager();
            return resourceManager;
        }

        public const int VOLUNTEER_CENTER_GAIN = 30; 
        public const int VOLUNTEER_GENERATION_DIFFICULTY = 100;

        private int volunteerGenerationCounter;

        public int Funds { get; set; }
        public int VolunteerCapacity { get; set; }
        public int TotalVolunteers { get; set; }
        public int IdleVolunteers { get; set; }

        private MSResourceManager() : base(MoodSwing.GetInstance()) { }

        public static void instantiate(int initial_funds, int initial_volunteer_centers)
        {
            if (resourceManager == null) resourceManager = new MSResourceManager();
            resourceManager.Funds = initial_funds;
            resourceManager.VolunteerCapacity = initial_volunteer_centers * VOLUNTEER_CENTER_GAIN;
            resourceManager.TotalVolunteers = 0;
            resourceManager.IdleVolunteers = 0;
            resourceManager.volunteerGenerationCounter = 0;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (TotalVolunteers < VolunteerCapacity)
            {
                if (volunteerGenerationCounter++ > VOLUNTEER_GENERATION_DIFFICULTY)
                {
                    int volunteerGeneration = MSRandom.random.Next(VolunteerCapacity + 1);
                    if (volunteerGeneration > TotalVolunteers)
                    {
                        TotalVolunteers++;
                        MSUnitHandler.GetInstance().VolunteerCitizen((
                            MoodSwing.GetInstance().CurrentScreen as MSDistrictScreen).Map);
                    }
                    volunteerGenerationCounter = 0;
                }
            }
        }
    }
}
