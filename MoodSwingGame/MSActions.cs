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

using MoodSwingCoreComponents;

namespace MoodSwingGame
{
    public class Exit : MSAction
    {
        public void PerformAction(Game game)
        {
            game.Exit();
        }
    }
    public class OpenMainScreen : MSAction
    {
        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen.ResetHovers();
            moodSwing.CurrentScreen = MSMainScreen.getInstance();
        }
    }

    public class OpenCityScreen : MSAction
    {
        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen.ResetHovers();
            moodSwing.CurrentScreen = MSCityScreen.getInstance();
        }
    }

    public class OpenOptionsScreen : MSAction
    {
        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSOptionsScreen.getInstance().ReturnScreen = moodSwing.CurrentScreen;
            moodSwing.CurrentScreen.ResetHovers();
            moodSwing.CurrentScreen = MSOptionsScreen.getInstance();
        }
    }

    public class ReturnFromOptionsScreen : MSAction
    {
        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen.ResetHovers();
            moodSwing.CurrentScreen = MSOptionsScreen.getInstance().ReturnScreen;
        }
    }

    public class OpenDistrictScreen : MSAction
    {
        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            moodSwing.CurrentScreen.ResetHovers();
            moodSwing.CurrentScreen = new MSDistrictScreen(@"Content\mapinfo.txt", moodSwing);
        }
    }

    public class BuildTower : MSAction
    {
        MS3DTile toBuy;
        public BuildTower(MSBuyableBuilding toBuy)
        {
            this.toBuy = toBuy;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSTower.PRICE)
            {
                screen.ResourceManager.Funds -= MSTower.PRICE;
                screen.Map.MapArray[toBuy.Row, toBuy.Column] = new MSTower(MoodSwing.getInstance().Content.Load<Model>("districthall"), MoodSwing.getInstance().Content.Load<Texture2D>("MTextures/building_texture"),
                        MoodSwing.getInstance().Content.Load<Effect>("Mood"), toBuy.Position, toBuy.Row, toBuy.Column);
                screen.Map.MapArray[toBuy.Row, toBuy.Column].LightSource = screen.Map.LightSource;
                screen.RemoveComponent(screen.BuyDialog);
            }
        }
    }
}
