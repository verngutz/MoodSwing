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
    public class MSGeneralClinicPicker : MSCircularPicker
    {
        public MSGeneralClinicPicker(Texture2D background, Rectangle boundingRectangle, MSChangeableBuilding toBuy, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, 78, 78, 62, 62, toBuy, Shape.RECTANGULAR, spriteBatch, game)
        {
            AddSelection(
                "Upgrade to a children's hospital",
                MSChildrensHospitalStats.GetInstance(),
                new Rectangle(boundingRectangle.X, boundingRectangle.Y + 38, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/1"),
                Game.Content.Load<Texture2D>("BuyDialog/1clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/1hovered"));

            AddSelection(
                "Upgrade to a women's health center",
                MSWomensHealthCenterStats.GetInstance(),
                new Rectangle(boundingRectangle.X + 187, boundingRectangle.Y + 129, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/5"),
                Game.Content.Load<Texture2D>("BuyDialog/5clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/5hovered"));

            AddSelection(
                "Upgrade to an epidemics clinic and research center",
                MSEpidemicsClinicStats.GetInstance(),
                new Rectangle(boundingRectangle.X, boundingRectangle.Y + 129, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/8"),
                Game.Content.Load<Texture2D>("BuyDialog/8clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/8hovered"));

            AddMiddleOrb("General Clinic");
        }
    }
}
