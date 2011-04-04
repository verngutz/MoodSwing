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

namespace MoodSwingGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MoodSwing : Game
    {
        private GraphicsDeviceManager graphics;

        public bool FullScreen 
        { 
            get { return graphics.IsFullScreen; } 
            set 
            { 
                graphics.IsFullScreen = value;
                graphics.ApplyChanges();
            } 
        }

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

        private MouseState oldMouseState;
        public MouseState OldMouseState { get { return oldMouseState; } }

        private Queue<Song> bgm;
        public Queue<Song> BGM { get { return bgm; } set { bgm = value; } }

        private const int nativeScreenWidth = 1024;
        private const int nativeScreenHeight = 768;
        private Matrix displayScale;
        public Matrix DisplayScale { get { return displayScale; } }

        public GameTime prevGameTime;
        private MoodSwing()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            
            //graphics.PreferMultiSampling = true;

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            Window.Title = "MoodSwing";
            MediaPlayer.Volume = 0.85f;
            bgm = new Queue<Song>();
            bgm.Enqueue(Content.Load<Song>("MoodSwing"));
            bgm.Enqueue(Content.Load<Song>("7thFloor"));
            bgm.Enqueue(Content.Load<Song>("Bliss"));
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
            oldMouseState = Mouse.GetState();
            CurrentScreen = MSIntroScreen.getInstance();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            float horizontalScale = GraphicsDevice.PresentationParameters.BackBufferWidth / nativeScreenWidth;
            float verticalScale = GraphicsDevice.PresentationParameters.BackBufferHeight / nativeScreenHeight;
            float scale = Math.Min(horizontalScale, verticalScale);
            displayScale = Matrix.CreateScale(new Vector3(scale, scale, 1));
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

                KeyboardState newKeyBoardState = Keyboard.GetState();
                MouseState newMouseState = Mouse.GetState();

                oldMouseState = newMouseState;
                oldKeyboardState = newKeyBoardState;
                prevGameTime = gameTime;
                base.Update(gameTime);

                MSNotifier.GetInstance().Update(gameTime);

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
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, displayScale);
            CurrentScreen.Draw(gameTime);

            //int frameRate = (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            //spriteBatch.DrawString(Content.Load<SpriteFont>("ToolTipFont"), "Frame Rate: " + frameRate + "fps", new Vector2(5, 735), Color.White);
            MSNotifier.GetInstance().Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
