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
    public class OpenMainScreen : ChangeScreen
    {
        private static OpenMainScreen instance;
        public static OpenMainScreen GetInstance()
        {
            if (instance == null)
                instance = new OpenMainScreen();
            return instance;
        }
        private OpenMainScreen() { }

        public override void PerformAction(Game game)
        {
            base.ChangeToScreen(MSMainScreen.getInstance(), game);
            if ((game as MoodSwing).CurrentScreen is MSDistrictScreen)
                ((game as MoodSwing).CurrentScreen as MSDistrictScreen).save();
        }
    }
}
