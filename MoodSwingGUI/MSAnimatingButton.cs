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

        private int x0;
        private int y0;
        private int w0;
        private int h0;

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
            x0 = boundingRectangle.X;
            y0 = boundingRectangle.Y;
            w0 = boundingRectangle.Width;
            h0 = boundingRectangle.Height;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (CurrentState)
            {
                case MSGUIClickableState.LEFTCLICKED:
                    boundingRectangle = new Rectangle(
                        ClickPosition.X(clickTimer, x0),
                        ClickPosition.Y(clickTimer, y0),
                        ClickSize.X(clickTimer, w0),
                        ClickSize.Y(clickTimer, h0));
                    clickTimer++;
                    break;
                case MSGUIClickableState.HOVERED:
                    boundingRectangle = new Rectangle(
                        HoverPosition.X(hoverTimer, x0),
                        HoverPosition.Y(hoverTimer, y0),
                        HoverSize.X(hoverTimer, w0),
                        HoverSize.Y(hoverTimer, h0));
                    hoverTimer++;
                    break;
                case MSGUIClickableState.UNLEFTCLICKED:
                    boundingRectangle = new Rectangle(
                        UnclickPosition.X(unclickTimer, x0),
                        UnclickPosition.Y(unclickTimer, y0),
                        UnclickSize.X(unclickTimer, w0),
                        UnclickSize.Y(unclickTimer, h0));
                    unclickTimer++;
                    break;
                case MSGUIClickableState.UNHOVERED:
                    boundingRectangle = new Rectangle(
                        UnhoverPosition.X(unhoverTimer, x0),
                        UnhoverPosition.Y(unhoverTimer, y0),
                        UnhoverSize.X(unhoverTimer, w0),
                        UnhoverSize.Y(unhoverTimer, h0));
                    unhoverTimer++;
                    break;
            }
        }

        public override void LeftClick()
        {
            base.LeftClick();
            clickTimer = 0;
        }
        public override void UnLeftClick()
        {
            base.UnLeftClick();
            unclickTimer = 0;
        }
        public override void Hover()
        {
            base.Hover();
            hoverTimer = 0;
        }
        public override void UnHover()
        {
            base.UnHover();
            unhoverTimer = 0;
        }
    }
}
