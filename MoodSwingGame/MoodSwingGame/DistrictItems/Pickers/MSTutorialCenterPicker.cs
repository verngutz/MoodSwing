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
    public class MSTutorialCenterPicker : MSCircularPicker
    {
        public MSTutorialCenterPicker(Texture2D background, Rectangle boundingRectangle, MSChangeableBuilding toBuy, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, 78, 78, 62, 62, toBuy, Shape.RECTANGULAR, spriteBatch, game)
        {

            AddMiddleOrb();
            String title = "";
            String description = "";
            MSTowerStats stat = null;

            MSDistrictScreen.DistrictName name = ((Game as MoodSwing).CurrentScreen as MSDistrictScreen).Name;
            if (name == MSDistrictScreen.DistrictName.FEAR)
            {
                title = "Upgrade to a School Supplies and Materials Donation Center";
                stat = MSSuppliesDonationCenterStats.GetInstance();
            }
            else if (name == MSDistrictScreen.DistrictName.SADNESS)
            {
                title = "Upgrade to a Children's Interactive Library";
                stat = MSChildrensLibraryStats.GetInstance();
            }
            else if (name == MSDistrictScreen.DistrictName.ANGER)
            {
                title = "Upgrade to an Education-For-All School";
                stat = MSEFASchoolStats.GetInstance();
            }

            AddSelection(
                title,
                description,
                stat,
                    new Rectangle(boundingRectangle.X + 55, boundingRectangle.Y + 130, 152, 78),
                Game.Content.Load<Texture2D>("BuyDialog/4"),
                Game.Content.Load<Texture2D>("BuyDialog/4clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/4hovered"));
        }
    }
}
