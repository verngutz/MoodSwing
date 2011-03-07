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
    public class MSMoodManager : GameComponent
    {
        private const float INITIAL_MOOD = 0.5f;
        private const float MOB_SUCCESS_MOOD_DAMAGE = 0.05f;
        private const float PERSON_HELPED_MOOD_BONUS = 0.01f;
        private const float PERSON_HELPED_SCORE_INCREASE = 0.01f;
        private const float MDG_COMPLETE_MOOD_BONUS = 0.05f;
        private const float LOW_MOOD_LEVEL = 0.2f;
        private const int LOW_MOOD_WARNING_DELAY = 100;

        private int lowMoodWarningTimer;
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

        private MSMDGProgressBar povertyProgressBar;
        public MSMDGProgressBar PovertyProgressBar { get { return povertyProgressBar; } }

        private MSMDGProgressBar educationProgressBar;
        public MSMDGProgressBar EducationProgressBar { get { return educationProgressBar; } }

        private MSMDGProgressBar genderEqualityProgressBar;
        public MSMDGProgressBar GenderEqualityProgressBar { get { return genderEqualityProgressBar; } }

        private MSMDGProgressBar childHealthProgressBar;
        public MSMDGProgressBar ChildHealthProgressBar { get { return childHealthProgressBar; } }

        private MSMDGProgressBar maternalHealthProgressBar;
        public MSMDGProgressBar MaternalHealthProgressBar { get { return maternalHealthProgressBar; } }

        private MSMDGProgressBar hivAidsProgressBar;
        public MSMDGProgressBar HivAidsProgressBar { get { return hivAidsProgressBar; } }

        private MSMDGProgressBar environmentProgressBar;
        public MSMDGProgressBar EnvironmentProgressBar { get { return environmentProgressBar; } }

        private MSMDGProgressBar globalPartnershipProgressBar;
        public MSMDGProgressBar GlobalPartnershipProgressBar { get { return globalPartnershipProgressBar; } }

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
            : base(MoodSwing.GetInstance())
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

            povertyProgressBar = new MSMDGProgressBar
            (
                new Rectangle(587, 41, 48, 35),
                (Game as MoodSwing).SpriteBatch,
                Game,
                Game.Content.Load<Texture2D>("GamePanel/1"),
                Game.Content.Load<Texture2D>("GamePanel/a"),
                Game.Content.Load<Texture2D>("GamePanel/10"),
                MSProgressBar.Orientation.VERTICAL
            );

            educationProgressBar = new MSMDGProgressBar
            (
                new Rectangle(639, 34, 31, 39),
                (Game as MoodSwing).SpriteBatch,
                Game,
                Game.Content.Load<Texture2D>("GamePanel/2"),
                Game.Content.Load<Texture2D>("GamePanel/b"),
                Game.Content.Load<Texture2D>("GamePanel/20"),
                MSProgressBar.Orientation.VERTICAL
            );

            genderEqualityProgressBar = new MSMDGProgressBar
            (
                new Rectangle(682, 36, 38, 34),
                (Game as MoodSwing).SpriteBatch,
                Game,
                Game.Content.Load<Texture2D>("GamePanel/3"),
                Game.Content.Load<Texture2D>("GamePanel/c"),
                Game.Content.Load<Texture2D>("GamePanel/30"),
                MSProgressBar.Orientation.VERTICAL
            );

            childHealthProgressBar = new MSMDGProgressBar
            (
                new Rectangle(721, 31, 45, 36),
                (Game as MoodSwing).SpriteBatch,
                Game,
                Game.Content.Load<Texture2D>("GamePanel/4"),
                Game.Content.Load<Texture2D>("GamePanel/d"),
                Game.Content.Load<Texture2D>("GamePanel/40"),
                MSProgressBar.Orientation.VERTICAL
            );

            maternalHealthProgressBar = new MSMDGProgressBar
            (
                new Rectangle(770, 25, 37, 39),
                (Game as MoodSwing).SpriteBatch,
                Game,
                Game.Content.Load<Texture2D>("GamePanel/5"),
                Game.Content.Load<Texture2D>("GamePanel/e"),
                Game.Content.Load<Texture2D>("GamePanel/50"),
                MSProgressBar.Orientation.VERTICAL
            );

            hivAidsProgressBar = new MSMDGProgressBar
            (
                new Rectangle(813, 28, 37, 33),
                (Game as MoodSwing).SpriteBatch,
                Game,
                Game.Content.Load<Texture2D>("GamePanel/6"),
                Game.Content.Load<Texture2D>("GamePanel/f"),
                Game.Content.Load<Texture2D>("GamePanel/60"),
                MSProgressBar.Orientation.VERTICAL
            );

            environmentProgressBar = new MSMDGProgressBar
            (
                new Rectangle(856, 23, 51, 33),
                (Game as MoodSwing).SpriteBatch,
                Game,
                Game.Content.Load<Texture2D>("GamePanel/7"),
                Game.Content.Load<Texture2D>("GamePanel/g"),
                Game.Content.Load<Texture2D>("GamePanel/70"),
                MSProgressBar.Orientation.VERTICAL
            );

            globalPartnershipProgressBar = new MSMDGProgressBar
            (
                new Rectangle(908, 17, 45, 31),
                (Game as MoodSwing).SpriteBatch,
                Game,
                Game.Content.Load<Texture2D>("GamePanel/8"),
                Game.Content.Load<Texture2D>("GamePanel/h"),
                Game.Content.Load<Texture2D>("GamePanel/80"),
                MSProgressBar.Orientation.VERTICAL
            );

            lowMoodWarningTimer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (mood <= LOW_MOOD_LEVEL)
            {
                if (lowMoodWarningTimer == 0)
                {
                    MSNotifier.GetInstance().InvokeNotification("Warning: The district is in a bad mood.");
                    lowMoodWarningTimer = LOW_MOOD_WARNING_DELAY;
                }
                else
                    lowMoodWarningTimer--;

            }
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
                    (new InitiateGameOverLoseSequence()).PerformAction(Game);
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

        private void AddScore(ref float score_variable, ref bool score_enabled, ref MSMDGProgressBar bar)
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

            if(povertyScore == 1 && educationScore == 1 && genderEqualityScore == 1 && childHealthScore == 1
                && maternalHealthScore == 1 && hivAidsScore == 1 && environmentScore == 1 && globalPartnershipScore == 1)
                (new InitiateGameOverWinSequence()).PerformAction(Game);
        }
    }
}
