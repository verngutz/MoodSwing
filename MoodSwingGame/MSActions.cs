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
            MSMoodManager.Reset();
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
    
    public class InitiateGameOverLoseSequence : MSAction
    {
        public void PerformAction(Game game)
        {
            MSDistrictScreen screen = ((game as MoodSwing).CurrentScreen as MSDistrictScreen);
            screen.Paused = true;
            screen.BlackOutPanel.Visible = true;
            if(screen.BuyDialog != null)
                screen.RemoveComponent(screen.BuyDialog);
            MSNotifier.GetInstance().ClearNotifications();
            MSNotifier.GetInstance().InvokeNotification("Try Again\n\tThe district went into a wild uproar before you could achieve the Millenium Development Goals.");
        }
    }

    public class InitiateGameOverWinSequence : MSAction
    {
        public void PerformAction(Game game)
        {
            MSDistrictScreen screen = ((game as MoodSwing).CurrentScreen as MSDistrictScreen);
            screen.Paused = true;
            screen.BlackOutPanel.Visible = true;
            if (screen.BuyDialog != null)
                screen.RemoveComponent(screen.BuyDialog);
            MSNotifier.GetInstance().ClearNotifications();
            MSNotifier.GetInstance().InvokeNotification("You win!\n\tYou have successfully achieved the eight Millenium Development Goals.");
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
            screen.Paused = !(MSMoodManager.GetInstance().IsAlive);
            screen.MainMenuButton.Visible = false;
            screen.OptionsButton.Visible = false;
            screen.ExitButton.Visible = false;
            screen.OpenInGameMenu.Visible = true;
            screen.CloseInGameMenu.Visible = false;
            screen.BlackOutPanel.Visible = !(MSMoodManager.GetInstance().IsAlive);
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
            if (screen.ResourceManager.Funds >= toBuildStats.GetFundsCost())
            {
                if (screen.ResourceManager.IdleVolunteers >= toBuildStats.GetVolunteerCost())
                {
                    screen.ResourceManager.Funds -= toBuildStats.GetFundsCost();
                    screen.ResourceManager.IdleVolunteers -= toBuildStats.GetVolunteerCost();
                    MS3DTile futureSelf = MSTowerFactory.CreateMSTower(toBuildStats, toBuy.Position, toBuy.Rotation, toBuy.TileCoordinate);
                    
                    futureSelf.LightSource = screen.Map.LightSource;
                    toBuy.StartBuildProcess(toBuildStats.GetVolunteerCost(), futureSelf);

                    MSUnitHandler.GetInstance().SendWorkers(screen.Map, toBuy, toBuildStats.GetVolunteerCost());
                    screen.RemoveComponent(screen.BuyDialog);
                }
                else
                {
                    MSNotifier.GetInstance().InvokeNotification("You need more volunteers.");
                }
            }
            else
            {
                MSNotifier.GetInstance().InvokeNotification("You need more funds.");
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
                screen.ResourceManager.Funds >= MSVolunteerCenterStats.GetInstance().GetFundsCost())
            {
                screen.ResourceManager.Funds -= MSVolunteerCenterStats.GetInstance().GetFundsCost();
                //screen.ResourceManager.VolunteerCapacity += MSResourceManager.VOLUNTEER_CENTER_GAIN;
                MS3DTile futureSelf = new MSVolunteerCenter(moodSwing.Content.Load<Model>("MModels/BuildingBig"),
                    moodSwing.Content.Load<Texture2D>("MTextures/BuildingVolunteer"),
                    moodSwing.Content.Load<Effect>("Mood"),
                    toBuy.Position,
                    toBuy.Rotation,
                    toBuy.Row,
                    toBuy.Column);
                futureSelf.LightSource = screen.Map.LightSource;
                toBuy.StartBuildProcess(1, futureSelf);

                MSUnitHandler.GetInstance().SendWorkers(screen.Map, toBuy, 1);
                MSUnitHandler.GetInstance().IsLeaderBusy = true;
                screen.RemoveComponent(screen.BuyDialog);
            }
            else if (MSUnitHandler.GetInstance().IsLeaderBusy)
            {
                MSNotifier.GetInstance().InvokeNotification("John Doe is currently busy.");
            }
            else
            {
                MSNotifier.GetInstance().InvokeNotification("You need more funds.");
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
            if (screen.ResourceManager.Funds >= MSFundraiserStats.GetInstance().GetFundsCost())
            {
                if (screen.ResourceManager.IdleVolunteers >= MSFundraiserStats.GetInstance().GetVolunteerCost())
                {
                    screen.ResourceManager.Funds -= MSFundraiserStats.GetInstance().GetFundsCost();
                    screen.ResourceManager.IdleVolunteers -= MSFundraiserStats.GetInstance().GetVolunteerCost();
                    MS3DTile futureSelf = new MSFundraiser(moodSwing.Content.Load<Model>("MModels/BuildingBig"),
                        moodSwing.Content.Load<Texture2D>("MTextures/BuildingFunds"),
                        moodSwing.Content.Load<Effect>("Mood"),
                        toBuy.Position,
                        toBuy.Rotation,
                        toBuy.Row,
                        toBuy.Column,
                        screen.ResourceManager);
                    futureSelf.LightSource = screen.Map.LightSource;
                    toBuy.StartBuildProcess(MSFundraiserStats.GetInstance().GetVolunteerCost(), futureSelf);

                    MSUnitHandler.GetInstance().SendWorkers(screen.Map, toBuy, 
                        MSFundraiserStats.GetInstance().GetVolunteerCost());
                    screen.RemoveComponent(screen.BuyDialog);
                }
                else
                {
                    MSNotifier.GetInstance().InvokeNotification("You need more volunteers.");
                }
            }
            else
            {
                MSNotifier.GetInstance().InvokeNotification("You need more funds.");
            }
        }
    }
}
