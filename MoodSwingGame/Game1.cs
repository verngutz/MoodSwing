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
        KeyboardState oldKeyboardState;
        MouseState oldMouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.IsFullScreen = true;
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
            MainMenu.MAIN_MENU.add(Content.Load<Texture2D>("ChainSaw_unpressed"),
                                    Content.Load<Texture2D>("ChainSaw_pressed"),
                                    Content.Load<Texture2D>("ChainSaw_hover"));
                            
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

           
            KeyboardState newKeyboardState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();
            
            currScreen.sentinel(oldKeyboardState, oldMouseState);
            if (currScreen is OpeningFrame)
            {
                if (newKeyboardState.IsKeyDown(Keys.Escape) && oldKeyboardState.IsKeyUp(Keys.Escape))
                {
                    currScreen = currScreen.next();
                }
            }

            else if (currScreen is MainMenu)
            {
                if (newKeyboardState.IsKeyDown(Keys.Escape) && oldKeyboardState.IsKeyUp(Keys.Escape))
                {
                    //this.Exit();
                }
            }

            oldMouseState = newMouseState; ;
            oldKeyboardState = newKeyboardState;

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
            currScreen.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
