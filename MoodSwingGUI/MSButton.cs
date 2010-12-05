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

namespace MoodSwingGUI
{
    public class MSButton : MS2DComponent
    {
        enum ButtonState {
            UNCLICKED = 0,
            CLICKED,
            HOVERED
        }

        private Rectangle boundingRectangle;
        private ButtonState currentState;
        private Texture2D clickedTexture;
        private Texture2D hoveredTexture;
        private Texture2D unclickedTexture;
        private Color highlight;
        private MSLabel label;
        private SpriteBatch spriteBatch;

        public MSButton(Game g, int x, int y, int width, int height,
            Texture2D unclicked, Texture2D clicked, Texture2D hovered, SpriteBatch sb, Color hlight )
            : base(sb, g)
        {
            unclickedTexture = unclicked;
            clickedTexture = clicked;
            hoveredTexture = hovered;
            spriteBatch = sb;
            highlight = hlight;
            boundingRectangle = new Rectangle( x, y, width, height );
        }

        public override void Draw(GameTime gameTime)
        {
            Texture2D currTexture = null;
            switch (currentState)
            {
                case ButtonState.CLICKED:
                    currTexture = clickedTexture;
                    break;
                case ButtonState.HOVERED:
                    currTexture = hoveredTexture;
                    break;
                case ButtonState.UNCLICKED:
                    currTexture = unclickedTexture;
                    break;
            }

            spriteBatch.Draw(currTexture, boundingRectangle, highlight);
            base.Draw(gameTime);

        }
    }
}
