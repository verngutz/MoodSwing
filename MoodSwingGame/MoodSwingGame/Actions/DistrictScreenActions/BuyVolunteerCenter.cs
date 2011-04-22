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

    public class BuyVolunteerCenter : MSAction
    {
        private MSChangeableBuilding toBuy;
        public BuyVolunteerCenter(MSChangeableBuilding toBuy)
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
                screen.RemoveComponent(screen.CircularPicker);
            }
            else if (MSUnitHandler.GetInstance().IsLeaderBusy)
            {
                MoodSwing.GetInstance().Notifier.InvokeNotification("You may only build one Volunteer Center at a time.");
            }
            else
            {
                MoodSwing.GetInstance().Notifier.InvokeNotification("You need more funds.");
            }
        }
    }

}
