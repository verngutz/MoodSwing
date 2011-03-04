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
    public class MSNotifier : MSPanel
    {
        public static MSNotifier instance;
        public static MSNotifier GetInstance()
        {
            if (instance == null)
                instance = new MSNotifier();
            return instance;
        }

        private Queue<string> notifications;
        private SpriteFont notificationFont;

        private MSNotifier() 
            : base(MoodSwing.GetInstance().Content.Load<Texture2D>("BlackOut"), new Rectangle(0, 728, 1024, 40), null, Shape.RECTANGULAR, MoodSwing.GetInstance().SpriteBatch, MoodSwing.GetInstance()) 
        {
            notifications = new Queue<string>();
            notificationFont = Game.Content.Load<SpriteFont>("Temp");
        }
    }
}
