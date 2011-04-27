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

using MoodSwingGUI;
using MoodSwingCoreComponents;

namespace MoodSwingGame
{
    public class InitiateGameOverSequence : MSAction
    {
        private bool win;

        public InitiateGameOverSequence(bool win)
        {
            this.win = win;
        }
        public void PerformAction(Game game)
        {
            MSDistrictScreen screen = ((game as MoodSwing).CurrentScreen as MSDistrictScreen);
            screen.Paused = true;
            screen.BlackOutPanel.Visible = true;
            if (screen.CircularPicker != null)
                screen.RemoveComponent(screen.CircularPicker);
            screen.HandleMouseInput(false);
            MoodSwing.GetInstance().Notifier.ClearComponents();
            MoodSwing.GetInstance().Notifier.ClearNotifications();

            if(win)
                MoodSwing.GetInstance().Notifier.InvokeNotification("You win!\nYou have successfully achieved the Millenium Development Goals for this district.");
            else
                MoodSwing.GetInstance().Notifier.InvokeNotification("Try Again\nThe district went into a wild uproar before you could achieve the Millenium Development Goals.");

            MoodSwing.GetInstance().Notifier.FreezeNotifications = true;
            if (MoodSwing.GetInstance().CurrentScreen is MSDistrictScreen)
                MSDistrictScreen.clearSave();
        }
    }
}
