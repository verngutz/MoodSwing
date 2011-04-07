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
    public class MSRadioButtonGroup : MSGUIUnclickable
    {
        private List<MSRadioButton> radioButtons;
        public MSRadioButton CurrentTicked { get; set; }

        private MSPanel container;

        public MSRadioButtonGroup(MSPanel container)
            : base(container.BoundingRectangle, container.SpriteBatch, container.Game) 
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
            button.Group = this;
            container.AddComponent(button, alignment);
            if (button.IsTicked)
            {
                if (CurrentTicked != null)
                    CurrentTicked.IsTicked = false;
                CurrentTicked = button;
            }
        }
    }

    public class MSRadioButton : MSCheckbox
    {
        public MSRadioButtonGroup Group { get; set; }

        public MSRadioButton(MSButton unticked, MSButton ticked, bool isTicked)
            : base(unticked, ticked, isTicked) { }

        public override void UnLeftClick()
        {
            if (IsTicked)
                current.UnLeftClickNoAction();
            else
            {
                base.UnLeftClick();
                if (Group.CurrentTicked != null)
                {
                    Group.CurrentTicked.IsTicked = false;
                    Group.CurrentTicked.UnHover();
                }
                Group.CurrentTicked = this;
            }
        }
    }
}
