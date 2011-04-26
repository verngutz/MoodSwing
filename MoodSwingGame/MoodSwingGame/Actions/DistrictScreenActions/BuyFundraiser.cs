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
    public class BuyFundraiser : MSAction
    {
        private MSChangeableBuilding toBuy;

        public BuyFundraiser(MSChangeableBuilding toBuy)
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
                    MS3DTile futureSelf = new MSFundraiser("MModels/BuildingBig",
                        "MTextures/BuildingFunds",
                        "Mood",
                        toBuy.Position,
                        toBuy.Rotation,
                        toBuy.Row,
                        toBuy.Column,
                        screen.ResourceManager);
                    futureSelf.LightSource = screen.Map.LightSource;
                    toBuy.StartBuildProcess(MSFundraiserStats.GetInstance().GetVolunteerCost(), futureSelf);

                    MSUnitHandler.GetInstance().SendWorkers(screen.Map, toBuy,
                        MSFundraiserStats.GetInstance().GetVolunteerCost());
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
