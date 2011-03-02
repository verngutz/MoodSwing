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
        private const float INITIAL_MOOD = 0.5f;
        private const float MOB_SUCCESS_MOOD_DAMAGE = 0.05f;
        private const float PERSON_HELPED_MOOD_BONUS = 0.01f;
        private const float PERSON_HELPED_SCORE_INCREASE = 0.01f;
        private const float MDG_COMPLETE_MOOD_BONUS = 0.05f;

        private bool immortal; //just for testing.

        private float mood;
        public float Mood { get { return mood; } }
        private bool isAlive;
        public bool IsAlive { get { return isAlive; } }

        private bool povertyBonusEnabled;
        private float povertyScore;
        public float PovertyScore { get { return povertyScore; } }

        private bool educationBonusEnabled;
        private float educationScore;
        public float EducationScore { get { return educationScore; } }

        private bool genderEqualityBonusEnabled;
        private float genderEqualityScore;
        public float GenderEqualityScore { get { return genderEqualityScore; } }

        private bool childHleathBonusEnabled;
        private float childHealthScore;
        public float ChildHealthScore { get { return childHealthScore; } }

        private bool maternalHealthBonusEnabled;
        private float maternalHealthScore;
        public float MaternalHealthScore { get { return maternalHealthScore; } }

        private bool hivAidsBonusEnabled;
        private float hivAidsScore;
        public float HivAidsScore { get { return hivAidsScore; } }

        private bool environmentBonusEnabled;
        private float environmentScore;
        public float EnvironmentScore { get { return environmentScore; } }

        private bool globalPartnershipBonusEnabled;
        private float globalPartnershipScore;
        public float GlobalPartnershipScore { get { return globalPartnershipScore; } }

        private MSProgressBar povertyProgressBar;
        public MSProgressBar PovertyProgressBar { get { return povertyProgressBar; } }

        private MSProgressBar educationProgressBar;
        public MSProgressBar EducationProgressBar { get { return educationProgressBar; } }

        private MSProgressBar genderEqualityProgressBar;
        public MSProgressBar GenderEqualityProgressBar { get { return genderEqualityProgressBar; } }

        private MSProgressBar childHealthProgressBar;
        public MSProgressBar ChildHealthProgressBar { get { return childHealthProgressBar; } }

        private MSProgressBar maternalHealthProgressBar;
        public MSProgressBar MaternalHealthProgressBar { get { return maternalHealthProgressBar; } }

        private MSProgressBar hivAidsProgressBar;
        public MSProgressBar HivAidsProgressBar { get { return hivAidsProgressBar; } }

        private MSProgressBar environmentProgressBar;
        public MSProgressBar EnvironmentProgressBar { get { return environmentProgressBar; } }

        private MSProgressBar globalPartnershipProgressBar;
        public MSProgressBar GlobalPartnershipProgressBar { get { return globalPartnershipProgressBar; } }

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

            povertyProgressBar = new MSProgressBar
            (
                new Rectangle(0, 0, 1024, 91),
                MoodSwing.GetInstance().SpriteBatch,
                MoodSwing.GetInstance(),
                null,
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/1"),
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/1Full"),
                MSProgressBar.Orientation.VERTICAL
            );

            educationProgressBar = new MSProgressBar
            (
                new Rectangle(0, 0, 1024, 91),
                MoodSwing.GetInstance().SpriteBatch,
                MoodSwing.GetInstance(),
                null,
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/2"),
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/2Full"),
                MSProgressBar.Orientation.VERTICAL
            );

            genderEqualityProgressBar = new MSProgressBar
            (
                new Rectangle(0, 0, 1024, 91),
                MoodSwing.GetInstance().SpriteBatch,
                MoodSwing.GetInstance(),
                null,
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/3"),
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/3Full"),
                MSProgressBar.Orientation.VERTICAL
            );

            childHealthProgressBar = new MSProgressBar
            (
                new Rectangle(0, 0, 1024, 91),
                MoodSwing.GetInstance().SpriteBatch,
                MoodSwing.GetInstance(),
                null,
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/4"),
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/4Full"),
                MSProgressBar.Orientation.VERTICAL
            );

            maternalHealthProgressBar = new MSProgressBar
            (
                new Rectangle(0, 0, 1024, 91),
                MoodSwing.GetInstance().SpriteBatch,
                MoodSwing.GetInstance(),
                null,
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/5"),
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/5Full"),
                MSProgressBar.Orientation.VERTICAL
            );

            hivAidsProgressBar = new MSProgressBar
            (
                new Rectangle(0, 0, 1024, 91),
                MoodSwing.GetInstance().SpriteBatch,
                MoodSwing.GetInstance(),
                null,
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/6"),
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/6Full"),
                MSProgressBar.Orientation.VERTICAL
            );

            environmentProgressBar = new MSProgressBar
            (
                new Rectangle(0, 0, 1024, 91),
                MoodSwing.GetInstance().SpriteBatch,
                MoodSwing.GetInstance(),
                null,
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/7"),
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/7Full"),
                MSProgressBar.Orientation.VERTICAL
            );

            globalPartnershipProgressBar = new MSProgressBar
            (
                new Rectangle(0, 0, 1024, 91),
                MoodSwing.GetInstance().SpriteBatch,
                MoodSwing.GetInstance(),
                null,
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/8"),
                MoodSwing.GetInstance().Content.Load<Texture2D>("GamePanel/8Full"),
                MSProgressBar.Orientation.VERTICAL
            );
        }

        public void TakeDamage()
        {
            mood -= MOB_SUCCESS_MOOD_DAMAGE;
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
            mood += PERSON_HELPED_MOOD_BONUS;
            if (mood > 1)
                mood = 1;

            isAlive = true;
        }

        private void AddScore(ref float score_variable, ref bool score_enabled, ref MSProgressBar bar)
        {
            if (score_enabled)
            {
                score_variable += PERSON_HELPED_SCORE_INCREASE;
                if (score_variable > 1)
                {
                    score_variable = 1;
                    mood += MDG_COMPLETE_MOOD_BONUS;
                    score_enabled = false;
                }
                bar.Progress = score_variable;
            }
        }

        public void AddMDGScore(MSMilleniumDevelopmentGoal mdg)
        {
            switch (mdg)
            {
                case MSMilleniumDevelopmentGoal.POVERTY:
                    AddScore(ref povertyScore, ref povertyBonusEnabled, ref povertyProgressBar);
                    break;
                case MSMilleniumDevelopmentGoal.EDUCATION:
                    AddScore(ref educationScore, ref educationBonusEnabled, ref educationProgressBar);
                    break;
                case MSMilleniumDevelopmentGoal.GENDER_EQUALITY:
                    AddScore(ref genderEqualityScore, ref genderEqualityBonusEnabled, ref genderEqualityProgressBar);
                    break;
                case MSMilleniumDevelopmentGoal.CHILD_HEALTH:
                    AddScore(ref childHealthScore, ref childHleathBonusEnabled, ref childHealthProgressBar);
                    break;
                case MSMilleniumDevelopmentGoal.MATERNAL_HEALTH:
                    AddScore(ref maternalHealthScore, ref maternalHealthBonusEnabled, ref maternalHealthProgressBar);
                    break;
                case MSMilleniumDevelopmentGoal.HIV_AIDS:
                    AddScore(ref hivAidsScore, ref hivAidsBonusEnabled, ref hivAidsProgressBar);
                    break;
                case MSMilleniumDevelopmentGoal.ENVIRONMENT:
                    AddScore(ref environmentScore, ref environmentBonusEnabled, ref environmentProgressBar);
                    break;
                case MSMilleniumDevelopmentGoal.GLOBAL_PARTNERSHIP:
                    AddScore(ref globalPartnershipScore, ref globalPartnershipBonusEnabled, ref globalPartnershipProgressBar);
                    break;
            }
        }
    }
}
