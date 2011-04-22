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

        private static List<MSStoryEvent> timeActiveEvents;
        private static List<MSStoryEvent> eventsToRemove;

        public static void Init()
        {
            timeActiveEvents = new List<MSStoryEvent>();
            eventsToRemove = new List<MSStoryEvent>();
            AddStoryEvent(new Welcome());
            StoryEnabled = false;
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
