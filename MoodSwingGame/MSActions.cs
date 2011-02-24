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

    public class BuyTower : MSAction
    {
        private MSBuyableBuilding toBuy;

        public BuyTower(MSBuyableBuilding toBuy)
        {
            this.toBuy = toBuy;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSResourceManager.TOWER_COST)
            {
                screen.ResourceManager.Funds -= MSResourceManager.TOWER_COST;
                screen.Map.MapArray[toBuy.Row, toBuy.Column] = new MSTower(moodSwing.Content.Load<Model>("districthall"),
                    moodSwing.Content.Load<Texture2D>("MTextures/building_A"),
                    moodSwing.Content.Load<Effect>("Mood"), 
                    toBuy.Position, 
                    toBuy.Row, 
                    toBuy.Column);

                screen.Map.MapArray[toBuy.Row, toBuy.Column].LightSource = screen.Map.LightSource;
                screen.RemoveComponent(screen.BuyDialog);
            }
        }
    }

    public class BuyVolunteerCenter : MSAction
    {
        private MSBuyableBuilding toBuy;

        public BuyVolunteerCenter(MSBuyableBuilding toBuy)
        {
            this.toBuy = toBuy;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSResourceManager.VOLUNTEER_CENTER_COST)
            {
                screen.ResourceManager.Funds -= MSResourceManager.VOLUNTEER_CENTER_COST;
                screen.ResourceManager.VolunteerCapacity += MSResourceManager.VOLUNTEER_CENTER_GAIN;
                screen.Map.MapArray[toBuy.Row, toBuy.Column] = new MSVolunteerCenter(moodSwing.Content.Load<Model>("districthall"), 
                    moodSwing.Content.Load<Texture2D>("MTextures/volunteer_center"),
                    moodSwing.Content.Load<Effect>("Mood"), 
                    toBuy.Position, 
                    toBuy.Row, 
                    toBuy.Column);
                screen.Map.MapArray[toBuy.Row, toBuy.Column].LightSource = screen.Map.LightSource;
                screen.RemoveComponent(screen.BuyDialog);
            }
        }
    }
}
