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
    public class MSWomenCenterPicker : MSCircularPicker
    {
        public MSWomenCenterPicker(Texture2D background, Rectangle boundingRectangle, MSChangeableBuilding toBuy, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, 78, 78, 62, 62, toBuy, Shape.RECTANGULAR, spriteBatch, game)
        {
            String title = "";
            String description = "";
            MSTowerStats stat = null;

            MSDistrictScreen.DistrictName name = ((Game as MoodSwing).CurrentScreen as MSDistrictScreen).Name;
            if (name == MSDistrictScreen.DistrictName.FEAR)
            {
                title = "Upgrade to a \"White Screens\" Center for Corporate Equity";
                stat = MSWhiteScreensCenterStats.GetInstance();
            }
            else if (name == MSDistrictScreen.DistrictName.SADNESS)
            {
                title = "Upgrade to a Training Center for Aspiring Women Politicians";
                stat = MSWomenPoliticiansCenterStats.GetInstance();
            }
            else if (name == MSDistrictScreen.DistrictName.ANGER)
            {
                title = "Upgrade to a Women's Open University";
                stat = MSWomensUniversityStats.GetInstance();
            }

            AddSelection(
                title,
                description,
                stat,
                new Rectangle(boundingRectangle.X + 129, boundingRectangle.Y, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/3"),
                Game.Content.Load<Texture2D>("BuyDialog/3clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/3hovered"));
        }
    }
}
