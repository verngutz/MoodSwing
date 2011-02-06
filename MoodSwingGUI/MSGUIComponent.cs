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
    public abstract class MSGUIComponent : MS2DComponent
    {
        public MSGUIComponent(Vector2 position, Vector2 size, SpriteBatch spriteBatch, Game game)
            : base(position, size, spriteBatch, game) { }
    }
}
