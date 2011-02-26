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
            MSMoodManager.GetInstance().Reset();
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

    public class CloseInGameMenu : MSAction
    {
        public void PerformAction(Game game)
        {
            MSDistrictScreen screen = ((game as MoodSwing).CurrentScreen as MSDistrictScreen);
            screen.Paused = false;
            screen.MainMenuButton.Visible = false;
            screen.OptionsButton.Visible = false;
            screen.ExitButton.Visible = false;
            screen.OpenInGameMenu.Visible = true;
            screen.CloseInGameMenu.Visible = false;
            screen.BlackOutPanel.Visible = false;
        }
    }

    public class BuyFoodCenter : MSAction
    {
        private MSBuyableBuilding toBuy;

        public BuyFoodCenter(MSBuyableBuilding toBuy)
        {
            this.toBuy = toBuy;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSResourceManager.TOWER_MONEY_COST
                && screen.ResourceManager.IdleVolunteers >= MSFoodCenterStats.GetInstance().GetCapacity())
            {
                screen.ResourceManager.Funds -= MSResourceManager.TOWER_MONEY_COST;
                screen.ResourceManager.IdleVolunteers -= MSFoodCenterStats.GetInstance().GetCapacity();
                screen.Map.MapArray[toBuy.Row, toBuy.Column] = new MSTower(moodSwing.Content.Load<Model>("districthall"),
                    moodSwing.Content.Load<Texture2D>("MTextures/building_A"),
                    moodSwing.Content.Load<Effect>("Mood"), 
                    toBuy.Position, 
                    toBuy.Row, 
                    toBuy.Column,
                    MSFoodCenterStats.GetInstance());

                screen.Map.MapArray[toBuy.Row, toBuy.Column].LightSource = screen.Map.LightSource;
                screen.RemoveComponent(screen.BuyDialog);
            }
        }
    }

    public class BuyTutorialCenter : MSAction
    {
        private MSBuyableBuilding toBuy;

        public BuyTutorialCenter(MSBuyableBuilding toBuy)
        {
            this.toBuy = toBuy;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSResourceManager.TOWER_MONEY_COST
                && screen.ResourceManager.IdleVolunteers >= MSTutorialCenterStats.GetInstance().GetCapacity())
            {
                screen.ResourceManager.Funds -= MSResourceManager.TOWER_MONEY_COST;
                screen.ResourceManager.IdleVolunteers -= MSTutorialCenterStats.GetInstance().GetCapacity();
                screen.Map.MapArray[toBuy.Row, toBuy.Column] = new MSTower(moodSwing.Content.Load<Model>("districthall"),
                    moodSwing.Content.Load<Texture2D>("MTextures/building_B"),
                    moodSwing.Content.Load<Effect>("Mood"),
                    toBuy.Position,
                    toBuy.Row,
                    toBuy.Column,
                    MSTutorialCenterStats.GetInstance());

                screen.Map.MapArray[toBuy.Row, toBuy.Column].LightSource = screen.Map.LightSource;
                screen.RemoveComponent(screen.BuyDialog);
            }
        }
    }

    public class BuyWomensOrg : MSAction
    {
        private MSBuyableBuilding toBuy;

        public BuyWomensOrg(MSBuyableBuilding toBuy)
        {
            this.toBuy = toBuy;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSResourceManager.TOWER_MONEY_COST
                && screen.ResourceManager.IdleVolunteers >= MSWomensOrgStats.GetInstance().GetCapacity())
            {
                screen.ResourceManager.Funds -= MSResourceManager.TOWER_MONEY_COST;
                screen.ResourceManager.IdleVolunteers -= MSWomensOrgStats.GetInstance().GetCapacity();
                screen.Map.MapArray[toBuy.Row, toBuy.Column] = new MSTower(moodSwing.Content.Load<Model>("districthall"),
                    moodSwing.Content.Load<Texture2D>("MTextures/building_C"),
                    moodSwing.Content.Load<Effect>("Mood"),
                    toBuy.Position,
                    toBuy.Row,
                    toBuy.Column,
                    MSWomensOrgStats.GetInstance());

                screen.Map.MapArray[toBuy.Row, toBuy.Column].LightSource = screen.Map.LightSource;
                screen.RemoveComponent(screen.BuyDialog);
            }
        }
    }

    public class BuyHealthCenter : MSAction
    {
        private MSBuyableBuilding toBuy;

        public BuyHealthCenter(MSBuyableBuilding toBuy)
        {
            this.toBuy = toBuy;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSResourceManager.TOWER_MONEY_COST
                && screen.ResourceManager.IdleVolunteers >= MSHealthCenterStats.GetInstance().GetCapacity())
            {
                screen.ResourceManager.Funds -= MSResourceManager.TOWER_MONEY_COST;
                screen.ResourceManager.IdleVolunteers -= MSHealthCenterStats.GetInstance().GetCapacity();
                screen.Map.MapArray[toBuy.Row, toBuy.Column] = new MSTower(moodSwing.Content.Load<Model>("districthall"),
                    moodSwing.Content.Load<Texture2D>("MTextures/building_D"),
                    moodSwing.Content.Load<Effect>("Mood"),
                    toBuy.Position,
                    toBuy.Row,
                    toBuy.Column,
                    MSHealthCenterStats.GetInstance());

                screen.Map.MapArray[toBuy.Row, toBuy.Column].LightSource = screen.Map.LightSource;
                screen.RemoveComponent(screen.BuyDialog);
            }
        }
    }

    public class BuyEcoPark : MSAction
    {
        private MSBuyableBuilding toBuy;

        public BuyEcoPark(MSBuyableBuilding toBuy)
        {
            this.toBuy = toBuy;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSResourceManager.TOWER_MONEY_COST
                && screen.ResourceManager.IdleVolunteers >= MSEcoParkStats.GetInstance().GetCapacity())
            {
                screen.ResourceManager.Funds -= MSResourceManager.TOWER_MONEY_COST;
                screen.ResourceManager.IdleVolunteers -= MSEcoParkStats.GetInstance().GetCapacity();
                screen.Map.MapArray[toBuy.Row, toBuy.Column] = new MSTower(moodSwing.Content.Load<Model>("districthall"),
                    moodSwing.Content.Load<Texture2D>("MTextures/building_E"),
                    moodSwing.Content.Load<Effect>("Mood"),
                    toBuy.Position,
                    toBuy.Row,
                    toBuy.Column,
                    MSEcoParkStats.GetInstance());

                screen.Map.MapArray[toBuy.Row, toBuy.Column].LightSource = screen.Map.LightSource;
                screen.RemoveComponent(screen.BuyDialog);
            }
        }
    }

    public class BuyGlobalCenter : MSAction
    {
        private MSBuyableBuilding toBuy;

        public BuyGlobalCenter(MSBuyableBuilding toBuy)
        {
            this.toBuy = toBuy;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSResourceManager.TOWER_MONEY_COST
                && screen.ResourceManager.IdleVolunteers >= MSGlobalCenterStats.GetInstance().GetCapacity())
            {
                screen.ResourceManager.Funds -= MSResourceManager.TOWER_MONEY_COST;
                screen.ResourceManager.IdleVolunteers -= MSGlobalCenterStats.GetInstance().GetCapacity();
                screen.Map.MapArray[toBuy.Row, toBuy.Column] = new MSTower(moodSwing.Content.Load<Model>("districthall"),
                    moodSwing.Content.Load<Texture2D>("MTextures/building_B"),
                    moodSwing.Content.Load<Effect>("Mood"),
                    toBuy.Position,
                    toBuy.Row,
                    toBuy.Column,
                    MSGlobalCenterStats.GetInstance());

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

    public class BuyFundraiser : MSAction
    {
        private MSBuyableBuilding toBuy;

        public BuyFundraiser(MSBuyableBuilding toBuy)
        {
            this.toBuy = toBuy;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSResourceManager.FUNDRAISER_MONEY_COST
                && screen.ResourceManager.IdleVolunteers >= MSResourceManager.FUNDRAISER_VOLUNTEER_COST)
            {
                screen.ResourceManager.Funds -= MSResourceManager.FUNDRAISER_MONEY_COST;
                screen.ResourceManager.IdleVolunteers -= MSResourceManager.FUNDRAISER_VOLUNTEER_COST;
                screen.Map.MapArray[toBuy.Row, toBuy.Column] = new MSFundraiser(moodSwing.Content.Load<Model>("districthall"),
                    moodSwing.Content.Load<Texture2D>("MTextures/fundraiser"),
                    moodSwing.Content.Load<Effect>("Mood"),
                    toBuy.Position,
                    toBuy.Row,
                    toBuy.Column,
                    screen.ResourceManager);
                screen.Map.MapArray[toBuy.Row, toBuy.Column].LightSource = screen.Map.LightSource;
                screen.RemoveComponent(screen.BuyDialog);
            }
        }
    }
}
