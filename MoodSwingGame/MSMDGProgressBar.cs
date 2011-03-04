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
    public class MSMDGProgressBar : MSGUIUnclickable
    {
        private Texture2D borderTexture;
        private Texture2D fillTexture;
        private Texture2D fullTexture;
        private MSProgressBar.Orientation orientation;
        public double Progress { get; set; }

        public MSMDGProgressBar(Rectangle bounding_rectangle, SpriteBatch sprite_batch, Game game,
            Texture2D border_texture, Texture2D fill_texture, Texture2D full_texture, MSProgressBar.Orientation orientation)
            : base(bounding_rectangle, sprite_batch, game)
        {
            this.borderTexture = border_texture;
            this.fillTexture = fill_texture;
            this.fullTexture = full_texture;
            this.orientation = orientation;
            Progress = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle fillRectangle = new Rectangle();
            switch (orientation)
            {
                case MSProgressBar.Orientation.HORIZONTAL:
                    fillRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)(Progress * Size.X), (int)Size.Y);
                    break;
                case MSProgressBar.Orientation.VERTICAL:
                    fillRectangle = new Rectangle((int)Position.X, (int)(Position.Y + (1 - Progress) * Size.Y), (int)(Size.X), (int)(Progress * Size.Y));
                    break;
            }

            spriteBatch.Draw(fillTexture, fillRectangle, Color.White);

            if (borderTexture != null)
                spriteBatch.Draw(borderTexture, BoundingRectangle, Color.White);

            if (fullTexture != null && Progress >= 1)
                spriteBatch.Draw(fullTexture, new Rectangle(BoundingRectangle.X, 0, BoundingRectangle.Width, 91), Color.White);

            base.Draw(gameTime);
        }
    }
}
