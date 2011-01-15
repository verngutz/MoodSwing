using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using MoodSwingCoreComponents;

namespace MoodSwingGame
{
    public class Exit : MSAction
    {
        public override void PerformAction(Game game)
        {
            game.Exit();
        }
    }
    public class OpenMainScreen : MSAction
    {
        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen = MSMainScreen.getInstance();
        }
    }

    public class OpenCityScreen : MSAction
    {
        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen = MSCityScreen.getInstance();
        }
    }

    public class OpenOptionsScreen : MSAction
    {
        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSOptionsScreen.getInstance().ReturnScreen = moodSwing.CurrentScreen;
            moodSwing.CurrentScreen = MSOptionsScreen.getInstance();
        }
    }

    public class ReturnFromOptionsScreen : MSAction
    {
        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen = MSOptionsScreen.getInstance().ReturnScreen;
        }
    }

    public class OpenDistrictScreen : MSAction
    {
        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen = MSDistrictScreen.getInstance();
                
        }
    }
}
