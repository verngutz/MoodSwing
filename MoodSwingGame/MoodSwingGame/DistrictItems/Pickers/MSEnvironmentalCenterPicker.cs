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
    public class MSEnvironmentalCenterPicker : MSCircularPicker
    {
        public MSEnvironmentalCenterPicker(Texture2D background, Rectangle boundingRectangle, MSChangeableBuilding toBuy, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, 78, 78, 62, 62, toBuy, Shape.RECTANGULAR, spriteBatch, game)
        {
            AddSelection(
                "Upgrade to a Water Reservoir and Treatment Plant",
                MSWaterTreatmentPlantStats.GetInstance(),
                new Rectangle(boundingRectangle.X + 129, boundingRectangle.Y + 187, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/6"),
                Game.Content.Load<Texture2D>("BuyDialog/6clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/6hovered"));

            AddMiddleOrb("Environmental Awareness Center");
        }
    }
}
