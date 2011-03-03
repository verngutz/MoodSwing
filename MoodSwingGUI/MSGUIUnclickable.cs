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
    /// MSGUIUnclickable is a GUI component that is drawable but not clickable.
    /// </summary>
    public abstract class MSGUIUnclickable : MS2DComponent, MSGUIObject
    {
        public MSGUIUnclickable(Rectangle boundingRectangle, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, spriteBatch, game) { }
    }
}
