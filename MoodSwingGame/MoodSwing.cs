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

        private static MoodSwing MSInstance;
        public static MoodSwing getInstance() 
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

        private MoodSwing()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
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
            CurrentScreen.Update(gameTime);
            KeyboardState newKeyBoardState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();

            oldMouseState = newMouseState;
            oldKeyboardState = newKeyBoardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            CurrentScreen.Draw(gameTime);

            int frameRate = (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            spriteBatch.DrawString(Content.Load<SpriteFont>("Temp"), "Frame Rate: " + frameRate + "fps", new Vector2(5, 735), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
