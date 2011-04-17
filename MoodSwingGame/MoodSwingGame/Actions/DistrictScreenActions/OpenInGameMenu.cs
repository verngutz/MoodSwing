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
    public class OpenInGameMenu : MSAction
    {
        public void PerformAction(Game game)
        {
            MSDistrictScreen screen = ((game as MoodSwing).CurrentScreen as MSDistrictScreen);
            screen.Paused = true;
            screen.MainMenuButton.Visible = true;
            screen.OptionsButton.Visible = true;
            screen.ExitButton.Visible = true;
            screen.CloseInGameMenu.Visible = true;
            screen.OpenInGameMenu.Visible = false;
            screen.BlackOutPanel.Visible = true;
        }
    }
}
