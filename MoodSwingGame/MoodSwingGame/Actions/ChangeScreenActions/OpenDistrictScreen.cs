﻿using System;
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
    public class OpenDistrictScreen : ChangeScreen
    {
        private MSDistrictScreen.DistrictName name;
        public OpenDistrictScreen(MSDistrictScreen.DistrictName name)
        {
            this.name = name;
        }

        public override void PerformAction(Game game)
        {
            base.ChangeToScreen(new MSDistrictScreen(name, game as MoodSwing), game);
        }
    }
}
