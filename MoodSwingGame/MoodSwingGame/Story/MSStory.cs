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
    public static class MSStory
    {
        public static bool StoryEnabled { set; get; }

        public static bool VolunteerCenterEnabled { set; get; }
        public static bool TowerEnabled { set; get; }

        private static bool firstMDGBranchEnabled;
        public static bool FirstMDGBranchEnabled
        {
            get { return firstMDGBranchEnabled; }
            set 
            { 
                firstMDGBranchEnabled = value;
                MSMoodManager.GetInstance().PovertyProgressBar.Visible = value;
                MSMoodManager.GetInstance().EducationProgressBar.Visible = value;
                MSMoodManager.GetInstance().GenderEqualityProgressBar.Visible = value;
                MSUnitHandler.GetInstance().SetMobEnabled(MSMilleniumDevelopmentGoal.POVERTY, value);
                MSUnitHandler.GetInstance().SetMobEnabled(MSMilleniumDevelopmentGoal.EDUCATION, value);
                MSUnitHandler.GetInstance().SetMobEnabled(MSMilleniumDevelopmentGoal.GENDER_EQUALITY, value);
            }
        }

        private static bool secondMDGBranchEnabled;
        public static bool SecondMDGBranchEnabled
        {
            get { return secondMDGBranchEnabled; }
            set 
            { 
                secondMDGBranchEnabled = value;
                MSMoodManager.GetInstance().ChildHealthProgressBar.Visible = value;
                MSMoodManager.GetInstance().MaternalHealthProgressBar.Visible = value;
                MSMoodManager.GetInstance().HivAidsProgressBar.Visible = value;
                MSUnitHandler.GetInstance().SetMobEnabled(MSMilleniumDevelopmentGoal.CHILD_HEALTH, value);
                MSUnitHandler.GetInstance().SetMobEnabled(MSMilleniumDevelopmentGoal.MATERNAL_HEALTH, value);
                MSUnitHandler.GetInstance().SetMobEnabled(MSMilleniumDevelopmentGoal.HIV_AIDS, value);
            }
        }

        private static bool thirdMDGBranchEnabled;
        public static bool ThirdMDGBranchEnabled
        {
            get { return thirdMDGBranchEnabled; }
            set 
            {
                thirdMDGBranchEnabled = value;
                MSMoodManager.GetInstance().EnvironmentProgressBar.Visible = value;
                MSMoodManager.GetInstance().GlobalPartnershipProgressBar.Visible = value;
                MSUnitHandler.GetInstance().SetMobEnabled(MSMilleniumDevelopmentGoal.ENVIRONMENT, value);
                MSUnitHandler.GetInstance().SetMobEnabled(MSMilleniumDevelopmentGoal.GLOBAL_PARTNERSHIP, value);
            }
        }

        private static List<MSStoryEvent> timeActiveEvents;
        private static List<MSStoryEvent> eventsToRemove;

        public static void Init()
        {
            StoryEnabled = false;
            VolunteerCenterEnabled = true;
            TowerEnabled = true;
            FirstMDGBranchEnabled = true;
            SecondMDGBranchEnabled = true;
            ThirdMDGBranchEnabled = true;

            if (StoryEnabled)
            {
                timeActiveEvents = new List<MSStoryEvent>();
                eventsToRemove = new List<MSStoryEvent>();
                AddStoryEvent(new Welcome());

                VolunteerCenterEnabled = false;
                TowerEnabled = false;
                FirstMDGBranchEnabled = false;
                SecondMDGBranchEnabled = false;
                ThirdMDGBranchEnabled = false;
            }
        }

        public static void AddStoryEvent(MSStoryEvent e)
        {
            timeActiveEvents.Add(e);
        }

        public static void RemoveStoryEvent(MSStoryEvent e)
        {
            eventsToRemove.Add(e);
        }

        public static void Update(double gameTime, Game game)
        {
            if (StoryEnabled)
            {
                foreach (MSStoryEvent e in timeActiveEvents)
                {
                    if (e.Enabled())
                    {
                        e.PerformAction(game);
                    }
                }

                foreach (MSStoryEvent e in eventsToRemove)
                {
                    timeActiveEvents.Remove(e);
                }

                eventsToRemove.Clear();
            }
        }
    }
}
