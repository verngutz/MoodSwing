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
    public class CloseInGameMenu : MSAction
    {
        public void PerformAction(Game game)
        {
            MSDistrictScreen screen = ((game as MoodSwing).CurrentScreen as MSDistrictScreen);
            screen.Paused = !(MSMoodManager.GetInstance().IsAlive);
            screen.MainMenuButton.Visible = false;
            screen.OptionsButton.Visible = false;
            screen.ExitButton.Visible = false;
            screen.OpenInGameMenu.Visible = true;
            screen.CloseInGameMenu.Visible = false;
            screen.BlackOutPanel.Visible = !(MSMoodManager.GetInstance().IsAlive);
        }
    }
}
