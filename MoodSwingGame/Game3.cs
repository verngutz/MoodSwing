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

namespace MoodSwingGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game3 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D bg;
        Texture2D fg;
        MSPanel main;
        MSPanel p1;
        MSPanel p2;
        MSPanel p3;
        MSPanel p4;
        MSPanel p5;
        MSPanel p6;
        MSPanel p7;
        MSPanel p8;
        MSPanel p9;

        public Game3()
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bg = Content.Load<Texture2D>("193719,xcitefun-windows-7-wallpapers-3");
            fg = Content.Load<Texture2D>("stewie-griffin-middle-finger");
            main = new MSPanel(bg, new Rectangle(50, 50, 400, 400), 25, 25, 25, 25, spriteBatch, this);
            p1 = new MSPanel(fg, new Rectangle(0, 0, 50, 100), 0, 0, 0, 0, spriteBatch, this);
            p2 = new MSPanel(fg, new Rectangle(0, 0, 50, 100), 0, 0, 0, 0, spriteBatch, this);
            p3 = new MSPanel(fg, new Rectangle(0, 0, 50, 100), 0, 0, 0, 0, spriteBatch, this);
            p4 = new MSPanel(fg, new Rectangle(0, 0, 50, 100), 0, 0, 0, 0, spriteBatch, this);
            p5 = new MSPanel(fg, new Rectangle(0, 0, 50, 100), 0, 0, 0, 0, spriteBatch, this);
            p6 = new MSPanel(fg, new Rectangle(0, 0, 50, 100), 0, 0, 0, 0, spriteBatch, this);
            p7 = new MSPanel(fg, new Rectangle(0, 0, 50, 100), 0, 0, 0, 0, spriteBatch, this);
            p8 = new MSPanel(fg, new Rectangle(0, 0, 50, 100), 0, 0, 0, 0, spriteBatch, this);
            p9 = new MSPanel(fg, new Rectangle(0, 0, 50, 100), 0, 0, 0, 0, spriteBatch, this);
            main.AddElement(p1, Alignment.TOP_LEFT);
            main.AddElement(p2, Alignment.TOP_CENTER);
            main.AddElement(p3, Alignment.TOP_RIGHT);
            main.AddElement(p4, Alignment.MIDDLE_LEFT);
            main.AddElement(p5, Alignment.MIDDLE_CENTER);
            main.AddElement(p6, Alignment.MIDDLE_RIGHT);
            main.AddElement(p7, Alignment.BOTTOM_LEFT);
            main.AddElement(p8, Alignment.BOTTOM_CENTER);
            main.AddElement(p9, Alignment.BOTTOM_RIGHT);
            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here

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
            main.Draw(gameTime);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

