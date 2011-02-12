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

namespace MoodSwingGUI
{
    public class MSAnimatingButton : MSButton
    {
        public MS2DParametricEquation UnhoverPosition { set; get; }
        public MS2DParametricEquation UnhoverSize { set; get; }
        private int unhoverTimer;

        public MS2DParametricEquation HoverPosition { set; get; }
        public MS2DParametricEquation HoverSize { set; get; }
        private int hoverTimer;

        public MS2DParametricEquation ClickPosition { set; get; }
        public MS2DParametricEquation ClickSize { set; get; }
        private int clickTimer;

        public MS2DParametricEquation UnclickPosition { set; get; }
        public MS2DParametricEquation UnclickSize { set; get; }
        private int unclickTimer;

        /// <summary>
        /// Constructs an MSButton with no highlight.
        /// </summary>
        /// <param name="label">Add text to this MSButton with an MSLabel. Use null if you don't need to add text. Make sure to position the MSLabel relative to the upper-left corner of this MSButton.</param>
        /// <param name="action">will be executed when this MSButton is clicked</param>
        /// <param name="boundingRectangle">the bounding Rectangle of this MSButton</param>
        /// <param name="unhovered">will be drawn when this MSButton is unhovered</param>
        /// <param name="clicked">will be drawn when this MSButton is pressed and hovered</param>
        /// <param name="hovered">will be drawn when this MSButton is released and hovered</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSButton</param>
        /// <param name="shape">the Shape of this MSButton<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">the Game where this MSButton will be used</param>
        public MSAnimatingButton(MSLabel label, MSAction action, Rectangle boundingRectangle, Texture2D unhovered, Texture2D clicked, Texture2D hovered, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(label, action, boundingRectangle, unhovered, clicked, hovered, Color.White, shape, spriteBatch, game) { }

        /// <summary>
        /// Constructs an MSButton.
        /// </summary>
        /// <param name="label">Add text to this MSButton with an MSLabel. Use null if you don't need to add text. Make sure to position the MSLabel relative to the upper-left corner of this MSButton.</param>
        /// <param name="action">will be executed when this MSButton is clicked</param>
        /// <param name="boundingRectangle">the bounding Rectangle of thisMSButton</param>
        /// <param name="unhovered">will be drawn when this MSButton is unhovered</param>
        /// <param name="clicked">will be drawn when this MSButton is pressed and hovered</param>
        /// <param name="hovered">will be drawn when this MSButton is released and hovered</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSButton</param>
        /// <param name="highlight">the Color to overlay this MSButton with</param>
        /// <param name="shape">the Shape of this MSButton<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">the Game where this MSButton will be used</param>
        public MSAnimatingButton(MSLabel label, MSAction action, Rectangle boundingRectangle, Texture2D unhovered, Texture2D clicked, Texture2D hovered, Color highlight, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(label, action, boundingRectangle, unhovered, clicked, hovered, highlight, shape, spriteBatch, game) 
        {
            clickTimer = 0;
            unclickTimer = 0;
            hoverTimer = 0;
            unhoverTimer = 0;
            ClickPosition = new ConstantParametricCurve();
            ClickSize = new ConstantParametricCurve();
            HoverPosition = new ConstantParametricCurve();
            HoverSize = new ConstantParametricCurve();
            UnclickPosition = new ConstantParametricCurve();
            UnclickSize = new ConstantParametricCurve();
            UnhoverPosition = new ConstantParametricCurve();
            UnhoverSize = new ConstantParametricCurve();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (currentState)
            {
                case MSButtonState.CLICKED:
                    boundingRectangle = new Rectangle(
                        boundingRectangle.X + ClickPosition.Dx(clickTimer),
                        boundingRectangle.Y + ClickPosition.Dy(clickTimer),
                        boundingRectangle.Width + ClickSize.Dx(clickTimer),
                        boundingRectangle.Height + ClickSize.Dy(clickTimer));
                    clickTimer++;
                    unclickTimer = 0;
                    hoverTimer = 0;
                    unhoverTimer = 0;
                    break;
                case MSButtonState.HOVERED:
                    boundingRectangle = new Rectangle(
                        boundingRectangle.X + HoverPosition.Dx(hoverTimer),
                        boundingRectangle.Y + HoverPosition.Dy(hoverTimer),
                        boundingRectangle.Width + HoverSize.Dx(hoverTimer),
                        boundingRectangle.Height + HoverSize.Dy(hoverTimer));
                    clickTimer = 0;
                    unclickTimer = 0;
                    hoverTimer++;
                    unhoverTimer = 0;
                    break;
                case MSButtonState.UNCLICKED:
                    boundingRectangle = new Rectangle(
                        boundingRectangle.X + UnclickPosition.Dx(unclickTimer),
                        boundingRectangle.Y + UnclickPosition.Dy(unclickTimer),
                        boundingRectangle.Width + UnclickSize.Dx(unclickTimer),
                        boundingRectangle.Height + UnclickSize.Dy(unclickTimer));
                    clickTimer = 0;
                    unclickTimer++;
                    hoverTimer = 0;
                    unhoverTimer = 0;
                    break;
                case MSButtonState.UNHOVERED:
                    boundingRectangle = new Rectangle(
                        boundingRectangle.X + UnhoverPosition.Dx(unhoverTimer),
                        boundingRectangle.Y + UnhoverPosition.Dy(unhoverTimer),
                        boundingRectangle.Width + UnhoverSize.Dx(unhoverTimer),
                        boundingRectangle.Height + UnhoverSize.Dy(unhoverTimer));
                    clickTimer = 0;
                    unclickTimer = 0;
                    hoverTimer = 0;
                    unhoverTimer++;
                    break;
            }
        }
    }
}
