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
    public class MoodSwing : Microsoft.Xna.Framework.Game
    {
        private static MoodSwing MSInstance;
        public static MoodSwing getInstance() 
        {
            if (MSInstance == null)
                MSInstance = new MoodSwing();
            return MSInstance;
        }

        private SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        GraphicsDeviceManager graphics;

        public MSScreen CurrentScreen { set; get; }

        private KeyboardState oldKeyboardState;
        public KeyboardState OldKeyboardState { get { return oldKeyboardState; } }

        private MouseState oldMouseState;
        public MouseState OldMouseState { get { return oldMouseState; } }

        private Vector2 mouseRHoldButton;


        private MoodSwing()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
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
            IsMouseVisible = true;
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

            //Camera movement using keyboard
            if (newKeyBoardState.IsKeyDown(Keys.Up))
                MSCamera.getInstance().shift(new Vector3(0, 0.3f, 0));
            else if (newKeyBoardState.IsKeyDown(Keys.Down))
                MSCamera.getInstance().shift(new Vector3(0, -0.3f, 0));
            else if (newKeyBoardState.IsKeyDown(Keys.Left))
                MSCamera.getInstance().shift(new Vector3(-.3f, 0, 0));
            else if (newKeyBoardState.IsKeyDown(Keys.Right))
                MSCamera.getInstance().shift(new Vector3(0.3f, 0, 0));

            

            //Camera rotation handler
            if (newMouseState.RightButton == ButtonState.Pressed)
            {
                if (oldMouseState.RightButton == ButtonState.Released)
                    mouseRHoldButton = new Vector2(newMouseState.X, newMouseState.Y);

                MSCamera camera = MSCamera.getInstance();
                Vector2 movement = new Vector2(newMouseState.X, newMouseState.Y) - mouseRHoldButton;

                if (movement.X != 0)
                {
                    if (movement.X > 0)
                    {
                        camera.rotate(new Vector3(0, 0, 1));
                    }
                    else
                        camera.rotate(new Vector3(0, 0, -1));
                }
            }
            else
            {
                //Camera movement using mouse
                if (newMouseState.X >= 0 && newMouseState.X <= 5)
                    MSCamera.getInstance().shift(new Vector3(.3f, 0, 0));
                else if (newMouseState.X <= graphics.GraphicsDevice.Viewport.Width &&
                    newMouseState.X >= graphics.GraphicsDevice.Viewport.Width - 5)
                    MSCamera.getInstance().shift(new Vector3(-.3f, 0, 0));
                else if (newMouseState.Y >= 0 && newMouseState.Y <= 5)
                    MSCamera.getInstance().shift(new Vector3(0, -0.3f, 0));
                else if (newMouseState.Y <= graphics.GraphicsDevice.Viewport.Height &&
                    newMouseState.Y >= graphics.GraphicsDevice.Viewport.Height - 5)
                    MSCamera.getInstance().shift(new Vector3(0, 0.3f, 0));
            }
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            CurrentScreen.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
