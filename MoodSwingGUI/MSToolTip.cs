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
    public class MSToolTip : MSGUIUnclickable
    {
        private Texture2D background;

        protected List<MSGUIUnclickable> components;
        public List<MSGUIUnclickable> Components { get { return components; } }

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

        public MSToolTip(Texture2D background, Rectangle bounding_rectangle, SpriteBatch sprite_batch, Game game)
            : this(background, bounding_rectangle, 0, 0, 0, 0, sprite_batch, game) { }

        public MSToolTip(Texture2D background, Rectangle bounding_rectangle, int top_padding, int bottom_padding, int left_padding, int right_padding, SpriteBatch sprite_batch, Game game)
            : base(bounding_rectangle, sprite_batch, game)
        {
            this.background = background;
            components = new List<MSGUIUnclickable>();

            boundedPosition = Position + new Vector2(left_padding, top_padding);
            boundedSize = Size - new Vector2(left_padding, top_padding) - new Vector2(right_padding, bottom_padding);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (background != null)
                SpriteBatch.Draw(background, BoundingRectangle, Color.White);

            foreach (MSGUIUnclickable component in components)
            {
                if(component.Visible)
                    component.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (MSGUIUnclickable component in components)
                if (component.Visible)
                    component.Update(gameTime);
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

        /**
        public void AddComponent(MSToolTip tool_tip)
        {
            AddComponent(tool_tip, Alignment.MANUAL);
        }

        public void AddComponent(MSToolTip tool_tip, Alignment alignment)
        {
            AddComponent(tool_tip as MSGUIUnclickable, alignment);

            foreach (MSGUIUnclickable component in tool_tip.Components)
                AddComponent(component);
        }

        public void RemoveComponent(MSToolTip tool_tip)
        {
            foreach (MSGUIUnclickable component in tool_tip.Components)
                RemoveComponent(component);

            RemoveComponent(tool_tip as MSGUIUnclickable);
        }
         */
    }
}
