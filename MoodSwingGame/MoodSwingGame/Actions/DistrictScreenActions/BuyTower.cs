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
    public class BuyTower : MSAction
    {
        private MSChangeableBuilding toBuy;
        private MSTowerStats toBuildStats;

        public BuyTower(MSChangeableBuilding toBuy, MSTowerStats toBuildStats)
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
                    screen.RemoveComponent(screen.CircularPicker);
                }
                else
                {
                    MoodSwing.GetInstance().Notifier.InvokeNotification("You need more volunteers.");
                }
            }
            else
            {
                MoodSwing.GetInstance().Notifier.InvokeNotification("You need more funds.");
            }
        }
    }
}
