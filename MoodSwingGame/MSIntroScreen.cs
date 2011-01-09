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
using MoodSwingGUI;

namespace MoodSwingGame
{
    public class MSIntroScreen : MSScreen
    {
        private static MSIntroScreen singleton;
        public static MSIntroScreen getInstance(MoodSwing game)
        {
            if (singleton == null)
                singleton = new MSIntroScreen(game.SpriteBatch, game);
            return singleton;
        }

        private Video introVideo;
        private VideoPlayer videoPlayer;

        private MSIntroScreen(SpriteBatch spriteBatch, MoodSwing game) : base(null, spriteBatch, game) 
        {
            videoPlayer = new VideoPlayer();
            this.LoadContent();
        }

        protected override void LoadContent()
        {
            introVideo = Game.Content.Load<Video>("MoodSwingIntro");
            videoPlayer.Play(introVideo);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (videoPlayer.State == MediaState.Stopped)
            {
                (Game as MoodSwing).CurrentState = MoodSwing.State.MAIN;
                (Game as MoodSwing).CurrentScreen = MSMainScreen.getInstance(Game as MoodSwing);
            }
            base.Update(gameTime);
        }

        public override bool CheckMouseClick(MouseState oldMouseState)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState.LeftButton == ButtonState.Pressed
                && oldMouseState.LeftButton == ButtonState.Released)
            {
                videoPlayer.Stop();
                return true;
            }
            return false;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(videoPlayer.GetTexture(), new Rectangle(0, 0, 1024, 768), Color.White);
            base.Draw(gameTime);
        }
    }
}
