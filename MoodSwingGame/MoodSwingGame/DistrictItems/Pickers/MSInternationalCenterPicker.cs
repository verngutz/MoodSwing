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
    public class MSInternationalCenterPicker : MSCircularPicker
    {
        public MSInternationalCenterPicker(Texture2D background, Rectangle boundingRectangle, MSChangeableBuilding toBuy, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, 78, 78, 62, 62, toBuy, Shape.RECTANGULAR, spriteBatch, game)
        {

            AddMiddleOrb();
            AddSelection(
                "Establish the World Without Boundaries Foundation",
                "",
                MSWorldWithoutBoundariesStats.GetInstance(),
                    new Rectangle(boundingRectangle.X + 55, boundingRectangle.Y + 130, 152, 78),
                Game.Content.Load<Texture2D>("BuyDialog/7"),
                Game.Content.Load<Texture2D>("BuyDialog/7clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/7hovered"));
        }
    }
}
