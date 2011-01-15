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
        /// <param name="label">The MSLabel to add to this MSButton, if any, otherwise use null. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">The MSAction that will be executed when this MSButton is clicked.</param>
        /// <param name="x">The x-position, in screen coordinates, corresponding to where the upper-left corner of this MSButton should be.</param>
        /// <param name="y">The y-position, in screen coordinates, corresponding to where the upper-left corner of this MSButton should be.</param>
        /// <param name="width">The width, in screen coordinates, of this MSButton.</param>
        /// <param name="height">The height, in screen coordinates, of this MSButton.</param>
        /// <param name="unclicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.UNCLICKED.</param>
        /// <param name="clicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.CLICKED.</param>
        /// <param name="hovered">The Texture2D object that will be drawn when this MSButton is MSButtonState.HOVERED.</param>
        /// <param name="spriteBatch">The SpriteBatch that will draw this MSButton.</param>
        /// <param name="shape">The Shape of this MSButton.<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">The Game where this MSButton will be used.</param>
        public MSButton(MSLabel label, MSAction action, float x, float y, float width, float height, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Shape shape, Game game)
            : this(label, action, new Vector2(x, y), new Vector2(width, height), unclicked, clicked, hovered, spriteBatch, Color.White, shape, game) { }

        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">The MSLabel to add to this MSButton, if any, otherwise use null. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">The MSAction that will be executed when this MSButton is clicked.</param>
        /// <param name="x">The x-position, in screen coordinates, corresponding to where the upper-left corner of this MSButton should be.</param>
        /// <param name="y">The y-position, in screen coordinates, corresponding to where the upper-left corner of this MSButton should be.</param>
        /// <param name="width">The width, in screen coordinates, of this MSButton.</param>
        /// <param name="height">The height, in screen coordinates, of this MSButton.</param>
        /// <param name="unclicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.UNCLICKED.</param>
        /// <param name="clicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.CLICKED.</param>
        /// <param name="hovered">The Texture2D object that will be drawn when this MSButton is MSButtonState.HOVERED.</param>
        /// <param name="spriteBatch">The SpriteBatch that will draw this MSButton.</param>
        /// <param name="highlight">The Microsoft.XNA.Framework.Graphics.Color to overlay this MSButton with.</param>
        /// <param name="shape">The Shape of this MSButton.<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">The Game where this MSButton will be used.</param>
        public MSButton(MSLabel label, MSAction action, float x, float y, float width, float height, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game)
            : this(label, action, new Vector2(x, y), new Vector2(width, height), unclicked, clicked, hovered, spriteBatch, highlight, shape, game) { }

        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">The MSLabel to add to this MSButton, if any, otherwise use null. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">The MSAction that will be executed when this MSButton is clicked.</param>
        /// <param name="boundingRectangle">The Microsoft.XNA.Framework.Rectangle containing this button.</param>
        /// <param name="unclicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.UNCLICKED.</param>
        /// <param name="clicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.CLICKED.</param>
        /// <param name="hovered">The Texture2D object that will be drawn when this MSButton is MSButtonState.HOVERED.</param>
        /// <param name="spriteBatch">The SpriteBatch that will draw this MSButton.</param>
        /// <param name="shape">The Shape of this MSButton.<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">The Game where this MSButton will be used.</param>
        public MSButton(MSLabel label, MSAction action, Rectangle boundingRectangle, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Shape shape, Game game)
            : this(label, action, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), unclicked, clicked, hovered, spriteBatch, Color.White, shape, game) { }
        
        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">The MSLabel to add to this MSButton, if any, otherwise use null. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">The MSAction that will be executed when this MSButton is clicked.</param>
        /// <param name="boundingRectangle">The Microsoft.XNA.Framework.Rectangle containing this button.</param>
        /// <param name="unclicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.UNCLICKED.</param>
        /// <param name="clicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.CLICKED.</param>
        /// <param name="hovered">The Texture2D object that will be drawn when this MSButton is MSButtonState.HOVERED.</param>
        /// <param name="spriteBatch">The SpriteBatch that will draw this MSButton.</param>
        /// <param name="highlight">The Microsoft.XNA.Framework.Graphics.Color to overlay this MSButton with.</param>
        /// <param name="shape">The Shape of this MSButton.<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">The Game where this MSButton will be used.</param>
        public MSButton(MSLabel label, MSAction action, Rectangle boundingRectangle, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Color highlight, Shape shape, Game game)
            : this(label, action, new Vector2(boundingRectangle.X, boundingRectangle.Y), new Vector2(boundingRectangle.Width, boundingRectangle.Height), unclicked, clicked, hovered, spriteBatch, highlight, shape, game) { }

        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">The MSLabel to add to this MSButton, if any, otherwise use null. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">The MSAction that will be executed when this MSButton is clicked.</param>
        /// <param name="position">A Microsoft.XNA.Framework.Vector2 representing the position of this MSButton in screen coordinates.</param>
        /// <param name="size">A Microsoft.XNA.Framework.Vector2 </param>
        /// <param name="unclicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.UNCLICKED.</param>
        /// <param name="clicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.CLICKED.</param>
        /// <param name="hovered">The Texture2D object that will be drawn when this MSButton is MSButtonState.HOVERED.</param>
        /// <param name="spriteBatch">The SpriteBatch that will draw this MSButton.</param>
        /// <param name="shape">The Shape of this MSButton.<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">The Game where this MSButton will be used.</param>
        public MSButton(MSLabel label, MSAction action, Vector2 position, Vector2 size, Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch spriteBatch, Shape shape, Game game)
            : this(label, action, position, size, unclicked, clicked, hovered, spriteBatch, Color.White, shape, game) { }
        
        /// <summary>
        /// Constructs an MSButton from the given parameters.
        /// </summary>
        /// <param name="label">The MSLabel to add to this MSButton, if any, otherwise use null. Make sure to position the MSLabel relative to the upper-right corner of this MSButton.</param>
        /// <param name="action">The MSAction that will be executed when this MSButton is clicked.</param>
        /// <param name="position">A Microsoft.XNA.Framework.Vector2 representing the position of this MSButton in screen coordinates.</param>
        /// <param name="size">A Microsoft.XNA.Framework.Vector2 </param>
        /// <param name="unclicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.UNCLICKED.</param>
        /// <param name="clicked">The Texture2D object that will be drawn when this MSButton is MSButtonState.CLICKED.</param>
        /// <param name="hovered">The Texture2D object that will be drawn when this MSButton is MSButtonState.HOVERED.</param>
        /// <param name="spriteBatch">The SpriteBatch that will draw this MSButton.</param>
        /// <param name="highlight">The Microsoft.XNA.Framework.Graphics.Color to overlay this MSButton with.</param>
        /// <param name="shape">The Shape of this MSButton.<seealso cref="MoodSwingCoreComponents.Shape"/></param>
        /// <param name="game">The Game where this MSButton will be used.</param>
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

        public override bool CheckMouseClick(MouseState oldMouseState)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (CollidesWithMouse(currentMouseState))
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
