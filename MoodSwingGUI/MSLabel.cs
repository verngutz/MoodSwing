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
    public abstract class MSLabel : MSGUIUnclickable
    {
        protected String text;
        public abstract String Text { set; get; }

        protected SpriteFont spriteFont;
        public Color TextColor { set; get; }

        public MSLabel(String text, Rectangle boundingRectangle, SpriteFont spriteFont, Color textColor, SpriteBatch spriteBatch, Game game)
            : base(boundingRectangle, spriteBatch, game) { }
    }
}
