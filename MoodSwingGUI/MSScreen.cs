using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoodSwingCoreComponents;

namespace MoodSwingGUI
{
    public class MSScreen : MSPanel
    {
        public MSScreen(Texture2D background, SpriteBatch spriteBatch, Game game)
            : this(background, 0, 0, 0, 0, Color.White, spriteBatch, game) { }

        public MSScreen(Texture2D background, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, 0, 0, 0, 0, highlight, spriteBatch, game) { }

        public MSScreen(Texture2D background, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : this(background, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, spriteBatch, game) { }

        public MSScreen(Texture2D background, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : base(background, new Vector2(0, 0), new Vector2(game.GraphicsDevice.DisplayMode.Width, game.GraphicsDevice.DisplayMode.Height), topPadding, bottomPadding, leftPadding, rightPadding, spriteBatch, game) { }

        public override bool CheckMouseClick(MouseState oldMouseState)
        {
            MouseState currentMouseState = Mouse.GetState();
            foreach (MS2DClickable element in ClickableElements)
            {
                if (element.CheckMouseClick(oldMouseState))
                {
                    return true;
                }
            }
            return false;
        }


    }
}
