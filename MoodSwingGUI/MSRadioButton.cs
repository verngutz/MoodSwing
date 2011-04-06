using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGUI
{
    public class MSRadioButton : MSCheckbox
    {
        private MSRadioButtonGroup group;

        public MSRadioButton(MSButton unticked, MSButton ticked, bool isTicked, MSRadioButtonGroup group)
            : base(unticked, ticked, isTicked) 
        {
            this.group = group;
        }

        public override void UnLeftClick()
        {
            if (IsTicked)
                current.UnLeftClickNoAction();
            else
            {
                base.UnLeftClick();
                if (group.CurrentTicked != null)
                {
                    group.CurrentTicked.IsTicked = false;
                    group.CurrentTicked.UnHover();
                }
                group.CurrentTicked = this;
            }
        }
    }
}
