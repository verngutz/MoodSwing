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
    public class MSGeneralRefugePicker : MSCircularPicker
    {
        public MSGeneralRefugePicker(Texture2D background, Rectangle boundingRectangle, MSChangeableBuilding toBuy, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, 78, 78, 62, 62, toBuy, Shape.RECTANGULAR, spriteBatch, game)
        {
            AddSelection(
                "Upgrade To Include A Feeding Center For The Poor",
                "One in four children in Mood City are underweight, and a majority of the children "
                + "in this district is a part of that fourth. Upgrade this refuge to include a food donations center "
                + "for those poor people.\n\n" 
                + "MDG Target by 2015: Halve the proportion of the population who suffer from hunger.",
                MSGeneralRefugeStats.GetInstance(),
                new Rectangle(boundingRectangle.X + 38, boundingRectangle.Y, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/2"),
                Game.Content.Load<Texture2D>("BuyDialog/2clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/2hovered"));

            AddSelection(
                "Upgrade to a center for women empowerment",
                "",
                MSWomenCenterStats.GetInstance(),
                new Rectangle(boundingRectangle.X + 129, boundingRectangle.Y, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/3"),
                Game.Content.Load<Texture2D>("BuyDialog/3clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/3hovered"));

            AddSelection(
                "Upgrade to a tutorial center for the masses",
                "",
                MSTutorialCenterStats.GetInstance(),
                new Rectangle(boundingRectangle.X + 187, boundingRectangle.Y + 38, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/4"),
                Game.Content.Load<Texture2D>("BuyDialog/4clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/4hovered"));
        }
    }
}
