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
        private const int GOAL_POVERTY_SCORE = 50;
        private const int GOAL_EDUCATION_SCORE = 50;
        private const int GOAL_GENDER_EQUALITY_SCORE = 50;
        private const int GOAL_CHILD_HEALTH_SCORE = 50;
        private const int GOAL_MATERNAL_HEALTH_SCORE = 50;
        private const int GOAL_HIV_AIDS_SCORE = 50;
        private const int GOAL_ENVIRONMENT_SCORE = 50;
        private const int GOAL_GLOBAL_PARTNERSHIP_SCORE = 50;

        private const float INITIAL_MOOD = 0.5f;
        private const float MOOD_DAMAGE = 0.05f;
        private const float MOOD_HEAL = 0.01f;
        private const float MOOD_BONUS = 0.05f;

        private bool immortal; //just for testing.

        private float mood;
        public float Mood { get { return mood; } }
        private bool isAlive;
        public bool IsAlive { get { return isAlive; } }

        private bool povertyBonusEnabled;
        private int povertyScore;
        public int PovertyScore { get { return povertyScore; } }

        private bool educationBonusEnabled;
        private int educationScore;
        public int EducationScore { get { return educationScore; } }

        private bool genderEqualityBonusEnabled;
        private int genderEqualityScore;
        public int GenderEqualityScore { get { return genderEqualityScore; } }

        private bool childHleathBonusEnabled;
        private int childHealthScore;
        public int ChildHealthScore { get { return childHealthScore; } }

        private bool maternalHealthBonusEnabled;
        private int maternalHealthScore;
        public int MaternalHealthScore { get { return maternalHealthScore; } }

        private bool hivAidsBonusEnabled;
        private int hivAidsScore;
        public int HivAidsScore { get { return hivAidsScore; } }

        private bool environmentBonusEnabled;
        private int environmentScore;
        public int EnvironmentScore { get { return environmentScore; } }

        private bool globalPartnershipBonusEnabled;
        private int globalPartnershipScore;
        public int GlobalPartnershipScore { get { return globalPartnershipScore; } }

        private static MSMoodManager moodManager = null;

        public static MSMoodManager GetInstance()
        {
            if (moodManager == null)
                Reset();
            return moodManager;
        }

        public static void Reset()
        {
            moodManager = new MSMoodManager();
        }

        private MSMoodManager()
        {
            mood = INITIAL_MOOD;
            isAlive = true;

            povertyScore = 0;
            educationScore = 0;
            genderEqualityScore = 0;
            childHealthScore = 0;
            maternalHealthScore = 0;
            hivAidsScore = 0;
            environmentScore = 0;
            globalPartnershipScore = 0;

            povertyBonusEnabled = true;
            educationBonusEnabled = true;
            genderEqualityBonusEnabled = true;
            childHleathBonusEnabled = true;
            maternalHealthBonusEnabled = true;
            hivAidsBonusEnabled = true;
            environmentBonusEnabled = true;
            globalPartnershipBonusEnabled = true;

            immortal = true;
        }

        public void TakeDamage()
        {
            mood -= MOOD_DAMAGE;
            if (mood <= 0)
            {
                mood = 0;
                if (!immortal)
                {
                    isAlive = false;
                    (new InitiateGameOverSequence()).PerformAction(MoodSwing.GetInstance());
                }
            }
        }

        public void TakeHealth()
        {
            mood += MOOD_HEAL;
            if (mood > 1)
                mood = 1;

            isAlive = true;
        }

        private void AddScore(ref int score_variable, ref bool score_enabled, int goal_amount)
        {
            if (score_enabled)
            {
                score_variable++;
                if (score_variable > goal_amount)
                {
                    score_variable = goal_amount;
                    mood += MOOD_BONUS;
                    score_enabled = false;
                }
            }
        }

        public void AddMDGScore(MSMilleniumDevelopmentGoal mdg)
        {
            switch (mdg)
            {
                case MSMilleniumDevelopmentGoal.POVERTY:
                    AddScore(ref povertyScore, ref povertyBonusEnabled, GOAL_POVERTY_SCORE);
                    break;
                case MSMilleniumDevelopmentGoal.EDUCATION:
                    AddScore(ref educationScore, ref educationBonusEnabled, GOAL_EDUCATION_SCORE);
                    break;
                case MSMilleniumDevelopmentGoal.GENDER_EQUALITY:
                    AddScore(ref genderEqualityScore, ref genderEqualityBonusEnabled, GOAL_GENDER_EQUALITY_SCORE);
                    break;
                case MSMilleniumDevelopmentGoal.CHILD_HEALTH:
                    AddScore(ref childHealthScore, ref childHleathBonusEnabled, GOAL_CHILD_HEALTH_SCORE);
                    break;
                case MSMilleniumDevelopmentGoal.MATERNAL_HEALTH:
                    AddScore(ref maternalHealthScore, ref maternalHealthBonusEnabled, GOAL_MATERNAL_HEALTH_SCORE);
                    break;
                case MSMilleniumDevelopmentGoal.HIV_AIDS:
                    AddScore(ref hivAidsScore, ref hivAidsBonusEnabled, GOAL_HIV_AIDS_SCORE);
                    break;
                case MSMilleniumDevelopmentGoal.ENVIRONMENT:
                    AddScore(ref environmentScore, ref environmentBonusEnabled, GOAL_ENVIRONMENT_SCORE);
                    break;
                case MSMilleniumDevelopmentGoal.GLOBAL_PARTNERSHIP:
                    AddScore(ref globalPartnershipScore, ref globalPartnershipBonusEnabled, GOAL_GLOBAL_PARTNERSHIP_SCORE);
                    break;
            }
        }
    }
}
