using System;
using System.Collections.Generic;
using System.Linq;
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
    /// MSButton represents a drawable, clickable, action-attachable button.
    /// </summary>
    public class MSButton : MS2DClickable
    {
        private MSButtonState currentState;
        private Texture2D clickedTexture;
        private Texture2D hoveredTexture;
        private Texture2D unclickedTexture;
        private Color highlight;
        private MSLabel label;
        private MSAction action;
        private Vector2 scale;
        private Shape shape;
        
        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                if(label != null)
                    label.Position += base.Position;
            }
        }

        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">Add text to this MSButton with an MSLabel. Use null if you don't need to add text. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">will be executed when this MSButton is clicked</param>
        /// <param name="x">the x-position, in screen coordinates, corresponding to where the upper-left corner of this MSButton should be</param>
        /// <param name="y">the y-position, in screen coordinates, corresponding to where the upper-left corner of this MSButton should be</param>
        /// <param name="width">width in pixels of this MSButton</param>
        /// <param name="height">height in pixels of this MSButton</param>
        /// <param name="unclicked">will be drawn when this MSButton is MSButtonState.UNCLICKED</param>
        /// <param name="clicked">will be drawn when this MSButton is MSButtonState.CLICKED</param>
        /// <param name="hovered">will be drawn when this MSButton is MSButtonState.HOVERED</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSButton</param>
        /// <param name="shape">the Shape of this MSButton<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">the Game where this MSButton will be used</param>
        public MSButton(MSLabel label, MSAction action, float x, float y, float width, float height, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Shape shape, Game game)
            : this(label, action, new Vector2(x, y), new Vector2(width, height), unclicked, clicked, hovered, spriteBatch, Color.White, shape, game) { }

        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">Add text to this MSButton with an MSLabel. Use null if you don't need to add text. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">will be executed when this MSButton is clicked</param>
        /// <param name="x">the x-position, in screen coordinates, corresponding to where the upper-left corner of this MSButton should be</param>
        /// <param name="y">the y-position, in screen coordinates, corresponding to where the upper-left corner of this MSButton should be</param>
        /// <param name="width">width in pixels of this MSButton</param>
        /// <param name="height">height in pixels of this MSButton</param>
        /// <param name="unclicked">will be drawn when this MSButton is MSButtonState.UNCLICKED</param>
        /// <param name="clicked">will be drawn when this MSButton is MSButtonState.CLICKED</param>
        /// <param name="hovered">will be drawn when this MSButton is MSButtonState.HOVERED</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSButton</param>
        /// <param name="highlight">the Color to overlay this MSButton with</param>
        /// <param name="shape">the Shape of this MSButton<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">the Game where this MSButton will be used</param>
        public MSButton(MSLabel label, MSAction action, float x, float y, float width, float height, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game)
            : this(label, action, new Vector2(x, y), new Vector2(width, height), unclicked, clicked, hovered, spriteBatch, highlight, shape, game) { }

        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">Add text to this MSButton with an MSLabel. Use null if you don't need to add text. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">will be executed when this MSButton is clicked</param>
        /// <param name="boundingRectangle">the Rectangle that encloses this MSButton</param>
        /// <param name="unclicked">will be drawn when this MSButton is MSButtonState.UNCLICKED</param>
        /// <param name="clicked">will be drawn when this MSButton is MSButtonState.CLICKED</param>
        /// <param name="hovered">will be drawn when this MSButton is MSButtonState.HOVERED</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSButton</param>
        /// <param name="shape">the Shape of this MSButton<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">the Game where this MSButton will be used</param>
        public MSButton(MSLabel label, MSAction action, Rectangle boundingRectangle, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Shape shape, Game game)
            : this(label, action, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), unclicked, clicked, hovered, spriteBatch, Color.White, shape, game) { }

        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">Add text to this MSButton with an MSLabel. Use null if you don't need to add text. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">will be executed when this MSButton is clicked</param>
        /// <param name="boundingRectangle">the Rectangle that encloses this MSButton</param>
        /// <param name="unclicked">will be drawn when this MSButton is MSButtonState.UNCLICKED</param>
        /// <param name="clicked">will be drawn when this MSButton is MSButtonState.CLICKED</param>
        /// <param name="hovered">will be drawn when this MSButton is MSButtonState.HOVERED</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSButton</param>
        /// <param name="highlight">the Color to overlay this MSButton with</param>
        /// <param name="shape">the Shape of this MSButton<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">the Game where this MSButton will be used</param>
        public MSButton(MSLabel label, MSAction action, Rectangle boundingRectangle, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game)
            : this(label, action, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), unclicked, clicked, hovered, spriteBatch, highlight, shape, game) { }

        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">Add text to this MSButton with an MSLabel. Use null if you don't need to add text. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">will be executed when this MSButton is clicked</param>
        /// <param name="position">represents the position of this MSButton in screen coordinates</param>
        /// <param name="size">represents the size of this MSButton in pixels</param>
        /// <param name="unclicked">will be drawn when this MSButton is MSButtonState.UNCLICKED</param>
        /// <param name="clicked">will be drawn when this MSButton is MSButtonState.CLICKED</param>
        /// <param name="hovered">will be drawn when this MSButton is MSButtonState.HOVERED</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSButton</param>
        /// <param name="shape">the Shape of this MSButton<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">the Game where this MSButton will be used</param>
        public MSButton(MSLabel label, MSAction action, Vector2 position, Vector2 size, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Shape shape, Game game)
            : this(label, action, position, size, unclicked, clicked, hovered, spriteBatch, Color.White, shape, game) { }

        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">Add text to this MSButton with an MSLabel. Use null if you don't need to add text. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">will be executed when this MSButton is clicked</param>
        /// <param name="position">represents the position of this MSButton in screen coordinates</param>
        /// <param name="size">represents the size of this MSButton in pixels</param>
        /// <param name="unclicked">will be drawn when this MSButton is MSButtonState.UNCLICKED</param>
        /// <param name="clicked">will be drawn when this MSButton is MSButtonState.CLICKED</param>
        /// <param name="hovered">will be drawn when this MSButton is MSButtonState.HOVERED</param>
        /// <param name="spriteBatch">the SpriteBatch that will draw this MSButton</param>
        /// <param name="highlight">the Color to overlay this MSButton with</param>
        /// <param name="shape">the Shape of this MSButton<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">the Game where this MSButton will be used</param>
        public MSButton(MSLabel label, MSAction action, Vector2 position, Vector2 size, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game)
            : base(position, size, shape, spriteBatch, game)
        {
            unclickedTexture = unclicked;
            clickedTexture = clicked;
            hoveredTexture = hovered;
            this.highlight = highlight;
            currentState = 0;
            this.label = label;
            this.label.Position += Position;
            this.action = action;
            scale = Size / new Vector2(unclicked.Width, unclicked.Height);
            this.shape = shape;
        }

        public override void Draw(GameTime gameTime)
        {
            switch (currentState)
            {
                case MSButtonState.CLICKED:
                    SpriteBatch.Draw(clickedTexture, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    break;
                case MSButtonState.HOVERED:
                    SpriteBatch.Draw(hoveredTexture, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    break;
                case MSButtonState.UNCLICKED:
                    SpriteBatch.Draw(unclickedTexture, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    break;
            }

            if( label != null ) label.Draw(gameTime);
            base.Draw(gameTime);

        }

        /// <summary>
        /// Changes this MSButton's MSButtonState depending on the previous and current MouseStates of the Mouse
        /// </summary>
        /// <param name="oldMouseState">the previous MouseState of the Mouse</param>
        /// <returns>true if this MSButton has been clicked or equivalently, if this MSButton performs its MSAction</returns>
        public override bool CheckMouseClick(MouseState oldMouseState)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (CollidesWithMouse())
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed 
                    && oldMouseState.LeftButton == ButtonState.Released)
                {
                    currentState = MSButtonState.CLICKED;
                }
                else if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    currentState = MSButtonState.HOVERED;
                    if (oldMouseState.LeftButton == ButtonState.Pressed)
                    {
                        action.PerformAction(Game);
                        currentState = MSButtonState.UNCLICKED;
                        return true;
                    }
                }
            }
            else
                currentState = MSButtonState.UNCLICKED;
            return false;
        }
    }
}
