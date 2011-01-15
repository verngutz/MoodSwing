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
            moodSwing.CurrentScreen = MSMainScreen.getInstance(moodSwing);
        }
    }

    public class OpenCityScreen : MSAction
    {
        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen = MSCityScreen.getInstance(moodSwing);
        }
    }

    public class OpenOptionsScreen : MSAction
    {
        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSOptionsScreen.getInstance(moodSwing).ReturnScreen = moodSwing.CurrentScreen;
            moodSwing.CurrentScreen = MSOptionsScreen.getInstance(moodSwing);
        }
    }

    public class ReturnFromOptionsScreen : MSAction
    {
        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen = MSOptionsScreen.getInstance(moodSwing).ReturnScreen;
        }
    }
}
