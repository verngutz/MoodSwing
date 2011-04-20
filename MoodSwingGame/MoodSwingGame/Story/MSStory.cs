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
        //public static bool eventVariable1; etc. etc.

        private static List<MSStoryEvent> timeActiveEvents = new List<MSStoryEvent>();

        public static void AddStoryEvent(MSStoryEvent e)
        {
            timeActiveEvents.Add(e);
        }

        public static void RemoveStoryEvent(MSStoryEvent e)
        {
            timeActiveEvents.Remove(e);
        }

        private static void Update(int time, Game game)
        {
            foreach(MSStoryEvent e in timeActiveEvents)
            {
                if (e.Enabled())
                {
                    e.PerformAction(game);
                }
            }

            /**
             * if (time == 0)
             * {
             *      AddStoryEvent(blah); etc. etc.
             * }
             */
        }
    }
}
