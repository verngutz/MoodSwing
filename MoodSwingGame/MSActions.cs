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

    public class BuyTower : MSAction
    {
        private MSBuyableBuilding toBuy;
        private MSTowerStats toBuildStats;

        public BuyTower(MSBuyableBuilding toBuy, MSTowerStats toBuildStats)
        {
            this.toBuy = toBuy;
            this.toBuildStats = toBuildStats;
        }

        public void PerformAction(Game game)
        {
            MoodSwing moodSwing = (MoodSwing)game;
            MSDistrictScreen screen = moodSwing.CurrentScreen as MSDistrictScreen;
            if (screen.ResourceManager.Funds >= MSResourceManager.TOWER_MONEY_COST
                && screen.ResourceManager.IdleVolunteers >= toBuildStats.GetCapacity())
            {
                screen.ResourceManager.Funds -= MSResourceManager.TOWER_MONEY_COST;
                screen.ResourceManager.IdleVolunteers -= toBuildStats.GetCapacity();
                MS3DTile futureSelf = MSTowerFactory.CreateMSTower(toBuildStats, toBuy.Position, toBuy.TileCoordinate);

                futureSelf.LightSource = screen.Map.LightSource;

                MSVolunteerCenter center = screen.Map.GetNearestVolunteerCenter(toBuy);
                Node path = screen.Map.GetPath(center.TileCoordinate, toBuy.TileCoordinate);
                toBuy.StartBuildProcess(toBuildStats.GetCapacity(), futureSelf);

                for (int i = 0; i < MSFoodCenterStats.GetInstance().GetCapacity(); i++)
                {
                    MSWorker worker = new MSWorker(MoodSwing.getInstance().Content.Load<Model>("person"),
                        MoodSwing.getInstance().Content.Load<Texture2D>("MTextures/tao"),
                        MoodSwing.getInstance().Content.Load<Effect>("Mood"),
                        center.Position + new Vector3(0,0, 20) , path, MSTypes.EDUCATION, toBuy );
                    MSUnitHandler.GetInstance().AddWorker(worker);
                }
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
            if (!MSUnitHandler.GetInstance().IsLeaderBusy &&
                screen.ResourceManager.Funds >= MSResourceManager.VOLUNTEER_CENTER_COST)
            {
                screen.ResourceManager.Funds -= MSResourceManager.VOLUNTEER_CENTER_COST;
                //screen.ResourceManager.VolunteerCapacity += MSResourceManager.VOLUNTEER_CENTER_GAIN;
                MS3DTile futureSelf = new MSVolunteerCenter(moodSwing.Content.Load<Model>("districthall"), 
                    moodSwing.Content.Load<Texture2D>("MTextures/volunteer_center"),
                    moodSwing.Content.Load<Effect>("Mood"), 
                    toBuy.Position, 
                    toBuy.Row, 
                    toBuy.Column);
                futureSelf.LightSource = screen.Map.LightSource;
                MSVolunteerCenter center = screen.Map.GetNearestVolunteerCenter(toBuy);
                Node path = screen.Map.GetPath(center.TileCoordinate, toBuy.TileCoordinate);
                toBuy.StartBuildProcess(1, futureSelf);

                MSWorker worker = new MSWorker(MoodSwing.getInstance().Content.Load<Model>("person"),
                        MoodSwing.getInstance().Content.Load<Texture2D>("MTextures/tao"),
                        MoodSwing.getInstance().Content.Load<Effect>("Mood"),
                        center.Position + new Vector3(0, 0, 20), path, MSTypes.EDUCATION, toBuy);
                MSUnitHandler.GetInstance().AddWorker(worker);
                MSUnitHandler.GetInstance().IsLeaderBusy = true;
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
                MS3DTile futureSelf = new MSFundraiser(moodSwing.Content.Load<Model>("districthall"),
                    moodSwing.Content.Load<Texture2D>("MTextures/fundraiser"),
                    moodSwing.Content.Load<Effect>("Mood"),
                    toBuy.Position,
                    toBuy.Row,
                    toBuy.Column,
                    screen.ResourceManager);
                futureSelf.LightSource = screen.Map.LightSource;

                MSVolunteerCenter center = screen.Map.GetNearestVolunteerCenter(toBuy);
                Node path = screen.Map.GetPath(center.TileCoordinate, toBuy.TileCoordinate);
                toBuy.StartBuildProcess(MSResourceManager.FUNDRAISER_VOLUNTEER_COST, futureSelf);

                for (int i = 0; i < MSResourceManager.FUNDRAISER_VOLUNTEER_COST; i++)
                {
                    MSWorker worker = new MSWorker(MoodSwing.getInstance().Content.Load<Model>("person"),
                            MoodSwing.getInstance().Content.Load<Texture2D>("MTextures/tao"),
                            MoodSwing.getInstance().Content.Load<Effect>("Mood"),
                            center.Position + new Vector3(0, 0, 20), path, MSTypes.EDUCATION, toBuy);
                    MSUnitHandler.GetInstance().AddWorker(worker);
                }
                screen.RemoveComponent(screen.BuyDialog);
            }
        }
    }
}
