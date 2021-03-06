﻿using System;
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

namespace MoodSwingCoreComponents
{
    /// <summary>
    /// MS2DClickable is an MS2DCollidable that can be clicked (collides with) the Mouse
    /// </summary>
    public abstract class MS2DClickable : MS2DCollidable
    {
        public MS2DClickable(Rectangle boundingRectangle, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, shape, spriteBatch, game) { }

        public MS2DClickable(Vector2 position, Vector2 size, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(position, size, shape, spriteBatch, game) { }

        /// <summary>
        /// Check whether the Mouse is currently over this MS2DClickable
        /// </summary>
        /// <returns>true if this Mouse is currently over this MS2DClickable, false otherwise</returns>
        public bool CollidesWithMouse()
        {
            return base.EnclosesPoint(new Point(MSMouse.GetState().X, MSMouse.GetState().Y));
        }
    }
}
