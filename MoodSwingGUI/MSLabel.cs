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


namespace MoodSwingGUI
{
    public class MSLabel:DrawableGameComponent
    {
        private String previousText;
        private String currentText;
        public String Text
        {
            set
            {
                if (currentText != null)
                {
                    previousText = String.Copy(currentText);
                }
                currentText = value;
            }
            get
            {
                return currentText;
            }
        }

        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Vector2 size;
        private SpriteFont spriteFont;
        private Color color;

        public MSLabel(String text, Rectangle boundingRectangle, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : base(game)
        {
            Text = text;
            position = new Vector2(boundingRectangle.X, boundingRectangle.Y);
            size = new Vector2(boundingRectangle.Width, boundingRectangle.Height);
            this.spriteFont = spriteFont;
            this.color = color;
            this.spriteBatch = spriteBatch;
        }

        public MSLabel(String text, Vector2 position, Vector2 size, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            :base(game)
        {
            Text = text;
            this.position = position;
            this.size = size;
            this.spriteFont = spriteFont;
            this.color = color;
            this.spriteBatch = spriteBatch;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(spriteFont, Text, position, color);
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (!currentText.Equals(previousText))
            {
            }
            base.Update(gameTime);
        }

        
    }
}
