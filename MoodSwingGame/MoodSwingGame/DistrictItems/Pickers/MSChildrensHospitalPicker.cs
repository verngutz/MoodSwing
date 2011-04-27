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
using MoodSwingGUI;

namespace MoodSwingGame
{
    public class MSChildrensHospitalPicker : MSCircularPicker
    {
        public MSChildrensHospitalPicker(Texture2D background, Rectangle boundingRectangle, MSChangeableBuilding toBuy, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, 78, 78, 62, 62, toBuy, Shape.RECTANGULAR, spriteBatch, game)
        {
            AddMiddleOrb();

            String title = "Equip With An Immunization Outreach Center";
            String description = "One of the best ways to fight child mortality is to ensure that children get vaccination for diseases such as polio.";
            MSTowerStats stat = null;

            MSDistrictScreen.DistrictName name = ((Game as MoodSwing).CurrentScreen as MSDistrictScreen).Name;
            switch (name)
            {
                case MSDistrictScreen.DistrictName.SADNESS:
                    stat = MSVaccinationCenterStats.GetInstance();
                    break;
                case MSDistrictScreen.DistrictName.ANGER:
                    stat = MSImmunizationOutreachStats.GetInstance();
                    break;
            }

            AddSelection(
                title,
                description,
                stat,
                new Rectangle(boundingRectangle.X + 60, boundingRectangle.Y + 135, 152, 78),
                Game.Content.Load<Texture2D>("BuyDialog/Upgrade"),
                Game.Content.Load<Texture2D>("BuyDialog/UpgradeClicked"),
                Game.Content.Load<Texture2D>("BuyDialog/UpgradeHovered"));

            
        }
    }
}
