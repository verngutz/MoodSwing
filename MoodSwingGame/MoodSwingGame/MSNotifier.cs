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
using MoodSwingCoreComponents;
using MoodSwingGUI;

namespace MoodSwingGame
{
    public class MSNotifier : MSPanel
    {
        private static MSNotifier instance;
        public static MSNotifier GetInstance()
        {
            if (instance == null)
                instance = new MSNotifier();
            return instance;
        }

        public bool FreezeNotifications { set; get; }

        private Queue<string> notifications;
        private SpriteFont notificationFont;
        private Color fadeEffect;
        private int fadeAlpha;
        private int fadeIncrement;
        private int holdTimer;
        private const int HOLD_TIME = 50;

        private MSNotifier() 
            : base(MoodSwing.GetInstance().Content.Load<Texture2D>("BlackOut"), new Rectangle(0, 400, 1024, 100), null, Shape.RECTANGULAR, MoodSwing.GetInstance().SpriteBatch, MoodSwing.GetInstance()) 
        {
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
                    if (holdTimer++ > HOLD_TIME)
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