﻿using System;
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
            AddMiddleOrb();
            AddSelection(
                "Upgrade to a Water Reservoir and Treatment Plant",
                "There are some places in the world where one cannot access safe drinking water, "
                + "and this district is one of them. The local government's promise to provide potable water "
                + "still remains a promise after such a long time. Complete the upgrade to help "
                + "the public get the water they need and deserve, and fulfill the environment sustainability. "
                + "goal for this district.\n\n"
                + "MDG Target by 2015: Halve the proportion of the population without sustainable access "
                + "to safe drinking water.",
                MSWaterTreatmentPlantStats.GetInstance(),
                    new Rectangle(boundingRectangle.X + 55, boundingRectangle.Y + 130, 152, 78),
                Game.Content.Load<Texture2D>("BuyDialog/Upgrade"),
                Game.Content.Load<Texture2D>("BuyDialog/UpgradeClicked"),
                Game.Content.Load<Texture2D>("BuyDialog/UpgradeHovered"));
        }
    }
}
