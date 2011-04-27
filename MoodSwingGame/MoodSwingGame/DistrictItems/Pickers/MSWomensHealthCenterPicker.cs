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
    public class MSWomensHealthCenterPicker : MSCircularPicker
    {
        public MSWomensHealthCenterPicker(Texture2D background, Rectangle boundingRectangle, MSChangeableBuilding toBuy, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, 78, 78, 62, 62, toBuy, Shape.RECTANGULAR, spriteBatch, game)
        {
            AddMiddleOrb();
            String title = "";
            String description = "";
            MSTowerStats stat = null;

            MSDistrictScreen.DistrictName name = ((Game as MoodSwing).CurrentScreen as MSDistrictScreen).Name;
            if (name == MSDistrictScreen.DistrictName.SADNESS)
            {
                title = "Upgrade to a Maternal Care Center Equipped with Mobile Units";
                stat = MSMaternalCareCenterStats.GetInstance();
            }
            else if (name == MSDistrictScreen.DistrictName.ANGER)
            {
                title = "Upgrade to a Fistula Treatment Center";
                stat = MSFistulaTreatmentCenterStats.GetInstance();
            }

            AddSelection(
                title,
                description,
                stat,
                    new Rectangle(boundingRectangle.X + 55, boundingRectangle.Y + 130, 152, 78),
                Game.Content.Load<Texture2D>("BuyDialog/5"),
                Game.Content.Load<Texture2D>("BuyDialog/5clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/5hovered"));
        }
    }
}
