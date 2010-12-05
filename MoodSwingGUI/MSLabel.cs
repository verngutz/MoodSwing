﻿using System;
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


namespace MoodSwingGUI
{
    public class MSLabel : MS2DComponent
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
            get { return currentText; }
        }

        private SpriteFont spriteFont;
        private Color color;
        private Vector2 fontScale;

        public MSLabel(String text, Rectangle boundingRectangle, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, spriteBatch, game)
        {
            Text = text;
            this.spriteFont = spriteFont;
            color = Color.White;
        }

        public MSLabel(String text, Rectangle boundingRectangle, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, spriteBatch, game)
        {
            Text = text;
            this.spriteFont = spriteFont;
            this.color = color;
        }

        public MSLabel(String text, Vector2 position, Vector2 size, SpriteFont spriteFont, SpriteBatch spriteBatch, Game game)
            :base(position, size, spriteBatch, game)
        {
            Text = text;
            this.spriteFont = spriteFont;
            color = Color.White;
        }

        public MSLabel(String text, Vector2 position, Vector2 size, SpriteFont spriteFont, Color color, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game)
        {
            Text = text;
            this.spriteFont = spriteFont;
            this.color = color;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(spriteFont, Text, Position, color, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
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
                this.fontScale = Size / spriteFont.MeasureString(Text);
            }
            base.Update(gameTime);
        }
    }
}