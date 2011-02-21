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

        private List<MSGUIUnclickable> components;
        public List<MSGUIUnclickable> Components
        {
            get { return components; }
        }

        private List<MSGUIClickable> clickableComponents;
        public List<MSGUIClickable> ClickableComponents
        {
            get { return clickableComponents; }
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


        public MSPanel(Texture2D background, Rectangle boundingRectangle, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, Color.White, shape, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, Color highlight, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, shape, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, Color.White, shape, spriteBatch, game) { }

        public MSPanel(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, Color highlight, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, shape, spriteBatch, game) 
        {
            this.background = background;
            components = new List<MSGUIUnclickable>();
            clickableComponents = new List<MSGUIClickable>();

            boundedPosition = Position + new Vector2(leftPadding, topPadding);
            boundedSize = Size - new Vector2(leftPadding, topPadding) - new Vector2(rightPadding, bottomPadding);

            this.highlight = highlight;
        }

        public void AddComponent(MSGUIUnclickable component)
        {
            AddComponent(component, Alignment.MANUAL);
        }
        
        public void AddComponent(MSGUIUnclickable component, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TOP_LEFT:
                    component.Position = boundedPosition;
                    break;
                case Alignment.TOP_CENTER:
                    component.Position = boundedPosition + new Vector2((boundedSize.X - component.Size.X) / 2, 0);

                    break;
                case Alignment.TOP_RIGHT:
                    component.Position = boundedPosition + new Vector2(boundedSize.X - component.Size.X, 0);
                    break;
                case Alignment.MIDDLE_LEFT:
                    component.Position = boundedPosition + new Vector2(0, (boundedSize.Y - component.Size.Y) / 2);
                    break;
                case Alignment.MIDDLE_CENTER:
                    component.Position = boundedPosition + (boundedSize - component.Size) / 2;
                    break;
                case Alignment.MIDDLE_RIGHT:
                    component.Position = boundedPosition + new Vector2(boundedSize.X - component.Size.X, (boundedSize.Y - component.Size.Y) / 2);
                    break;
                case Alignment.BOTTOM_LEFT:
                    component.Position = boundedPosition + new Vector2(0, boundedSize.Y - component.Size.Y);
                    break;
                case Alignment.BOTTOM_CENTER:
                    component.Position = boundedPosition + new Vector2((boundedSize.X - component.Size.X) / 2, boundedSize.Y - component.Size.Y);
                    break;
                case Alignment.BOTTOM_RIGHT:
                    component.Position = boundedPosition + new Vector2(boundedSize.X - component.Size.X, boundedSize.Y - component.Size.Y);
                    break;
            }
            components.Add(component);
        }

        public void RemoveComponent(MSGUIUnclickable component)
        {
            components.Remove(component);
        }

        public void AddComponent(MSGUIClickable component)
        {
            AddComponent(component, Alignment.MANUAL);
        }

        public void AddComponent(MSGUIClickable component, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TOP_LEFT:
                    component.Position = boundedPosition;
                    break;
                case Alignment.TOP_CENTER:
                    component.Position = boundedPosition + new Vector2((boundedSize.X - component.Size.X) / 2, 0);

                    break;
                case Alignment.TOP_RIGHT:
                    component.Position = boundedPosition + new Vector2(boundedSize.X - component.Size.X, 0);
                    break;
                case Alignment.MIDDLE_LEFT:
                    component.Position = boundedPosition + new Vector2(0, (boundedSize.Y - component.Size.Y) / 2);
                    break;
                case Alignment.MIDDLE_CENTER:
                    component.Position = boundedPosition + (boundedSize - component.Size) / 2;
                    break;
                case Alignment.MIDDLE_RIGHT:
                    component.Position = boundedPosition + new Vector2(boundedSize.X - component.Size.X, (boundedSize.Y - component.Size.Y) / 2);
                    break;
                case Alignment.BOTTOM_LEFT:
                    component.Position = boundedPosition + new Vector2(0, boundedSize.Y - component.Size.Y);
                    break;
                case Alignment.BOTTOM_CENTER:
                    component.Position = boundedPosition + new Vector2((boundedSize.X - component.Size.X) / 2, boundedSize.Y - component.Size.Y);
                    break;
                case Alignment.BOTTOM_RIGHT:
                    component.Position = boundedPosition + new Vector2(boundedSize.X - component.Size.X, boundedSize.Y - component.Size.Y);
                    break;
            }
            clickableComponents.Add(component);
            clickableComponents = clickableComponents.Reverse<MSGUIClickable>().ToList<MSGUIClickable>();
        }

        public void RemoveComponent(MSGUIClickable component)
        {
            clickableComponents.Remove(component);
        }

        public void AddComponent(MSPanel panel)
        {
            AddComponent(panel, Alignment.MANUAL);
        }

        public void AddComponent(MSPanel panel, Alignment alignment)
        {
            AddComponent(panel as MSGUIClickable, alignment);
            foreach (MSGUIUnclickable component in panel.Components)
                AddComponent(component);
            foreach (MSGUIClickable component in panel.ClickableComponents)
                AddComponent(component);
        }

        public void RemoveComponent(MSPanel panel)
        {
            foreach (MSGUIUnclickable component in panel.Components)
                RemoveComponent(component);
            foreach (MSGUIClickable component in panel.ClickableComponents)
                RemoveComponent(component);

            RemoveComponent(panel as MSGUIClickable);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (MSGUIUnclickable element in components)
                element.Update(gameTime);
            foreach (MSGUIClickable element in clickableComponents)
                element.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (background != null)
                SpriteBatch.Draw(background, BoundingRectangle, highlight);

            foreach(MSGUIUnclickable element in components)
                element.Draw(gameTime);

            foreach (MSGUIClickable element in clickableComponents.Reverse<MSGUIClickable>())
                element.Draw(gameTime);
        }

        public override void LeftClick() { }
        public override void UnLeftClick() { }
        public override void MiddleClick() { }
        public override void UnMiddleClick() { }
        public override void RightClick() { }
        public override void UnRightClick() { }
        public override void Hover() { }
        public override void UnHover() { }
        public override void HandleKeyboardInput(KeyboardState oldKeyboardState) { }
    }
}
