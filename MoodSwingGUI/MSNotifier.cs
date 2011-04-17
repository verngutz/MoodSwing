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

namespace MoodSwingGUI
{
    public class MSNotifier : MSPanel
    {
        public bool FreezeNotifications { set; get; }
        public int HoldTime { get; set; }

        private Queue<string> notifications;
        private SpriteFont notificationFont;
        private Color fadeEffect;
        private int fadeAlpha;
        private int fadeIncrement;
        private int holdTimer;

        public MSNotifier(Texture2D background, Rectangle boundingRectangle, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, null, shape, spriteBatch, game) 
        {
            HoldTime = 50;
            notificationFont = Game.Content.Load<SpriteFont>("ToolTipFont");
            notifications = new Queue<string>();
            fadeAlpha = 1;
            fadeEffect = new Color(255, 255, 255, fadeAlpha);
            fadeIncrement = 5;
            holdTimer = 0;
        }

        public void ClearNotifications()
        {
            notifications = new Queue<string>();
        }

        public void InvokeNotification(string notification)
        {
            notifications.Enqueue(notification);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (fadeAlpha > 0 && notifications.Count > 0)
            {
                fadeAlpha += fadeIncrement;
                if (fadeAlpha >= 255)
                {
                    fadeAlpha = 255;
                    fadeIncrement = 0;
                    if (holdTimer++ > HoldTime)
                    {
                        if (!FreezeNotifications)
                        {
                            holdTimer = 0;
                            fadeIncrement = -5;
                        }
                    }
                }
            }
            if (fadeAlpha <= 0)
            {
                notifications.Dequeue();
                fadeIncrement = 5;
                fadeAlpha = 1;
            }
            fadeEffect.A = (byte)fadeAlpha;
        }

        public override void Draw(GameTime gameTime)
        {
            if (notifications.Count > 0)
            {
                SpriteBatch.Draw(background, BoundingRectangle, fadeEffect);
                SpriteBatch.DrawString
                (
                    notificationFont,
                    notifications.Peek(),
                    Position + (Size - notificationFont.MeasureString(notifications.Peek())) / 2,
                    fadeEffect
                );
            }
        }
    }
}
