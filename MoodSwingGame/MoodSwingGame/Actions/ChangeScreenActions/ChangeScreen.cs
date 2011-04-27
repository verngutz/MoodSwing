using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using MoodSwingGUI;
using MoodSwingCoreComponents;

namespace MoodSwingGame
{
    public abstract class ChangeScreen : MSAction
    {
        public abstract void PerformAction(Game game);

        protected void ChangeToScreen(MSScreen screen, Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen.ResetHovers();
            moodSwing.CurrentScreen.HasFocus = false;
            moodSwing.CurrentScreen = screen;
            moodSwing.CurrentScreen.HasFocus = true;
            moodSwing.Notifier.Reset();
        }
    }
}
