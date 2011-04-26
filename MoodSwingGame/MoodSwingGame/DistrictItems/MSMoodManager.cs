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
        private const float INITIAL_MOOD = 1f;
        private const float MOB_SUCCESS_MOOD_DAMAGE = 0.05f;
        private const float PERSON_HELPED_MOOD_BONUS = 0.01f;
        private const float PERSON_HELPED_SCORE_INCREASE = 0.04f;
        private const float MDG_COMPLETE_MOOD_BONUS = 0.05f;
        private const float LOW_MOOD_LEVEL = 0.35f;
        private const int LOW_MOOD_WARNING_DELAY = 100;

        private int lowMoodWarningTimer;
        private static bool immortal = false; //just for testing.

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

        public static MSMoodManager instance;
        public static MSMoodManager GetInstance()
        {
            if (instance == null)
                instance = new MSMoodManager();
            return instance;
        }

        /*public String toString()
        {
            instance.mood = INITIAL_MOOD;
            instance.isAlive = true;

            instance.povertyScore = 0;
            instance.educationScore = 0;
            instance.genderEqualityScore = 0;
            instance.childHealthScore = 0;
            instance.maternalHealthScore = 0;
            instance.hivAidsScore = 0;
            instance.environmentScore = 0;
            instance.globalPartnershipScore = 0;

            instance.povertyBonusEnabled = true;
            instance.educationBonusEnabled = true;
            instance.genderEqualityBonusEnabled = true;
            instance.childHleathBonusEnabled = true;
            instance.maternalHealthBonusEnabled = true;
            instance.hivAidsBonusEnabled = true;
            instance.environmentBonusEnabled = true;
            instance.globalPartnershipBonusEnabled = true;

            instance.lowMoodWarningTimer = 0;

            instance.PovertyProgressBar.Progress = 0;
            instance.EducationProgressBar.Progress = 0;
            instance.GenderEqualityProgressBar.Progress = 0;
            instance.ChildHealthProgressBar.Progress = 0;
            instance.MaternalHealthProgressBar.Progress = 0;
            instance.HivAidsProgressBar.Progress = 0;
            instance.EnvironmentProgressBar.Progress = 0;
            instance.GlobalPartnershipProgressBar.Progress = 0;
        }*/
        public static void Reset()
        {
            instance.mood = INITIAL_MOOD;
            instance.isAlive = true;

            instance.povertyScore = 0;
            instance.educationScore = 0;
            instance.genderEqualityScore = 0;
            instance.childHealthScore = 0;
            instance.maternalHealthScore = 0;
            instance.hivAidsScore = 0;
            instance.environmentScore = 0;
            instance.globalPartnershipScore = 0;

            instance.povertyBonusEnabled = true;
            instance.educationBonusEnabled = true;
            instance.genderEqualityBonusEnabled = true;
            instance.childHleathBonusEnabled = true;
            instance.maternalHealthBonusEnabled = true;
            instance.hivAidsBonusEnabled = true;
            instance.environmentBonusEnabled = true;
            instance.globalPartnershipBonusEnabled = true;

            instance.lowMoodWarningTimer = 0;

            instance.PovertyProgressBar.Progress = 0;
            instance.EducationProgressBar.Progress = 0;
            instance.GenderEqualityProgressBar.Progress = 0;
            instance.ChildHealthProgressBar.Progress = 0;
            instance.MaternalHealthProgressBar.Progress = 0;
            instance.HivAidsProgressBar.Progress = 0;
            instance.EnvironmentProgressBar.Progress = 0;
            instance.GlobalPartnershipProgressBar.Progress = 0;
        }

        private MSMoodManager() : base(MoodSwing.GetInstance())
        {
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
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (mood <= LOW_MOOD_LEVEL)
            {
                if (lowMoodWarningTimer == 0 && MoodSwing.GetInstance().BGM.Count == 3)
                {
                    MoodSwing.GetInstance().BGM = new Queue<Song>();
                    Queue<Song> bgm = MoodSwing.GetInstance().BGM;
                    bgm.Enqueue(Game.Content.Load<Song>("DefendroThemeB"));
                    MediaPlayer.Stop();
                    Song nextSong = bgm.Dequeue();
                    MediaPlayer.Play(nextSong);
                    bgm.Enqueue(nextSong);
                    MoodSwing.GetInstance().Notifier.InvokeNotification("Warning: The district is in a bad mood.");
                    lowMoodWarningTimer = LOW_MOOD_WARNING_DELAY;
                }
                else
                    lowMoodWarningTimer--;

            }
            else
            {
                if (MoodSwing.GetInstance().BGM.Count == 1)
                {
                    MoodSwing.GetInstance().BGM = new Queue<Song>();
                    Queue<Song> bgm = MoodSwing.GetInstance().BGM;
                    bgm.Enqueue(Game.Content.Load<Song>("MoodSwing"));
                    bgm.Enqueue(Game.Content.Load<Song>("7thFloor"));
                    bgm.Enqueue(Game.Content.Load<Song>("Bliss"));
                    MediaPlayer.Stop();
                    Song nextSong = bgm.Dequeue();
                    MediaPlayer.Play(nextSong);
                    bgm.Enqueue(nextSong);
                }
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
                    (new InitiateGameOverSequence(false)).PerformAction(Game);
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
            if (score_enabled && bar.Visible)
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
                (new InitiateGameOverSequence(true)).PerformAction(Game);
        }
    }
}
