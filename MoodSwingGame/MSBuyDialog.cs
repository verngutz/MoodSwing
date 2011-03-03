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

            MSToolTip testToolTip = new MSToolTip(null, BoundingRectangle, SpriteBatch, Game);
            MSToolTip descriptionToolTip = new MSToolTip
            (
                Game.Content.Load<Texture2D>("BlackOut"),
                new Rectangle(BoundingRectangle.X, BoundingRectangle.Y, 100, 50),
                SpriteBatch,
                Game
            );

            MSToolTip costToolTip = new MSToolTip
            (
                null,
                new Rectangle(0, 0, 160, 160),
                SpriteBatch,
                Game
            );

            testToolTip.AddComponent(descriptionToolTip);
            testToolTip.AddComponent(costToolTip, Alignment.MIDDLE_CENTER);


            AddComponent(new MSButton(null,
                new BuyTower(toBuy, MSHealthCenterStats.GetInstance()),
                new Rectangle(boundingRectangle.X, boundingRectangle.Y + 38, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/1"),
                Game.Content.Load<Texture2D>("BuyDialog/1clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/1hovered"),
                testToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSFoodCenterStats.GetInstance()), 
                new Rectangle(boundingRectangle.X + 38, boundingRectangle.Y, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/2"),
                Game.Content.Load<Texture2D>("BuyDialog/2clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/2hovered"),
                testToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSWomensOrgStats.GetInstance()), 
                new Rectangle(boundingRectangle.X + 129, boundingRectangle.Y, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/3"),
                Game.Content.Load<Texture2D>("BuyDialog/3clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/3hovered"),
                testToolTip, 
                Shape.AMORPHOUS, spriteBatch, game));

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSTutorialCenterStats.GetInstance()),
                new Rectangle(boundingRectangle.X + 187, boundingRectangle.Y + 38, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/4"),
                Game.Content.Load<Texture2D>("BuyDialog/4clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/4hovered"),
                testToolTip, 
                Shape.AMORPHOUS, spriteBatch, game));

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSHealthCenterStats.GetInstance()), 
                new Rectangle(boundingRectangle.X + 187, boundingRectangle.Y + 129, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/5"),
                Game.Content.Load<Texture2D>("BuyDialog/5clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/5hovered"),
                testToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSEcoParkStats.GetInstance()), 
                new Rectangle(boundingRectangle.X + 129, boundingRectangle.Y + 187, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/6"),
                Game.Content.Load<Texture2D>("BuyDialog/6clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/6hovered"),
                testToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            AddComponent(new MSButton(null, 
                new BuyTower(toBuy, MSGlobalCenterStats.GetInstance()),
                new Rectangle(boundingRectangle.X + 38, boundingRectangle.Y + 187, 93, 73),
                Game.Content.Load<Texture2D>("BuyDialog/7"),
                Game.Content.Load<Texture2D>("BuyDialog/7clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/7hovered"),
                testToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            AddComponent(new MSButton(null,
                new BuyTower(toBuy, MSHealthCenterStats.GetInstance()),
                new Rectangle(boundingRectangle.X, boundingRectangle.Y + 129, 73, 93),
                Game.Content.Load<Texture2D>("BuyDialog/8"),
                Game.Content.Load<Texture2D>("BuyDialog/8clicked"),
                Game.Content.Load<Texture2D>("BuyDialog/8hovered"),
                testToolTip,
                Shape.AMORPHOUS, spriteBatch, game));

            AddComponent(new MSPanel(Game.Content.Load<Texture2D>("BuyDialog/middleOrb"), new Rectangle(0, 0, 184, 184), null, Shape.CIRCULAR, spriteBatch, game), Alignment.MIDDLE_CENTER);
            
            AddComponent(new MSButton(null,
                new BuyVolunteerCenter(toBuy),
                new Rectangle(boundingRectangle.X + 128, boundingRectangle.Y + 131, 77, 80),
                Game.Content.Load<Texture2D>("BuyDialog/volunteer"),
                Game.Content.Load<Texture2D>("BuyDialog/volunteerclicked"),
                Game.Content.Load<Texture2D>("BuyDialog/volunteerhover"),
                testToolTip,
                Shape.AMORPHOUS, spriteBatch, game));
            
            
            AddComponent(new MSButton(null,
                new BuyFundraiser(toBuy),
                new Rectangle(boundingRectangle.X + 55, boundingRectangle.Y + 131, 77, 80),
                Game.Content.Load<Texture2D>("BuyDialog/money"),
                Game.Content.Load<Texture2D>("BuyDialog/moneyClicked"),
                Game.Content.Load<Texture2D>("BuyDialog/moneyHover"),
                testToolTip,
                Shape.AMORPHOUS, spriteBatch, game));
             
        }
    }
}
