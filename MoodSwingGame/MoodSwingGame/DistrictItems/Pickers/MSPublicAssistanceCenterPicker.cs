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
    public class MSPublicAssistanceCenterPicker : MSCircularPicker
    {
        public MSPublicAssistanceCenterPicker(Texture2D background, Rectangle boundingRectangle, MSChangeableBuilding toBuy, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, 78, 78, 62, 62, toBuy, Shape.RECTANGULAR, spriteBatch, game)
        {
            if (MSStory.SecondMDGBranchEnabled)
            {
                AddSelection(
                    "Upgrade To A General Clinic",
                    "There are three major challenges the world faces in terms of health care: too many children die of "
                    + "avoidable and curable diseases, too many mothers die upon giving birth, and too many more people "
                    + "die of epidemics such as AIDS and malaria. Take the first step to improve this district's health "
                    + "care. Upgrade this assistance office into a clinic for emergency first aid. The medics in this clinic "
                    + "can also encourage people to have healthy, simple yet life-saving habits such as handwashing.\n\n"
                    + "Effect: Has a moderate power to stop mobs with MDG complaints on Maternal Health, Child Health, "
                    + "and Widespread Disease.",
                    MSGeneralClinicStats.GetInstance(),
                    new Rectangle(boundingRectangle.X, boundingRectangle.Y + 38, 73, 93),
                    Game.Content.Load<Texture2D>("BuyDialog/Clinic"),
                    Game.Content.Load<Texture2D>("BuyDialog/ClinicClicked"),
                    Game.Content.Load<Texture2D>("BuyDialog/ClinicHovered"));
            }
            if (MSStory.FirstMDGBranchEnabled)
            {
                AddSelection(
                    "Upgrade To A Refuge For The Poor And The Widows",
                    "About 1.4 billion people in the world are living in extreme poverty, earning less than "
                    + "$1.25 a day. Most people are poor because of the lack of education: 69 million school-age children "
                    + "are not in school. To make things worse, in some places, employment opportunities are still "
                    + "largely biased against women. Many women who suddenly become widows or single mothers find "
                    + "it difficult to have a decent form of subsistence. Help the poor and the women in this district. "
                    + "Upgrade this assistance office into a temporary home for them.\n\n"
                    + "Effect: Has a moderate power to stop mobs with MDG complaints on Poverty, Education, and Gender "
                    + "Disparity.",
                    MSGeneralRefugeStats.GetInstance(),
                    new Rectangle(boundingRectangle.X + 38, boundingRectangle.Y, 93, 73),
                    Game.Content.Load<Texture2D>("BuyDialog/Refuge"),
                    Game.Content.Load<Texture2D>("BuyDialog/RefugeClicked"),
                    Game.Content.Load<Texture2D>("BuyDialog/RefugeHovered"));
            }
            if (MSStory.ThirdMDGBranchEnabled)
            {
                AddSelection(
                    "Upgrade To An Environmental Awareness Center",
                    "About 17, 000 species of plants and animals are at risk of extinction, with the numbers growing "
                    + "daily. Every year, 5.2 million hectares of forest are lost. Let the people of this district "
                    + "know and encourage them to take action. Upgrade this office into an environmental awareness center "
                    + "and tell everyone to care for our planet.\n\n"
                    + "Effect: Has a moderate power to stop mobs with MDG complaints on Environment Sustainability.",
                    MSEnvironmentalCenterStats.GetInstance(),
                    new Rectangle(boundingRectangle.X + 129, boundingRectangle.Y + 187, 93, 73),
                    Game.Content.Load<Texture2D>("BuyDialog/Environment"),
                    Game.Content.Load<Texture2D>("BuyDialog/EnvironmentClicked"),
                    Game.Content.Load<Texture2D>("BuyDialog/EnvironmentHovered"));

                AddSelection(
                    "Upgrade To An International Partnership Center",
                    "The poorest countries in the world are benefitting from freer trade and from the relaxing of debt. "
                    + "Upgrade this office to an international partnership center and make this district open to foreign "
                    + "support./n/n"
                    + "Effect: Has a moderate power to stop mobs with MDG complaints on Global Partnership.",
                    MSInternationalCenterStats.GetInstance(),
                    new Rectangle(boundingRectangle.X + 38, boundingRectangle.Y + 187, 93, 73),
                    Game.Content.Load<Texture2D>("BuyDialog/GLobal"),
                    Game.Content.Load<Texture2D>("BuyDialog/GlobalClicked"),
                    Game.Content.Load<Texture2D>("BuyDialog/GlobalHovered"));
            }

            AddMiddleOrb();
        }
    }
}
