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
    /// <summary>
    /// MSCheckbox represents a checkbox that has two discrete states, ticked and unticked.
    /// </summary>
    public class MSCheckbox : MS2DClickable
    {
        /// <summary>
        /// Gets or sets the tick marker for this MSCheckbox
        /// </summary>
        /// <value>true if this MSCheckbox is ticked, false otherwise</value>
        public bool IsTicked { get; set; }

        private MSButton unticked;
        private MSButton ticked;

        /// <summary>
        /// Creates an MSCheckbox with the given parameters
        /// </summary>
        /// <param name="untickedLabel">Use this to add text to this MSCheckbox when it is unticked. Use null for none.</param>
        /// <param name="tickedLabel">Use this to add text to this MSCheckbox when it is ticked. Use null for none.</param>
        /// <param name="untickedAction">When this MSCheckbox is unticked and it is clicked, it will become ticked and it will perform the specified MSAction.</param>
        /// <param name="tickedAction">When this MSCheckbox is ticked and it is clicked, it will become unticked and it will perform the specified MSAction.</param>
        /// <param name="x">the x-position, in screen coordinates, corresponding to where the upper-left corner of this MSCheckbox should be</param>
        /// <param name="y">the y-position, in screen coordinates, corresponding to where the upper-left corner of this MSCheckbox should be</param>
        /// <param name="width">width in pixels of this MSCheckbox</param>
        /// <param name="height">height in pixels of this MSCheckbox</param>
        /// <param name="untickedUnclicked">will be drawn when this MSCheckbox is both unticked and unclicked</param>
        /// <param name="untickedClicked">will be drawn when this MSCheckbox is both unticked and clicked</param>
        /// <param name="untickedHovered">will be drawn when this MSCheckbox is both unticked and hovered</param>
        /// <param name="tickedUnclicked">will be drawn when this MSCheckbox is both ticked and unclicked</param>
        /// <param name="tickedClicked">will be drawn when this MSCheckbox is both ticked and clicked</param>
        /// <param name="tickedHovered">will be drawn when this MSCheckbox is both ticked and hovered</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSCheckbox</param>
        /// <param name="shape">the Shape of this MSCheckbox</param>
        /// <param name="game">the Game where this MSCheckbox will be used</param>
        /// <param name="isTicked">true if this Checkbox must be initially ticked, false otherwise</param>
        public MSCheckbox(MSLabel untickedLabel, MSLabel tickedLabel, MSAction untickedAction, MSAction tickedAction, int x, int y, int width, int height, Texture2D untickedUnclicked, Texture2D untickedClicked, Texture2D untickedHovered, Texture2D tickedUnclicked, Texture2D tickedClicked, Texture2D tickedHovered, SpriteBatch spriteBatch, Shape shape, Game game, bool isTicked)
            : this(untickedLabel, tickedLabel, untickedAction, tickedAction, new Vector2(x, y), new Vector2(width, height), untickedUnclicked, untickedClicked, untickedHovered, tickedUnclicked, tickedClicked, tickedHovered, spriteBatch, Color.White, shape, game, isTicked) { }

        /// <summary>
        /// Creates an MSCheckbox with the given parameters
        /// </summary>
        /// <param name="untickedLabel">Use this to add text to this MSCheckbox when it is unticked. Use null for none.</param>
        /// <param name="tickedLabel">Use this to add text to this MSCheckbox when it is ticked. Use null for none.</param>
        /// <param name="untickedAction">When this MSCheckbox is unticked and it is clicked, it will become ticked and it will perform the specified MSAction.</param>
        /// <param name="tickedAction">When this MSCheckbox is ticked and it is clicked, it will become unticked and it will perform the specified MSAction.</param>
        /// <param name="x">the x-position, in screen coordinates, corresponding to where the upper-left corner of this MSCheckbox should be</param>
        /// <param name="y">the y-position, in screen coordinates, corresponding to where the upper-left corner of this MSCheckbox should be</param>
        /// <param name="width">width in pixels of this MSCheckbox</param>
        /// <param name="height">height in pixels of this MSCheckbox</param>
        /// <param name="untickedUnclicked">will be drawn when this MSCheckbox is both unticked and unclicked</param>
        /// <param name="untickedClicked">will be drawn when this MSCheckbox is both unticked and clicked</param>
        /// <param name="untickedHovered">will be drawn when this MSCheckbox is both unticked and hovered</param>
        /// <param name="tickedUnclicked">will be drawn when this MSCheckbox is both ticked and unclicked</param>
        /// <param name="tickedClicked">will be drawn when this MSCheckbox is both ticked and clicked</param>
        /// <param name="tickedHovered">will be drawn when this MSCheckbox is both ticked and hovered</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSCheckbox</param>
        /// <param name="shape">the Shape of this MSCheckbox</param>
        /// <param name="game">the Game where this MSCheckbox will be used</param>
        /// <param name="isTicked">true if this Checkbox must be initially ticked, false otherwise</param>
        public MSCheckbox(MSLabel untickedLabel, MSLabel tickedLabel, MSAction untickedAction, MSAction tickedAction, int x, int y, int width, int height, Texture2D untickedUnclicked, Texture2D untickedClicked, Texture2D untickedHovered, Texture2D tickedUnclicked, Texture2D tickedClicked, Texture2D tickedHovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game, bool isTicked)
            : this(untickedLabel, tickedLabel, untickedAction, tickedAction, new Vector2(x, y), new Vector2(width, height), untickedUnclicked, untickedClicked, untickedHovered, tickedUnclicked, tickedClicked, tickedHovered, spriteBatch, highlight, shape, game, isTicked) { }

        /// <summary>
        /// Creates an MSCheckbox with the given parameters
        /// </summary>
        /// <param name="untickedLabel">Use this to add text to this MSCheckbox when it is unticked. Use null for none.</param>
        /// <param name="tickedLabel">Use this to add text to this MSCheckbox when it is ticked. Use null for none.</param>
        /// <param name="untickedAction">When this MSCheckbox is unticked and it is clicked, it will become ticked and it will perform the specified MSAction.</param>
        /// <param name="tickedAction">When this MSCheckbox is ticked and it is clicked, it will become unticked and it will perform the specified MSAction.</param>
        /// <param name="boundingRectangle">the Rectangle that encloses this MSCheckbox</param>
        /// <param name="untickedUnclicked">will be drawn when this MSCheckbox is both unticked and unclicked</param>
        /// <param name="untickedClicked">will be drawn when this MSCheckbox is both unticked and clicked</param>
        /// <param name="untickedHovered">will be drawn when this MSCheckbox is both unticked and hovered</param>
        /// <param name="tickedUnclicked">will be drawn when this MSCheckbox is both ticked and unclicked</param>
        /// <param name="tickedClicked">will be drawn when this MSCheckbox is both ticked and clicked</param>
        /// <param name="tickedHovered">will be drawn when this MSCheckbox is both ticked and hovered</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSCheckbox</param>
        /// <param name="highlight">the Color to overlay this MSCheckbox with</param>
        /// <param name="shape">the Shape of this MSCheckbox</param>
        /// <param name="game">the Game where this MSCheckbox will be used</param>
        /// <param name="isTicked">true if this Checkbox must be initially ticked, false otherwise</param>
        public MSCheckbox(MSLabel untickedLabel, MSLabel tickedLabel, MSAction untickedAction, MSAction tickedAction, Rectangle boundingRectangle, Texture2D untickedUnclicked, Texture2D untickedClicked, Texture2D untickedHovered, Texture2D tickedUnclicked, Texture2D tickedClicked, Texture2D tickedHovered, SpriteBatch spriteBatch, Shape shape, Game game, bool isTicked)
            : this(untickedLabel, tickedLabel, untickedAction, tickedAction, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), untickedUnclicked, untickedClicked, untickedHovered, tickedUnclicked, tickedClicked, tickedHovered, spriteBatch, Color.White, shape, game, isTicked) { }

        /// <summary>
        /// Creates an MSCheckbox with the given parameters
        /// </summary>
        /// <param name="untickedLabel">Use this to add text to this MSCheckbox when it is unticked. Use null for none.</param>
        /// <param name="tickedLabel">Use this to add text to this MSCheckbox when it is ticked. Use null for none.</param>
        /// <param name="untickedAction">When this MSCheckbox is unticked and it is clicked, it will become ticked and it will perform the specified MSAction.</param>
        /// <param name="tickedAction">When this MSCheckbox is ticked and it is clicked, it will become unticked and it will perform the specified MSAction.</param>
        /// <param name="position">represents the position of this MSCheckbox in screen coordinates</param>
        /// <param name="size">represents the size of this MSCheckbox in pixels</param>
        /// <param name="untickedUnclicked">will be drawn when this MSCheckbox is both unticked and unclicked</param>
        /// <param name="untickedClicked">will be drawn when this MSCheckbox is both unticked and clicked</param>
        /// <param name="untickedHovered">will be drawn when this MSCheckbox is both unticked and hovered</param>
        /// <param name="tickedUnclicked">will be drawn when this MSCheckbox is both ticked and unclicked</param>
        /// <param name="tickedClicked">will be drawn when this MSCheckbox is both ticked and clicked</param>
        /// <param name="tickedHovered">will be drawn when this MSCheckbox is both ticked and hovered</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSCheckbox</param>
        /// <param name="highlight">the Color to overlay this MSCheckbox with</param>
        /// <param name="shape">the Shape of this MSCheckbox</param>
        /// <param name="game">the Game where this MSCheckbox will be used</param>
        /// <param name="isTicked">true if this Checkbox must be initially ticked, false otherwise</param>
        public MSCheckbox(MSLabel untickedLabel, MSLabel tickedLabel, MSAction untickedAction, MSAction tickedAction, Rectangle boundingRectangle, Texture2D untickedUnclicked, Texture2D untickedClicked, Texture2D untickedHovered, Texture2D tickedUnclicked, Texture2D tickedClicked, Texture2D tickedHovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game, bool isTicked)
            : this(untickedLabel, tickedLabel, untickedAction, tickedAction, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), untickedUnclicked, untickedClicked, untickedHovered, tickedUnclicked, tickedClicked, tickedHovered, spriteBatch, highlight, shape, game, isTicked) { }

        /// <summary>
        /// Creates an MSCheckbox with the given parameters
        /// </summary>
        /// <param name="untickedLabel">Use this to add text to this MSCheckbox when it is unticked. Use null for none.</param>
        /// <param name="tickedLabel">Use this to add text to this MSCheckbox when it is ticked. Use null for none.</param>
        /// <param name="untickedAction">When this MSCheckbox is unticked and it is clicked, it will become ticked and it will perform the specified MSAction.</param>
        /// <param name="tickedAction">When this MSCheckbox is ticked and it is clicked, it will become unticked and it will perform the specified MSAction.</param>
        /// <param name="position">represents the position of this MSCheckbox in screen coordinates</param>
        /// <param name="size">represents the size of this MSCheckbox in pixels</param>
        /// <param name="untickedUnclicked">will be drawn when this MSCheckbox is both unticked and unclicked</param>
        /// <param name="untickedClicked">will be drawn when this MSCheckbox is both unticked and clicked</param>
        /// <param name="untickedHovered">will be drawn when this MSCheckbox is both unticked and hovered</param>
        /// <param name="tickedUnclicked">will be drawn when this MSCheckbox is both ticked and unclicked</param>
        /// <param name="tickedClicked">will be drawn when this MSCheckbox is both ticked and clicked</param>
        /// <param name="tickedHovered">will be drawn when this MSCheckbox is both ticked and hovered</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSCheckbox</param>
        /// <param name="shape">the Shape of this MSCheckbox</param>
        /// <param name="game">the Game where this MSCheckbox will be used</param>
        /// <param name="isTicked">true if this Checkbox must be initially ticked, false otherwise</param>
        public MSCheckbox(MSLabel untickedLabel, MSLabel tickedLabel, MSAction untickedAction, MSAction tickedAction, Vector2 position, Vector2 size, Texture2D untickedUnclicked, Texture2D untickedClicked, Texture2D untickedHovered, Texture2D tickedUnclicked, Texture2D tickedClicked, Texture2D tickedHovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game, bool isTicked)
            : base(position, size, shape, spriteBatch, game)
        {
            unticked = new MSButton(untickedLabel, untickedAction, position, size, untickedUnclicked, untickedClicked, untickedHovered, spriteBatch, highlight, shape, game);
            ticked = new MSButton(tickedLabel, tickedAction, position, size, tickedUnclicked, tickedClicked, tickedHovered, spriteBatch, highlight, shape, game);
            IsTicked = isTicked;
        }

        /// <summary>
        /// Creates an MSCheckbox using two MSButtons
        /// </summary>
        /// <param name="unticked">an MSButton representing the unticked state of this MSCheckbox and which will become invisible once this MSCheckbox is ticked</param>
        /// <param name="ticked">an MSButton representing the ticked state of this MSCheckbox and which will become invisible once this MSCheckbox is unticked</param>
        /// <param name="isTicked"></param>
        public MSCheckbox(MSButton unticked, MSButton ticked, bool isTicked) 
            : base(unticked.Position, unticked.Size, unticked.Shape, unticked.SpriteBatch, unticked.Game) 
        {
            this.unticked = unticked;
            this.ticked = ticked;
            IsTicked = IsTicked;
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsTicked)
            {
                ticked.Draw(gameTime);
            }
            else
            {
                unticked.Draw(gameTime);
            }
            base.Draw(gameTime);

        }

        /// <summary>
        /// Changes this MSCheckbox's state depending on the previous and current MouseStates of the Mouse
        /// </summary>
        /// <param name="oldMouseState">the previous MouseState of the Mouse</param>
        /// <returns>true if this MSCheckbox has been clicked or equivalently, if this MSCheckbox must toggle property IsTicked</returns>
        public override bool CheckMouseClick(MouseState oldMouseState)
        {
            if (IsTicked)
            {
                if (ticked.CheckMouseClick(oldMouseState))
                {
                    IsTicked = false;
                    return true;
                }
            }
            else
            {
                if (unticked.CheckMouseClick(oldMouseState))
                {
                    IsTicked = true;
                    return true;
                }
            }
            return false;
        }
    }
}

