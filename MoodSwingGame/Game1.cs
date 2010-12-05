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

namespace MoodSwingGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public SpriteBatch spriteBatch;
        public SpriteBatch Sprite
        {
            get { return spriteBatch; }
        }

        GraphicsDeviceManager graphics;
        MoodSwingScreen currScreen;
        public KeyboardState oldState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            oldState = Keyboard.GetState();
            //currScreen = OpeningFrame.OPENING_FRAME;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            OpeningFrame.INIT(Content.Load<Texture2D>("OpeningScreen"), this, spriteBatch);
            MainMenu.INIT(Content.Load<Texture2D>("MainMenu"), this, spriteBatch);

            currScreen = OpeningFrame.OPENING_FRAME;
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState newState = Keyboard.GetState();

            if (currScreen is OpeningFrame)
            {
                if (newState.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
                {
                    currScreen = currScreen.next();
                }
            }

            else if (currScreen is MainMenu)
            {
                if (newState.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
                {
                    //this.Exit();
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //currScreen.Draw(gameTime);
            // spriteBatch.Draw(texture, rect, Color.White);
            currScreen.draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
