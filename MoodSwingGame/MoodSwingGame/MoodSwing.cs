using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using MoodSwingGUI;
using MoodSwingCoreComponents;
using MoodSwingGraphics;

namespace MoodSwingGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MoodSwing : Game
    {
        private GraphicsDeviceManager graphics;

        private static MoodSwing MSInstance;
        public static MoodSwing GetInstance() 
        {
            if (MSInstance == null)
                MSInstance = new MoodSwing();
            return MSInstance;
        }

        private SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        public MSScreen CurrentScreen { set; get; }

        private KeyboardState oldKeyboardState;
        public KeyboardState OldKeyboardState { get { return oldKeyboardState; } }

        private Queue<Song> bgm;
        public Queue<Song> BGM { get { return bgm; } set { bgm = value; } }

        public GameTime prevGameTime;

        private MSNotifier notifier;
        public MSNotifier Notifier { get { return notifier; } }

        private MSBloomComponent bloom;
        public MSBloomComponent Bloom { get { return bloom; } }

        private MSSmokePlumeParticleSystem smokeParticles;
        public MSSmokePlumeParticleSystem SmokeParticles { get { return smokeParticles; } }

        private MoodSwing()
        {
            graphics = new GraphicsDeviceManager(this);

            MSResolution.Init(ref graphics);
            MSResolution.SetVirtualResolution(1024, 768);
            MSResolution.SetResolution(1024, 768, true);

            //graphics.PreferMultiSampling = true;
            
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            Window.Title = "MoodSwing";
            MediaPlayer.Volume = 0.85f;
            bgm = new Queue<Song>();
            bgm.Enqueue(Content.Load<Song>("MoodSwing"));
            bgm.Enqueue(Content.Load<Song>("7thFloor"));
            bgm.Enqueue(Content.Load<Song>("Bliss"));

            bloom = new MSBloomComponent(this);
            Components.Add(bloom);

            smokeParticles = new MSSmokePlumeParticleSystem(this);
            smokeParticles.DrawOrder = -1;
            Components.Add(smokeParticles);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            oldKeyboardState = Keyboard.GetState();
            CurrentScreen = MSIntroScreen.getInstance();

            #if DEBUG
            #region bloom tweaking controls
            System.Console.WriteLine("Q plus");
            System.Console.WriteLine("A minus");
            System.Console.WriteLine("H thresh");
            System.Console.WriteLine("B blur");
            System.Console.WriteLine("L bloom intensity");
            System.Console.WriteLine("S base intensity");
            System.Console.WriteLine("N bloom sat");
            System.Console.WriteLine("M base sat");
            #endregion
            #endif
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            notifier = new MSNotifier(Content.Load<Texture2D>("BlackOut"), new Rectangle(0, 400, 1024, 100), Shape.RECTANGULAR, SpriteBatch, this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                CurrentScreen.Update(gameTime);
                MoodSwing.GetInstance().Notifier.HandleMouseInput();

                KeyboardState newKeyBoardState = Keyboard.GetState();

                #if DEBUG
                #region bloom tweaking
                if (oldKeyboardState.IsKeyDown(Keys.Q) && newKeyBoardState.IsKeyUp(Keys.Q))
                {
                    if (oldKeyboardState.IsKeyDown(Keys.H))
                    {
                        bloom.Settings.BloomThreshold += 0.1f;
                        System.Console.WriteLine("Bloom Thresh: " + bloom.Settings.BloomThreshold);
                    }
                    else if (oldKeyboardState.IsKeyDown(Keys.B))
                    {
                        bloom.Settings.BlurAmount += 0.1f;

                        System.Console.WriteLine("Blur: " + bloom.Settings.BlurAmount);
                    }

                    else if (oldKeyboardState.IsKeyDown(Keys.L))
                    {
                        bloom.Settings.BloomIntensity += 0.1f;
                        System.Console.WriteLine("Bloom Intensity: " + bloom.Settings.BloomIntensity);
                    }
                    else if (oldKeyboardState.IsKeyDown(Keys.S))
                    {
                        bloom.Settings.BaseIntensity += 0.1f;

                        System.Console.WriteLine("Base Intensity: " + bloom.Settings.BaseIntensity);
                    }

                    else if (oldKeyboardState.IsKeyDown(Keys.N))
                    {
                        bloom.Settings.BloomSaturation += 0.1f;
                        System.Console.WriteLine("Bloom Sat: " + bloom.Settings.BloomSaturation);
                    }
                    else if (oldKeyboardState.IsKeyDown(Keys.M))
                    {
                        bloom.Settings.BaseSaturation += 0.1f;

                        System.Console.WriteLine("Base Sat: " + bloom.Settings.BaseSaturation);
                    }
                }
                else if (oldKeyboardState.IsKeyDown(Keys.A) && newKeyBoardState.IsKeyUp(Keys.A))
                {
                    if (oldKeyboardState.IsKeyDown(Keys.H))
                    {
                        bloom.Settings.BloomThreshold -= 0.1f;
                        System.Console.WriteLine("Bloom Thresh: " + bloom.Settings.BloomThreshold);
                    }
                    else if (oldKeyboardState.IsKeyDown(Keys.B))
                    {
                        bloom.Settings.BlurAmount -= 0.1f;
                        System.Console.WriteLine("Blur: " + bloom.Settings.BlurAmount);
                    }
                    else if (oldKeyboardState.IsKeyDown(Keys.L))
                    {
                        bloom.Settings.BloomIntensity -= 0.1f;

                        System.Console.WriteLine("Bloom Intensity: " + bloom.Settings.BloomIntensity);
                    }

                    else if (oldKeyboardState.IsKeyDown(Keys.S))
                    {
                        bloom.Settings.BaseIntensity -= 0.1f;
                        System.Console.WriteLine("Base Intensity: " + bloom.Settings.BaseIntensity);
                    }
                    else if (oldKeyboardState.IsKeyDown(Keys.N))
                    {
                        bloom.Settings.BloomSaturation -= 0.1f;
                        
                        System.Console.WriteLine("Bloom Sat: " + bloom.Settings.BloomSaturation);
                    }
                    else if (oldKeyboardState.IsKeyDown(Keys.M))
                    {
                        bloom.Settings.BaseSaturation -= 0.1f;
                        System.Console.WriteLine("Base Sat: " + bloom.Settings.BaseSaturation);
                    }
                }
                #endregion
                #endif

                oldKeyboardState = newKeyBoardState;
                prevGameTime = gameTime;

                if(!(CurrentScreen is MSDistrictScreen) || !(CurrentScreen as MSDistrictScreen).Paused)
                    base.Update(gameTime);
                
                MoodSwing.GetInstance().Notifier.Update(gameTime);

                if (MediaPlayer.State == MediaState.Stopped && !(CurrentScreen is MSIntroScreen))
                {
                    Song nextSong = bgm.Dequeue();
                    MediaPlayer.Play(nextSong);
                    bgm.Enqueue(nextSong);
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            MSResolution.BeginDraw();
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, MSResolution.GetTransformationMatrix());
            CurrentScreen.Draw(gameTime);

            //int frameRate = (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            //spriteBatch.DrawString(Content.Load<SpriteFont>("ToolTipFont"), "Frame Rate: " + frameRate + "fps", new Vector2(5, 735), Color.White);
            MoodSwing.GetInstance().Notifier.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
