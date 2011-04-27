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
                    "Call citizens to action! Build volunteer centers to be able to train more volunteers and let them "
                    + "acquire the necessary knowledge and technology to help improve this district.\n\n"
                    + "Effect: Increases your volunteer limit by " + MSResourceManager.VOLUNTEER_CENTER_GAIN + ".",
                    MSVolunteerCenterStats.GetInstance(),
                    new Rectangle(boundingRectangle.X + 130, boundingRectangle.Y - 5, 135, 270),
                    Game.Content.Load<Texture2D>("BuyDialog/VolunteerCenter"),
                    Game.Content.Load<Texture2D>("BuyDialog/VolunteerCenterClicked"),
                    Game.Content.Load<Texture2D>("BuyDialog/VolunteerCenterHovered"));
            }

            AddSelection("Build A Business Center",
                "Use your entrepreneurial skills to make money for yourself, and of course, for helping the people in "
                + "this district too!\n\n"
                + "Effect: Generates funds over time.",
                MSFundraiserStats.GetInstance(),
                new Rectangle(boundingRectangle.X - 5, boundingRectangle.Y - 5, 135, 270),
                Game.Content.Load<Texture2D>("BuyDialog/MoneyCenter"),
                Game.Content.Load<Texture2D>("BuyDialog/MoneyCenterClicked"),
                Game.Content.Load<Texture2D>("BuyDialog/MoneyCenterHovered"));

            AddMiddleOrb();

            if (MSStory.TowerEnabled)
            {
                AddSelection("Build A Public Assistance Center",
                    "The people of Mood City are in turmoil. Take the first step to help them. Build an office "
                    + "to lend a helping hand to and hear the concerns of the people in this district.\n\n"
                    + "Background Information: The eight Millenium Development Goals were formed by all "
                    + "of the world's countries and leading development institutions as a unified commitment "
                    + "to solve the world's toughest problems, which include, among others, widespread poverty, "
                    + "disease, and destruction of nature.\n\n"
                    + "Effect: Has a small power to stop mobs with any given MDG complaint.",
                    MSPublicAssistanceCenterStats.GetInstance(),
                    new Rectangle(boundingRectangle.X + 55, boundingRectangle.Y + 130, 152, 78),
                    Game.Content.Load<Texture2D>("BuyDialog/general"),
                    Game.Content.Load<Texture2D>("BuyDialog/GeneralClicked"),
                    Game.Content.Load<Texture2D>("BuyDialog/GeneralHovered"));
            }
        }
    }
}
