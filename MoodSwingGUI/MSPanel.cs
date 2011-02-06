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
    public class MSPanel : MSGUITypable
    {
        private Texture2D background;


        private List<MSGUIComponent> elements;
        public List<MSGUIComponent> Elements
        {
            get { return elements; }
        }

        private Stack<MSGUIClickable> clickableElements;
        public Stack<MSGUIClickable> ClickableElements
        {
            get { return clickableElements; }
        }

        private Stack<MSGUITypable> focusableElements;
        public Stack<MSGUITypable> FocusableElements 
        { 
            get { return focusableElements; } 
        }

        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                boundedPosition -= base.Position;
                boundedPosition += value;
                base.Position = value;
            }
        }

        private Vector2 boundedPosition;
        private Vector2 boundedSize;
        private Color highlight;
        private Vector2 scale;

        public MSPanel(Texture2D background, float x, float y, float width, float height, SpriteBatch spriteBatch, Game game)
            : this(background, x, y, width, height, 0, 0, 0, 0, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, float x, float y, float width, float height, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, new Vector2(x, y), new Vector2(width, height), 0, 0, 0, 0, highlight, spriteBatch, game) { }

        public MSPanel(Texture2D background, float x, float y, float width, float height, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : this(background, x, y, width, height, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, float x, float y, float width, float height, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, new Vector2(x, y), new Vector2(width, height), topPadding, bottomPadding, leftPadding, rightPadding, highlight, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), 0, 0, 0, 0, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), topPadding, bottomPadding, leftPadding, rightPadding, spriteBatch, game) { }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, SpriteBatch spriteBatch, Game game)
            : this(background, position, size, 0, 0, 0, 0, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, Color highlight, SpriteBatch spriteBatch, Game game)
            : this(background, position, size, 0, 0, 0, 0, highlight, spriteBatch, game) { }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, float topPadding, float bottomPadding, float leftPadding, float rightPadding, SpriteBatch spriteBatch, Game game)
            : this(background, position, size, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, spriteBatch, game) { }

        public MSPanel(Texture2D background, Vector2 position, Vector2 size, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, SpriteBatch spriteBatch, Game game)
            : base(position, size, Shape.RECTANGULAR, spriteBatch, game)
        {
            this.background = background;
            elements = new List<MSGUIComponent>();
            clickableElements = new Stack<MSGUIClickable>();
            focusableElements = new Stack<MSGUITypable>();

            boundedPosition = Position + new Vector2(leftPadding, topPadding);
            boundedSize = Size - new Vector2(leftPadding, topPadding) - new Vector2(rightPadding, bottomPadding);

            if(background == null)
                scale = Vector2.Zero;
            else
                scale = Size / new Vector2(background.Width, background.Height);

            this.highlight = highlight;
        }

        public void AddElement(MSGUIComponent element)
        {
            AddElement(element, Alignment.MANUAL);
        }
        
        public void AddElement(MSGUIComponent element, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TOP_LEFT:
                    element.Position = boundedPosition;
                    break;
                case Alignment.TOP_CENTER:
                    element.Position = boundedPosition + new Vector2((boundedSize.X - element.Size.X) / 2, 0);

                    break;
                case Alignment.TOP_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, 0);
                    break;
                case Alignment.MIDDLE_LEFT:
                    element.Position = boundedPosition + new Vector2(0, (boundedSize.Y - element.Size.Y) / 2);
                    break;
                case Alignment.MIDDLE_CENTER:
                    element.Position = boundedPosition + (boundedSize - element.Size) / 2;
                    break;
                case Alignment.MIDDLE_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, (boundedSize.Y - element.Size.Y) / 2);
                    break;
                case Alignment.BOTTOM_LEFT:
                    element.Position = boundedPosition + new Vector2(0, boundedSize.Y - element.Size.Y);
                    break;
                case Alignment.BOTTOM_CENTER:
                    element.Position = boundedPosition + new Vector2((boundedSize.X - element.Size.X) / 2, boundedSize.Y - element.Size.Y);
                    break;
                case Alignment.BOTTOM_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, boundedSize.Y - element.Size.Y);
                    break;
            }
            elements.Add(element);
        }

        public void AddElement(MSGUIClickable element)
        {
            AddElement(element, Alignment.MANUAL);
        }

        public void AddElement(MSGUIClickable element, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TOP_LEFT:
                    element.Position = boundedPosition;
                    break;
                case Alignment.TOP_CENTER:
                    element.Position = boundedPosition + new Vector2((boundedSize.X - element.Size.X) / 2, 0);

                    break;
                case Alignment.TOP_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, 0);
                    break;
                case Alignment.MIDDLE_LEFT:
                    element.Position = boundedPosition + new Vector2(0, (boundedSize.Y - element.Size.Y) / 2);
                    break;
                case Alignment.MIDDLE_CENTER:
                    element.Position = boundedPosition + (boundedSize - element.Size) / 2;
                    break;
                case Alignment.MIDDLE_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, (boundedSize.Y - element.Size.Y) / 2);
                    break;
                case Alignment.BOTTOM_LEFT:
                    element.Position = boundedPosition + new Vector2(0, boundedSize.Y - element.Size.Y);
                    break;
                case Alignment.BOTTOM_CENTER:
                    element.Position = boundedPosition + new Vector2((boundedSize.X - element.Size.X) / 2, boundedSize.Y - element.Size.Y);
                    break;
                case Alignment.BOTTOM_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, boundedSize.Y - element.Size.Y);
                    break;
            }
            clickableElements.Push(element);
        }

        public void AddElement(MSGUITypable element)
        {
            AddElement(element, Alignment.MANUAL);
        }

        public void AddElement(MSGUITypable element, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TOP_LEFT:
                    element.Position = boundedPosition;
                    break;
                case Alignment.TOP_CENTER:
                    element.Position = boundedPosition + new Vector2((boundedSize.X - element.Size.X) / 2, 0);

                    break;
                case Alignment.TOP_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, 0);
                    break;
                case Alignment.MIDDLE_LEFT:
                    element.Position = boundedPosition + new Vector2(0, (boundedSize.Y - element.Size.Y) / 2);
                    break;
                case Alignment.MIDDLE_CENTER:
                    element.Position = boundedPosition + (boundedSize - element.Size) / 2;
                    break;
                case Alignment.MIDDLE_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, (boundedSize.Y - element.Size.Y) / 2);
                    break;
                case Alignment.BOTTOM_LEFT:
                    element.Position = boundedPosition + new Vector2(0, boundedSize.Y - element.Size.Y);
                    break;
                case Alignment.BOTTOM_CENTER:
                    element.Position = boundedPosition + new Vector2((boundedSize.X - element.Size.X) / 2, boundedSize.Y - element.Size.Y);
                    break;
                case Alignment.BOTTOM_RIGHT:
                    element.Position = boundedPosition + new Vector2(boundedSize.X - element.Size.X, boundedSize.Y - element.Size.Y);
                    break;
            }
            clickableElements.Push(element);
            focusableElements.Push(element);
        }

        public override bool CheckMouseClick(MouseState oldMouseState)
        {
            System.Console.WriteLine("Panel " + Position + " is checked.");
            foreach (MSGUIClickable element in clickableElements)
                if (element.CheckMouseClick(oldMouseState))
                    return true;

            hasFocus = CollidesWithMouse();
            return hasFocus;
        }

        public override bool CheckKeyboardInput(KeyboardState oldKeyboardState)
        {
            if(hasFocus)
                foreach (MSGUITypable element in focusableElements)
                    if (element.CheckKeyboardInput(oldKeyboardState))
                        return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (MSGUIComponent element in elements)
                element.Update(gameTime);
            foreach (MSGUIClickable element in clickableElements)
                element.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (background != null)
                SpriteBatch.Draw(background, Position, null, highlight, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

            foreach(MSGUIComponent element in elements)
                element.Draw(gameTime);

            foreach (MSGUIClickable element in clickableElements.Reverse<MSGUIClickable>())
                element.Draw(gameTime);
        }
    }
}
