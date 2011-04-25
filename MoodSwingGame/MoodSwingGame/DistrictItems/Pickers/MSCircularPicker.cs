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
    public abstract class MSCircularPicker : MSPanel
    {
        protected MSBuilding selected;
        protected Effect selectedEffect;

        protected MSImageHolder bucksIcon;
        protected MSImageHolder gingerBreadManIcon;

        public MSCircularPicker(Texture2D background, Rectangle boundingRectangle, MSBuilding selected, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, selected, Color.White, shape, spriteBatch, game) { }

        public MSCircularPicker(Texture2D background, Rectangle boundingRectangle, MSBuilding selected, Color highlight, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, selected, shape, spriteBatch, game) { }

        public MSCircularPicker(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, MSBuilding selected, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, selected, Color.White, shape, spriteBatch, game) { }

        public MSCircularPicker(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, MSBuilding selected, Color highlight, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, null, shape, spriteBatch, game)
        {
            this.selected = selected;
            selectedEffect = selected.Effect;
            selected.Effect = Game.Content.Load<Effect>("highlight");

            bucksIcon = new MSImageHolder
            (
                new Rectangle(0, 0, 40, 24),
                Game.Content.Load<Texture2D>("BuyDialog/bucks"),
                SpriteBatch,
                Game
            );

            gingerBreadManIcon = new MSImageHolder
            (
                new Rectangle(0, 0, 20, 26),
                Game.Content.Load<Texture2D>("BuyDialog/gingerBreadMan"),
                SpriteBatch,
                Game
            );
        }

        public void UnhighlightSelected()
        {
            selected.Effect = selectedEffect;
        }

        protected void AddSelection(string description, MSBuildingStats stats, Rectangle boundingRectangle, Texture2D unhoveredTexture, Texture2D clickedTexture, Texture2D hoveredTexture)
        {
            if (selected is MSChangeableBuilding)
            {
                MSToolTip toolTip = new MSToolTip(Game.Content.Load<Texture2D>("BlackOut"), new Rectangle(0, 568, 1024, 200), 10, 10, 10, 10, SpriteBatch, Game);

                MSToolTip costToolTip = new MSToolTip
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

                toolTip.AddComponent(new MSWrappingLabel
                (
                    new Point(toolTip.BoundingRectangle.X, toolTip.BoundingRectangle.Y),
                    description,
                    Game.Content.Load<SpriteFont>("ToolTipFont"),
                    Color.White,
                    null,
                    2, 2, 2, 2, 1024, 768,
                    SpriteBatch,
                    Game
                ), Alignment.TOP_LEFT);

                toolTip.AddComponent(costToolTip, Alignment.MIDDLE_CENTER);

                costToolTip.AddComponent(bucksIcon, Alignment.MIDDLE_LEFT);

                costToolTip.AddComponent
                (
                    new MSWrappingLabel
                    (
                        new Point(0, 0),
                        stats.GetFundsCost().ToString(),
                        Game.Content.Load<SpriteFont>("BuyDialog"),
                        Color.White,
                        null,
                        null,
                        null,
                        SpriteBatch,
                        Game
                    ),
                    Alignment.MIDDLE_RIGHT
                );

                costToolTip.AddComponent(gingerBreadManIcon, Alignment.BOTTOM_LEFT);

                costToolTip.AddComponent
                (
                    new MSWrappingLabel
                    (
                        new Point(0, 0),
                        stats.GetVolunteerCost().ToString(),
                        Game.Content.Load<SpriteFont>("BuyDialog"),
                        Color.White,
                        null,
                        null,
                        null,
                        SpriteBatch,
                        Game
                    ),
                    Alignment.BOTTOM_RIGHT
                );

                MSAction toPerform;

                if (stats is MSTowerStats)
                {
                    toPerform = new BuyTower(selected as MSChangeableBuilding, stats as MSTowerStats);
                }
                else if (stats is MSVolunteerCenterStats)
                {
                    toPerform = new BuyVolunteerCenter(selected as MSChangeableBuilding);
                }
                else if (stats is MSFundraiserStats)
                {
                    toPerform = new BuyFundraiser(selected as MSChangeableBuilding);
                }
                else
                {
                    throw new ArgumentException();
                }

                AddComponent(new MSButton(null, toPerform, boundingRectangle, unhoveredTexture, clickedTexture, hoveredTexture, toolTip, Shape.AMORPHOUS, spriteBatch, Game));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected void AddMiddleOrb(string description)
        {
            MSPanel middleOrb = new MSPanel(Game.Content.Load<Texture2D>("BuyDialog/middleOrb"), new Rectangle(0, 0, 184, 184), 30, 0, 0, 0, null, Shape.CIRCULAR, spriteBatch, Game);
            AddComponent(middleOrb, Alignment.MIDDLE_CENTER);
            middleOrb.AddComponent
            (
                new MSFontScalingLabel
                (
                    description,
                    new Rectangle(86, 82, 91, 27),
                    Game.Content.Load<SpriteFont>("BuyDialog"),
                    SpriteBatch,
                    Game
                ),
                Alignment.TOP_CENTER
            );
        }
    }
}
