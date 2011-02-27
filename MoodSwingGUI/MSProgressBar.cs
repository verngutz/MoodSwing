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

namespace MoodSwingGUI
{
    public class MSProgressBar : MSGUIUnclickable
    {
        private Texture2D borderTexture;
        private Texture2D loadingProgressTexture;
        public double Progress { get; set; }

        public MSProgressBar(Vector2 position, Vector2 size, SpriteBatch spriteBatch, Game game,
            Texture2D borderTexture, Texture2D loadingProgressTexture)
            : this(new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), 
            spriteBatch, game, borderTexture, loadingProgressTexture) { }


        public MSProgressBar(Rectangle boundingRectangle, SpriteBatch spriteBatch, Game game,
            Texture2D borderTexture, Texture2D loadingProgressTexture)
            : base(boundingRectangle, spriteBatch, game)
        {
            this.borderTexture = borderTexture;
            this.loadingProgressTexture = loadingProgressTexture;
            Progress = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(borderTexture, boundingRectangle, Color.White);
            spriteBatch.Draw(loadingProgressTexture, 
                new Rectangle((int)Position.X, (int)Position.Y, (int)(Progress * Size.X), (int)Size.Y), Color.White);
            base.Draw(gameTime);
        }
    }
}
