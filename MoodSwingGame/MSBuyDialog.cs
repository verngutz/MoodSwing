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
            : base(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, shape, spriteBatch, game)
        {
            this.toBuy = toBuy;
            AddComponent(new MSButton(null, new BuyTower(toBuy), new Rectangle(boundingRectangle.X + 10, boundingRectangle.Y + 10, 50, 50), Game.Content.Load<Texture2D>("buildTower"), Game.Content.Load<Texture2D>("buildTowerHovered"), Game.Content.Load<Texture2D>("buildTowerHovered"), Shape.RECTANGULAR, spriteBatch, game));
            AddComponent(new MSButton(null, new BuyVolunteerCenter(toBuy), new Rectangle(boundingRectangle.X + 70, boundingRectangle.Y + 10, 50, 50), Game.Content.Load<Texture2D>("buildVolunteerCenter"), Game.Content.Load<Texture2D>("buildVolunteerCenterHovered"), Game.Content.Load<Texture2D>("buildVolunteerCenterHovered"), Shape.RECTANGULAR, spriteBatch, game));
            AddComponent(new MSButton(null, new BuyFundraiser(toBuy), new Rectangle(boundingRectangle.X + 130, boundingRectangle.Y + 10, 50, 50), Game.Content.Load<Texture2D>("buildFundraiser"), Game.Content.Load<Texture2D>("buildFundraiserHovered"), Game.Content.Load<Texture2D>("buildFundraiserHovered"), Shape.RECTANGULAR, spriteBatch, game));
        }
    }
}
