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
    public class MSMoodManager
    {
        private static MSMoodManager moodManager = null;

        public static MSMoodManager GetInstance()
        {
            if (moodManager == null)
                moodManager = new MSMoodManager();
            return moodManager;
        }

        private float poverty;

        private float education;

        private float genderEquality;

        private float childHealth;

        private float maternalHealth;

        private float hivAids;

        private float environment;

        private float globalPartnership;

        private float mood;
        public float Mood { get { return mood; } }
        private bool isAlive;
        public bool IsAlive { get { return isAlive; } }

        private MSMoodManager()
        {
            mood = 0.5f;
            isAlive = true;
        }

        public void takeDamage()
        {
            mood -= 0.05f;
            if (mood <= 0)
            {
                mood = 0;
                isAlive = false;
                (new InitiateGameOverSequence()).PerformAction(MoodSwing.getInstance());
            }
        }

        public void takeHealth()
        {
            mood += 0.01f;
            if (mood > 1)
                mood = 1;

            isAlive = true;
        }

        public void Reset()
        {
            mood = 0.5f;
            isAlive = true;
        }
    }
}
