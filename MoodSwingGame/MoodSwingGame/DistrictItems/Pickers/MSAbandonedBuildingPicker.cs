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
    public class MSAbandonedBuildingPicker : MSCircularPicker
    {
        public MSAbandonedBuildingPicker(Texture2D background, Rectangle boundingRectangle, MSChangeableBuilding toBuy, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, 78, 78, 62, 62, toBuy, Shape.RECTANGULAR, spriteBatch, game)
        {
            if (MSStory.VolunteerCenterEnabled)
            {
                AddSelection("Build Volunteer Center",
                    "Call citizens to action! Build volunteer centers to train more volunteers and let them "
                    + "acquire the necessary knowledge and technology to help improve this district.",
                    MSVolunteerCenterStats.GetInstance(),
                    new Rectangle(boundingRectangle.X + 128, boundingRectangle.Y + 131, 77, 80),
                    Game.Content.Load<Texture2D>("BuyDialog/volunteer"),
                    Game.Content.Load<Texture2D>("BuyDialog/volunteerclicked"),
                    Game.Content.Load<Texture2D>("BuyDialog/volunteerhover"));
            }

            AddSelection("Build a Business Center",
                "Use your entrepreneurial skills to make money which you can later use for things like "
                + "saving the world, for example.",
                MSFundraiserStats.GetInstance(),
                new Rectangle(boundingRectangle.X + 55, boundingRectangle.Y + 131, 77, 80),
                Game.Content.Load<Texture2D>("BuyDialog/money"),
                Game.Content.Load<Texture2D>("BuyDialog/moneyClicked"),
                Game.Content.Load<Texture2D>("BuyDialog/moneyHover"));

            if (MSStory.TowerEnabled)
            {
                AddSelection("Build a Public Assistance Center",
                    "Background Information: The eight Millenium Development Goals were formed by all "
                    + "of the world's countries and leading development institutions as a unified commitment "
                    + "to solve the world's toughest problems, which include, among others, widespread poverty, "
                    + "disease, and destruction of nature.",
                    MSPublicAssistanceCenterStats.GetInstance(),
                    new Rectangle(boundingRectangle.X, boundingRectangle.Y + 38, 73, 93),
                    Game.Content.Load<Texture2D>("BuyDialog/1"),
                    Game.Content.Load<Texture2D>("BuyDialog/1clicked"),
                    Game.Content.Load<Texture2D>("BuyDialog/1hovered"));
            }
        }
    }
}
