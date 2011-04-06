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
    public class MSRadioButtonGroup : MSGUIClickable
    {
        private List<MSRadioButton> radioButtons;
        public MSRadioButton CurrentTicked { get; set; }

        private MSPanel container;

        public MSRadioButtonGroup(MSPanel container)
            : base(container.BoundingRectangle, container.Shape, null, container.SpriteBatch, container.Game) 
        {
            this.container = container;
            radioButtons = new List<MSRadioButton>();
        }

        public void AddRadioButton(MSRadioButton button)
        {
            AddRadioButton(button, Alignment.MANUAL);
        }

        public void AddRadioButton(MSRadioButton button, Alignment alignment)
        {
            radioButtons.Add(button);
            container.AddComponent(button, alignment);
            if (button.IsTicked)
            {
                if (CurrentTicked != null)
                    CurrentTicked.IsTicked = false;
                CurrentTicked = button;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            container.Draw(gameTime);
            foreach (MSCheckbox button in radioButtons)
            {
                button.Draw(gameTime);
            }
        }

        public override void LeftClick() { }
        public override void UnLeftClick() { }
        public override void MiddleClick() { }
        public override void UnMiddleClick() { }
        public override void RightClick() { }
        public override void UnRightClick() { }
        public override void Hover() { }
        public override void UnHover() { }
    }
}
