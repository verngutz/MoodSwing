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
    public class MSCheckbox : MS2DClickable
    {
        public bool IsTicked { get; set; }

        private MSButton unticked;
        private MSButton ticked;

        public MSCheckbox(MSLabel untickedLabel, MSLabel tickedLabel, MSAction untickedAction, MSAction tickedAction, int x, int y, int width, int height, Texture2D untickedUnclicked, Texture2D untickedClicked, Texture2D untickedHovered, Texture2D tickedUnclicked, Texture2D tickedClicked, Texture2D tickedHovered, SpriteBatch spriteBatch, Shape shape, Game game, bool isTicked)
            : this(untickedLabel, tickedLabel, untickedAction, tickedAction, new Vector2(x, y), new Vector2(width, height), untickedUnclicked, untickedClicked, untickedHovered, tickedUnclicked, tickedClicked, tickedHovered, spriteBatch, Color.White, shape, game, isTicked) { }

        public MSCheckbox(MSLabel untickedLabel, MSLabel tickedLabel, MSAction untickedAction, MSAction tickedAction, int x, int y, int width, int height, Texture2D untickedUnclicked, Texture2D untickedClicked, Texture2D untickedHovered, Texture2D tickedUnclicked, Texture2D tickedClicked, Texture2D tickedHovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game, bool isTicked)
            : this(untickedLabel, tickedLabel, untickedAction, tickedAction, new Vector2(x, y), new Vector2(width, height), untickedUnclicked, untickedClicked, untickedHovered, tickedUnclicked, tickedClicked, tickedHovered, spriteBatch, highlight, shape, game, isTicked) { }

        public MSCheckbox(MSLabel untickedLabel, MSLabel tickedLabel, MSAction untickedAction, MSAction tickedAction, Rectangle boundingRectangle, Texture2D untickedUnclicked, Texture2D untickedClicked, Texture2D untickedHovered, Texture2D tickedUnclicked, Texture2D tickedClicked, Texture2D tickedHovered, SpriteBatch spriteBatch, Shape shape, Game game, bool isTicked)
            : this(untickedLabel, tickedLabel, untickedAction, tickedAction, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), untickedUnclicked, untickedClicked, untickedHovered, tickedUnclicked, tickedClicked, tickedHovered, spriteBatch, Color.White, shape, game, isTicked) { }

        public MSCheckbox(MSLabel untickedLabel, MSLabel tickedLabel, MSAction untickedAction, MSAction tickedAction, Rectangle boundingRectangle, Texture2D untickedUnclicked, Texture2D untickedClicked, Texture2D untickedHovered, Texture2D tickedUnclicked, Texture2D tickedClicked, Texture2D tickedHovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game, bool isTicked)
            : this(untickedLabel, tickedLabel, untickedAction, tickedAction, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), untickedUnclicked, untickedClicked, untickedHovered, tickedUnclicked, tickedClicked, tickedHovered, spriteBatch, highlight, shape, game, isTicked) { }

        public MSCheckbox(MSLabel untickedLabel, MSLabel tickedLabel, MSAction untickedAction, MSAction tickedAction, Vector2 position, Vector2 size, Texture2D untickedUnclicked, Texture2D untickedClicked, Texture2D untickedHovered, Texture2D tickedUnclicked, Texture2D tickedClicked, Texture2D tickedHovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game, bool isTicked)
            : base(position, size, shape, spriteBatch, game)
        {
            unticked = new MSButton(untickedLabel, untickedAction, position, size, untickedUnclicked, untickedClicked, untickedHovered, spriteBatch, highlight, shape, game);
            ticked = new MSButton(tickedLabel, tickedAction, position, size, tickedUnclicked, tickedClicked, tickedHovered, spriteBatch, highlight, shape, game);
            IsTicked = isTicked;
        }

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

