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
                "Upgrade To A Children's Hospital",
                "One in seven children in sub-Saharan Africa die before their fifth birthday. "
                + "This district also suffers from such a high rate of child mortality. "
                + "Upgrade to help this district fight against pneumonia, diarrhoea, and many other easily "
                + "preventable and curable sicknesses that cause a lot of child deaths.\n\n"
                + "MDG Target by 2015: Reduce by two thirds, between 1990 and 2015, the under-five mortality rate.",
                MSChildrensHospitalStats.GetInstance(),
                new Rectangle(boundingRectangle.X, boundingRectangle.Y + 38, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/1"),
                Game.Content.Load<Texture2D>("BuyDialog/1clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/1hovered"));

            AddSelection(
                "Upgrade To A Women's Health Center",
                "More than 350,000 women die each year because of complications during pregnancy or childbirth. "
                + "The risk is highest for adolescent girls. To make things worse, not everyone has access to knowledge "
                + "of proper family planning and reproductive health. Upgrade to help this district fight against "
                + "maternal mortality.\n\n"
                + "MDG Targets by 2015:\n 1.Reduce by three quarters, between 1990 and 2015, the maternal mortality ratio.\n"
                + "2.Achieve, by 2015, universal access to reproductive health.",
                MSWomensHealthCenterStats.GetInstance(),
                new Rectangle(boundingRectangle.X + 187, boundingRectangle.Y + 129, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/5"),
                Game.Content.Load<Texture2D>("BuyDialog/5clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/5hovered"));

            AddSelection(
                "Upgrade To An Epidemics Clinic and Research Center",
                "",
                MSEpidemicsClinicStats.GetInstance(),
                new Rectangle(boundingRectangle.X, boundingRectangle.Y + 129, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/8"),
                Game.Content.Load<Texture2D>("BuyDialog/8clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/8hovered"));
        }
    }
}
