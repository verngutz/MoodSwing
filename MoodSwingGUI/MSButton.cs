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
        private MSButtonState currentState;
        private Texture2D clickedTexture;
        private Texture2D hoveredTexture;
        private Texture2D unclickedTexture;
        private Color highlight;
        private MSLabel label;
        private MSAction action;
        private Vector2 scale;
        private Shape shape;

        public MSButton(MSLabel label, MSAction action, float x, float y, float width, float height, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Shape shape, Game game)
            : this(label, action, new Vector2(x, y), new Vector2(width, height), unclicked, clicked, hovered, spriteBatch, Color.White, shape, game) { }

        public MSButton(MSLabel label, MSAction action, float x, float y, float width, float height, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game)
            : this(label, action, new Vector2(x, y), new Vector2(width, height), unclicked, clicked, hovered, spriteBatch, highlight, shape, game) { }

        public MSButton(MSLabel label, MSAction action, Rectangle boundingRectangle, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Shape shape, Game game)
            : this(label, action, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), unclicked, clicked, hovered, spriteBatch, Color.White, shape, game) { }

        public MSButton(MSLabel label, MSAction action, Rectangle boundingRectangle, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game)
            : this(label, action, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), unclicked, clicked, hovered, spriteBatch, highlight, shape, game) { }

        public MSButton(MSLabel label, MSAction action, Vector2 position, Vector2 size, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Shape shape, Game game)
            : this(label, action, position, size, unclicked, clicked, hovered, spriteBatch, Color.White, shape, game) { }

        public MSButton(MSLabel label, MSAction action, Vector2 position, Vector2 size, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game)
            : base(position, size, spriteBatch, game)
        {
            unclickedTexture = unclicked;
            clickedTexture = clicked;
            hoveredTexture = hovered;
            this.highlight = highlight;
            currentState = 0;
            this.label = label;
            this.action = action;
            scale = Size / new Vector2(unclicked.Width, unclicked.Height);
            this.shape = shape;
        }

        public override void Draw(GameTime gameTime)
        {
            switch (currentState)
            {
                case MSButtonState.CLICKED:
                    SpriteBatch.Draw(clickedTexture, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    break;
                case MSButtonState.HOVERED:
                    SpriteBatch.Draw(hoveredTexture, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    break;
                case MSButtonState.UNCLICKED:
                    SpriteBatch.Draw(unclickedTexture, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    break;
            }

            if( label != null ) label.Draw(gameTime);
            base.Draw(gameTime);

        }

        private bool IsHoveredByMouse(MouseState currentMouseState)
        {
            switch (shape)
            {
                case Shape.RECTANGULAR:
                    return !(currentMouseState.X < Position.X || currentMouseState.X > Position.X + Size.X || currentMouseState.Y < Position.Y || currentMouseState.Y > Position.Y + Size.Y);
                case Shape.CIRCULAR:
                    return (Vector2.Distance(Position + Size / 2, new Vector2(currentMouseState.X, currentMouseState.Y)) <= Size.X/2);
            }
            return false;
        }

        public bool CheckMouseClick(MouseState oldMouseState)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (IsHoveredByMouse(currentMouseState))
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && 
                    oldMouseState.LeftButton == ButtonState.Released)
                {
                    currentState = MSButtonState.CLICKED;
                }
                else if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    currentState = MSButtonState.HOVERED;
                    if (oldMouseState.LeftButton == ButtonState.Pressed)
                    {
                        action.PerformAction(Game);
                        return true;
                    }
                }
            }
            else
                currentState = MSButtonState.UNCLICKED;
            return false;
        }
    }
}
