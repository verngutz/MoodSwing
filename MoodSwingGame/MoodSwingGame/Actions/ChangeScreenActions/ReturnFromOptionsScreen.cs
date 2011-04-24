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
    public class ReturnFromOptionsScreen : ChangeScreen
    {
        private static ReturnFromOptionsScreen instance;
        public static ReturnFromOptionsScreen GetInstance()
        {
            if (instance == null)
                instance = new ReturnFromOptionsScreen();
            return instance;
        }
        private ReturnFromOptionsScreen() { }

        public override void PerformAction(Game game)
        {
            base.ChangeToScreen(MSOptionsScreen.GetInstance().ReturnScreen, game);
        }
    }
}
