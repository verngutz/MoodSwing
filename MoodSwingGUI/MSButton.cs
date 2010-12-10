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
using MoodSwingCoreComponents;

namespace MoodSwingGUI
{
    public class MSButton : MS2DComponent
    {
        enum MSButtonState {
            UNCLICKED = 0,
            CLICKED,
            HOVERED
        }

        private MSButtonState currentState;
        private Texture2D clickedTexture;
        private Texture2D hoveredTexture;
        private Texture2D unclickedTexture;
        private Color highlight;
        private MSLabel label;
        private MSAction action;
        private Vector2 scale;

        public MSButton(Game g, MSLabel l, MSAction a, int x, int y, int width, int height,
            Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch sb, Color hlight )
            : base( new Vector2(x,y), new Vector2(width, height), sb, g)
        {
            unclickedTexture = unclicked;
            clickedTexture = clicked;
            hoveredTexture = hovered;
            highlight = hlight;
            currentState = 0;
            label = l;
            action = a;
            scale = Size / new Vector2(unclicked.Width, unclicked.Height);
        }


        public MSButton(Game g, MSLabel l, MSAction a, Vector2 pos, Vector2 size,
            Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch sb, Color hlight)
            : base(pos, size, sb, g)
        {
            unclickedTexture = unclicked;
            clickedTexture = clicked;
            hoveredTexture = hovered;
            highlight = hlight;
            currentState = 0;
            label = l;
            action = a;
            scale = Size / new Vector2(unclicked.Width, unclicked.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            Texture2D currTexture = null;
            switch (currentState)
            {
                case MSButtonState.CLICKED:
                    currTexture = clickedTexture;
                    break;
                case MSButtonState.HOVERED:
                    currTexture = hoveredTexture;
                    break;
                case MSButtonState.UNCLICKED:
                    currTexture = unclickedTexture;
                    break;
            }

            this.spriteBatch.Draw(currTexture, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            if( label != null ) label.Draw(gameTime);
            base.Draw(gameTime);

        }

        public void chechMouseToButtonCollision(MouseState oldMouseState)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState.X >= this.Position.X && currentMouseState.X <= this.Position.X + this.Size.X
             && currentMouseState.Y >= this.Position.Y && currentMouseState.Y <= this.Position.Y + this.Size.Y)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && 
                    oldMouseState.LeftButton == ButtonState.Released)
                {
                    currentState = MSButtonState.CLICKED;
                }
                else if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    currentState = MSButtonState.HOVERED;
                    if( oldMouseState.LeftButton == ButtonState.Pressed ) 
                        action.PerformAction(Game);
                }
            }
            else
                currentState = MSButtonState.UNCLICKED;
        }
    }
}
