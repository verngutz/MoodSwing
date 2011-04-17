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
    public class InitiateGameOverWinSequence : MSAction
    {
        public void PerformAction(Game game)
        {
            MSDistrictScreen screen = ((game as MoodSwing).CurrentScreen as MSDistrictScreen);
            screen.Paused = true;
            screen.BlackOutPanel.Visible = true;
            if (screen.BuyDialog != null)
                screen.RemoveComponent(screen.BuyDialog);
            screen.HandleMouseInput((game as MoodSwing).OldMouseState, false);
            MoodSwing.GetInstance().Notifier.ClearNotifications();
            MoodSwing.GetInstance().Notifier.InvokeNotification("You win!\nYou have successfully achieved the eight Millenium Development Goals.");
            MoodSwing.GetInstance().Notifier.FreezeNotifications = true;
        }
    }
}
