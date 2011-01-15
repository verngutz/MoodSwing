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
using MoodSwingGUI;

namespace MoodSwingGame
{
    public class MSTileFactory
    {
        
        public MSTileFactory()
        {

        }
        public static MSTile createMSTile(int tileKey, Vector2 position, Vector2 size)
        {
            switch (tileKey - ((tilekey / 10) * 10))
            {
                case 0:
                    return (new MSTile(position,size,MoodSwing.getInstance().SpriteBatch,MoodSwing.getInstance().Content.Load<Texture2D>("Lot"));
                    break;
                case 1:
                    return (new MSTile(position,size,MoodSwing.getInstance().SpriteBatch,MoodSwing.getInstance().Content.Load<Texture2D>("Road_4_0"));
                    break;
            }
        }
    }
}
