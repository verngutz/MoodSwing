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

            #region General Help Tool Tip
            MSToolTip generalHelpToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);

            MSToolTip generalHelpCostToolTip = new MSToolTip
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

            generalHelpToolTip.AddComponent(new MSUnresizingLabel
            (
                new Point(BoundingRectangle.X, BoundingRectangle.Y + BoundingRectangle.Height),
                "Build a general help center",
                Game.Content.Load<SpriteFont>("ToolTipFont"),
                Color.White,
                Game.Content.Load<Texture2D>("BlackOut"),
                2, 2, 2, 2,
                SpriteBatch,
                Game
            ));

            generalHelpToolTip.AddComponent(generalHelpCostToolTip, Alignment.MIDDLE_CENTER);

            generalHelpCostToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

            generalHelpCostToolTip.AddComponent
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

            generalHelpCostToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

            generalHelpCostToolTip.AddComponent
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
                new BuyTower(toBuy, MSGeneralHelpCenterStats.GetInstance()),
                new Rectangle(boundingRectangle.X, boundingRectangle.Y + 38, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/1"),
                Game.Content.Load<Texture2D>("BuyDialog/1clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/1hovered"),
                generalHelpToolTip,
                Shape.AMORPHOUS, spriteBatch, game));
        }
    }
}
