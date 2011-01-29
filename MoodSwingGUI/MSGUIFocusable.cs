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
    public abstract class MSGUIFocusable : MSGUIClickable
    {
        protected bool hasFocus;

        public MSGUIFocusable(Vector2 position, Vector2 size, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(position, size, shape, spriteBatch, game) { }

        public override bool CheckMouseClick(MouseState oldMouseState)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState.LeftButton == ButtonState.Pressed
                && oldMouseState.LeftButton == ButtonState.Released)
            {
                if (CollidesWithMouse())
                {
                    hasFocus = true;
                }
                else
                {
                    hasFocus = false;
                }
            }
            return hasFocus;
        }
    }
}
