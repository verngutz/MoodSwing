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
    public class MSBuyDialog : MSPanel
    {
        private MSBuyableBuilding toBuy;
        private Effect toBuyEffect;

        public MSBuyDialog(Texture2D background, Rectangle boundingRectangle, MSBuyableBuilding toBuy, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, toBuy, Color.White, shape, spriteBatch, game) { }

        public MSBuyDialog(Texture2D background, Rectangle boundingRectangle, MSBuyableBuilding toBuy, Color highlight, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, toBuy, shape, spriteBatch, game) { }

        public MSBuyDialog(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, MSBuyableBuilding toBuy, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, toBuy, Color.White, shape, spriteBatch, game) { }

        public MSBuyDialog(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, MSBuyableBuilding toBuy, Color highlight, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, null, shape, spriteBatch, game)
        {
            this.toBuy = toBuy;
            toBuyEffect = toBuy.Effect;
            toBuy.Effect = Game.Content.Load<Effect>("highlight");
            MSImageHolder bucksIcon = new MSImageHolder
            (
                new Rectangle(0, 0, 40, 24),
                Game.Content.Load<Texture2D>("BuyDialog/bucks"),
                SpriteBatch,
                Game
            );

            MSImageHolder gingerBreadManIcon = new MSImageHolder
            (
                new Rectangle(0, 0, 20, 26),
                Game.Content.Load<Texture2D>("BuyDialog/gingerBreadMan"),
                SpriteBatch,
                Game
            );

            #region Child Health Tool Tip
            MSToolTip childHealthToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip childHealthCostToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 75, 50),
                -30,
                0,
                0,
                0,
                SpriteBatch,
                Game
            );

            childHealthToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build a children's health center",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            childHealthToolTip.AddComponent(childHealthCostToolTip, Alignment.MIDDLE_CENTER);

            childHealthCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            childHealthCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSChildHealthCenterStats.GetInstance().GetFundsCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.MIDDLE_RIGHT
            );

            childHealthCostToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

            childHealthCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSChildHealthCenterStats.GetInstance().GetVolunteerCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.BOTTOM_RIGHT
            );
            #endregion

            AddComponent(new MSButton(null,
                new BuyTower(toBuy, MSChildHealthCenterStats.GetInstance()),
                new Rectangle(boundingRectangle.X, boundingRectangle.Y + 38, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/1"),
                Game.Content.Load<Texture2D>("BuyDialog/1clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/1hovered"),
                childHealthToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            #region Poverty Tool Tip
            MSToolTip povertyToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip povertyCostToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 75, 50),
                -30,
                0,
                0,
                0,
                SpriteBatch,
                Game
            );

            povertyToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build a feeding center for the poor",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            povertyToolTip.AddComponent(povertyCostToolTip, Alignment.MIDDLE_CENTER);

            povertyCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            povertyCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSFoodCenterStats.GetInstance().GetFundsCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.MIDDLE_RIGHT
            );

            povertyCostToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

            povertyCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSFoodCenterStats.GetInstance().GetVolunteerCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.BOTTOM_RIGHT
            );
            #endregion

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSFoodCenterStats.GetInstance()), 
                new Rectangle(boundingRectangle.X + 38, boundingRectangle.Y, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/2"),
                Game.Content.Load<Texture2D>("BuyDialog/2clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/2hovered"),
                povertyToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            #region Gender Equality Tool Tip
            MSToolTip genderEqualityToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip genderEqualityCostToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 75, 50),
                -30,
                0,
                0,
                0,
                SpriteBatch,
                Game
            );

            genderEqualityToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build an employment center for women",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            genderEqualityToolTip.AddComponent(genderEqualityCostToolTip, Alignment.MIDDLE_CENTER);

            genderEqualityCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            genderEqualityCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSWomensOrgStats.GetInstance().GetFundsCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.MIDDLE_RIGHT
            );

            genderEqualityCostToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

            genderEqualityCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSWomensOrgStats.GetInstance().GetVolunteerCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.BOTTOM_RIGHT
            );
            #endregion

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSWomensOrgStats.GetInstance()), 
                new Rectangle(boundingRectangle.X + 129, boundingRectangle.Y, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/3"),
                Game.Content.Load<Texture2D>("BuyDialog/3clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/3hovered"),
                genderEqualityToolTip, 
                Shape.AMORPHOUS, spriteBatch, game));

            #region Education Tool Tip
            MSToolTip educationToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip educationCostToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 75, 50),
                -30,
                0,
                0,
                0,
                SpriteBatch,
                Game
            );

            educationToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build a free tutorial center for children",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            educationToolTip.AddComponent(educationCostToolTip, Alignment.MIDDLE_CENTER);

            educationCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            educationCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSTutorialCenterStats.GetInstance().GetFundsCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.MIDDLE_RIGHT
            );

            educationCostToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

            educationCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSTutorialCenterStats.GetInstance().GetVolunteerCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.BOTTOM_RIGHT
            );
            #endregion

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSTutorialCenterStats.GetInstance()),
                new Rectangle(boundingRectangle.X + 187, boundingRectangle.Y + 38, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/4"),
                Game.Content.Load<Texture2D>("BuyDialog/4clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/4hovered"),
                educationToolTip, 
                Shape.AMORPHOUS, spriteBatch, game));

            #region Maternal Health Tool Tip
            MSToolTip maternalHealthToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip maternalHealthCostToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 75, 50),
                -30,
                0,
                0,
                0,
                SpriteBatch,
                Game
            );

            maternalHealthToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build a reproductive and maternal health center",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            maternalHealthToolTip.AddComponent(maternalHealthCostToolTip, Alignment.MIDDLE_CENTER);

            maternalHealthCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            maternalHealthCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSMaternalHealthCenterStats.GetInstance().GetFundsCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.MIDDLE_RIGHT
            );

            maternalHealthCostToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

            maternalHealthCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSMaternalHealthCenterStats.GetInstance().GetVolunteerCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.BOTTOM_RIGHT
            );
            #endregion

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSMaternalHealthCenterStats.GetInstance()), 
                new Rectangle(boundingRectangle.X + 187, boundingRectangle.Y + 129, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/5"),
                Game.Content.Load<Texture2D>("BuyDialog/5clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/5hovered"),
                maternalHealthToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            #region Environment Tool Tip
            MSToolTip environmentToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip environmentCostToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 75, 50),
                -30,
                0,
                0,
                0,
                SpriteBatch,
                Game
            );

            environmentToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build an environmental awareness center",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            environmentToolTip.AddComponent(environmentCostToolTip, Alignment.MIDDLE_CENTER);

            environmentCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            environmentCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSEcoParkStats.GetInstance().GetFundsCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.MIDDLE_RIGHT
            );

            environmentCostToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

            environmentCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSEcoParkStats.GetInstance().GetVolunteerCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.BOTTOM_RIGHT
            );
            #endregion

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSEcoParkStats.GetInstance()), 
                new Rectangle(boundingRectangle.X + 129, boundingRectangle.Y + 187, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/6"),
                Game.Content.Load<Texture2D>("BuyDialog/6clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/6hovered"),
                environmentToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            #region Global Partnership Tool Tip
            MSToolTip globalPartnershipToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip globalPartnershipCostToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 75, 50),
                -30,
                0,
                0,
                0,
                SpriteBatch,
                Game
            );

            globalPartnershipToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build global trade center",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"), 
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            globalPartnershipToolTip.AddComponent(globalPartnershipCostToolTip, Alignment.MIDDLE_CENTER);

            globalPartnershipCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            globalPartnershipCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSGlobalCenterStats.GetInstance().GetFundsCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.MIDDLE_RIGHT
            );

            globalPartnershipCostToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

            globalPartnershipCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSGlobalCenterStats.GetInstance().GetVolunteerCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.BOTTOM_RIGHT
            );
            #endregion

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSGlobalCenterStats.GetInstance()),
                new Rectangle(boundingRectangle.X + 38, boundingRectangle.Y + 187, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/7"),
                Game.Content.Load<Texture2D>("BuyDialog/7clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/7hovered"),
                globalPartnershipToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            #region HIV AIDS Tool Tip
            MSToolTip hivAidsToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip hivAidsCostToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 75, 50),
                -30,
                0,
                0,
                0,
                SpriteBatch,
                Game
            );

            hivAidsToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build HIV treatment center",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            hivAidsToolTip.AddComponent(hivAidsCostToolTip, Alignment.MIDDLE_CENTER);

            hivAidsCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            hivAidsCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSHivAidsHealthCenterStats.GetInstance().GetFundsCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.MIDDLE_RIGHT
            );

            hivAidsCostToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

            hivAidsCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSHivAidsHealthCenterStats.GetInstance().GetVolunteerCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.BOTTOM_RIGHT
            );
            #endregion

            AddComponent(new MSButton(null,
                new BuyTower(toBuy, MSHivAidsHealthCenterStats.GetInstance()),
                new Rectangle(boundingRectangle.X, boundingRectangle.Y + 129, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/8"),
                Game.Content.Load<Texture2D>("BuyDialog/8clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/8hovered"),
                hivAidsToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            MSPanel middleOrb = new MSPanel(Game.Content.Load<Texture2D>("BuyDialog/middleOrb"), new Rectangle(0, 0, 184, 184), 30, 0, 0, 0, null, Shape.CIRCULAR, spriteBatch, game);
            AddComponent(middleOrb, Alignment.MIDDLE_CENTER);
            middleOrb.AddComponent
            (
                new MSResizingLabel
                (
                    "For Sale",
                    new Rectangle(86, 82, 91, 27),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    SpriteBatch,
                    Game
                ),
                Alignment.TOP_CENTER
            );

            #region Volunteer Center Tool Tip
            MSToolTip volunteerCenterToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip volunteerCenterCostToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 75, 50),
                -30,
                0,
                0,
                0,
                SpriteBatch,
                Game
            );

            volunteerCenterToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build a Volunteer Center",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"), 
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            volunteerCenterToolTip.AddComponent(volunteerCenterCostToolTip, Alignment.MIDDLE_CENTER);

            volunteerCenterCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            volunteerCenterCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSVolunteerCenterStats.GetInstance().GetFundsCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.MIDDLE_RIGHT
            );
            #endregion
            
            AddComponent(new MSButton(null,
                new BuyVolunteerCenter(toBuy),
                new Rectangle(boundingRectangle.X + 128, boundingRectangle.Y + 131, 77, 80),
                Game.Content.Load<Texture2D>("BuyDialog/volunteer"),
                Game.Content.Load<Texture2D>("BuyDialog/volunteerclicked"),
                Game.Content.Load<Texture2D>("BuyDialog/volunteerhover"),
                volunteerCenterToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            #region Fundraiser Tool Tip
            MSToolTip fundraiserToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip fundraiserCostToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 75, 50),
                -30,
                0,
                0,
                0,
                SpriteBatch,
                Game
            );

            fundraiserToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build a Donations Center",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"), 
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            fundraiserToolTip.AddComponent(fundraiserCostToolTip, Alignment.MIDDLE_CENTER);

            fundraiserCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            fundraiserCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSFundraiserStats.GetInstance().GetFundsCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.MIDDLE_RIGHT
            );

            fundraiserCostToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

            fundraiserCostToolTip.AddComponent
            (
                new MSUnresizingLabel
                (
                    new Point(0, 0),
                    MSFundraiserStats.GetInstance().GetVolunteerCost().ToString(),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    Color.Black,
                    null,
                    SpriteBatch,
                    Game
                ),
                Alignment.BOTTOM_RIGHT
            );
            #endregion
            
            AddComponent(new MSButton(null,
                new BuyFundraiser(toBuy),
                new Rectangle(boundingRectangle.X + 55, boundingRectangle.Y + 131, 77, 80),
                Game.Content.Load<Texture2D>("BuyDialog/money"),
                Game.Content.Load<Texture2D>("BuyDialog/moneyClicked"),
                Game.Content.Load<Texture2D>("BuyDialog/moneyHover"),
                fundraiserToolTip,
                Shape.AMORPHOUS, spriteBatch, game));
             
        }

        protected override void Dispose(bool disposing)
        {
 	        base.Dispose(disposing);
            toBuy.Effect = toBuyEffect;
        }
    }
}
