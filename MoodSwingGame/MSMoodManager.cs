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
    class MSMoodManager
    {
        private static MSMoodManager moodManager = null;

        public static MSMoodManager getInstance()
        {
            if (moodManager == null)
                moodManager = new MSMoodManager();
            return moodManager;
        }

        private const int MAX_MOOD = 100;
        private int mood;
        public int Mood { get { return mood; } }
        private bool isAlive;
        public bool IsAlive { get { return isAlive; } }

        private MSMoodManager()
        {
            mood = MAX_MOOD;
            isAlive = true;
        }

        public void takeDamage(int amount)
        {
            mood -= amount;
            if (mood <= 0)
            {
                mood = 0;
            }
        }

        public void takeHealth(int amount)
        {
            mood += amount;
            if (mood > MAX_MOOD)
                mood = MAX_MOOD;
        }
    }
}
