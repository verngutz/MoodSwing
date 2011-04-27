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
    public class OpenOptionsScreen : ChangeScreen
    {
        private static OpenOptionsScreen instance;
        public static OpenOptionsScreen GetInstance()
        {
            if (instance == null)
                instance = new OpenOptionsScreen();
            return instance;
        }
        private OpenOptionsScreen() { }

        public override void PerformAction(Game game)
        {
            (game as MoodSwing).Notifier.InvokeNotification("This feature is not yet available in this version of the game.");
            /**
            MSOptionsScreen.GetInstance().ReturnScreen = MoodSwing.GetInstance().CurrentScreen;
            base.ChangeToScreen(MSOptionsScreen.GetInstance(), game);
             */
        }
    }
}
