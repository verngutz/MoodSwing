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
        private List<MSCheckbox> radioButtons;
        private MSCheckbox currentTicked;

        private MSPanel container;

        public MSRadioButtonGroup(MSPanel container) 
            : base(container.Position, container.Size, container.Shape, container.SpriteBatch, container.Game) 
        {
            this.container = container;
            radioButtons = new List<MSCheckbox>();
        }

        public void AddRadioButton(MSCheckbox button)
        {
            AddRadioButton(button, Alignment.MANUAL);
        }

        public void AddRadioButton(MSCheckbox button, Alignment alignment)
        {
            radioButtons.Add(button);
            container.AddElement(button, alignment);
            if (button.IsTicked)
            {
                if (currentTicked != null)
                {
                    currentTicked.IsTicked = !currentTicked.IsTicked;
                }
                currentTicked = button;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            container.Draw(gameTime);
            foreach (MSCheckbox button in radioButtons)
            {
                button.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

        public override bool CheckMouseClick(MouseState oldMouseState)
        {
            if (CollidesWithMouse())
            {
                foreach (MSCheckbox button in radioButtons)
                {
                    if (button.CheckMouseClick(oldMouseState))
                    {
                        currentTicked.IsTicked = !currentTicked.IsTicked;
                        currentTicked = button;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
