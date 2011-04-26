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
    public class MSNotifier : MSFacadePanel
    {
        public int HoldTime { get; set; }

        private bool freezeNotifications;
        public bool FreezeNotifications
        {
            get { return freezeNotifications; }
            set 
            {
                if (value == false)
                {
                    holdTimer = 0;
                    if (fadeAlpha >= 255)
                    {
                        fadeIncrement = -5;
                        fadeAlpha += fadeIncrement;
                    }
                }
                freezeNotifications = value; 
            }
        }

        private MSWrappingLabel notificationHolder;
        private Queue<string> notifications;
        private Color fadeEffect;
        private Color FadeEffect 
        {
            get { return fadeEffect; }
            set
            {
                fadeEffect = value;
                notificationHolder.TextColor = value;
            }
        }
        private int fadeAlpha;
        private int fadeIncrement;
        private int holdTimer;

        private bool dirtyParent;
        private MSScreen parent;

        public MSNotifier(Texture2D background, Rectangle boundingRectangle, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, null, shape, spriteBatch, game) 
        {
            notificationHolder = new MSWrappingLabel(new Point(0, 0), "", Game.Content.Load<SpriteFont>("ToolTipFont"), FadeEffect, null, null, null, SpriteBatch, Game);
            AddComponent(notificationHolder);

            HoldTime = 50;
            notifications = new Queue<string>();
            fadeAlpha = 1;
            FadeEffect = new Color(255, 255, 255, fadeAlpha);
            fadeIncrement = 5;
            holdTimer = 0;
            dirtyParent = true;
            FreezeNotifications = false;
        }

        public void Reset()
        {
            notifications = new Queue<string>();
            fadeAlpha = 1;
            FadeEffect = new Color(255, 255, 255, fadeAlpha);
            fadeIncrement = 5;
            holdTimer = 0;
            dirtyParent = true;
            FreezeNotifications = false;
        }

        public void ClearNotifications()
        {
            notifications = new Queue<string>();
            if (!dirtyParent)
            {
                parent.HasFocus = true;
                dirtyParent = true;
            }
        }

        public void InvokeNotification(string message)
        {
            notifications.Enqueue(message);
            notificationHolder.Text = message;
            notificationHolder.Position = Position + Size / 2 - notificationHolder.Size / 2;
        }

        public void ReturnFocusTo(MSScreen parent)
        {
            this.parent = parent;
            dirtyParent = false;
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

                    foreach (MSGUIObject component in components)
                    {
                        if (component is MSGUIClickable)
                        {
                            (component as MSGUIClickable).Visible = true;
                        }
                        else if (component is MSGUIUnclickable)
                        {
                            (component as MSGUIUnclickable).Visible = true;
                        }
                    }
                }
                else
                {
                    foreach (MSGUIObject component in components)
                    {
                        if (component is MSGUIClickable)
                        {
                            (component as MSGUIClickable).Visible = false;
                        }
                        else if (component is MSGUIUnclickable)
                        {
                            (component as MSGUIUnclickable).Visible = false;
                        }
                    }
                }
            }
            if (fadeAlpha <= 0)
            {
                notifications.Dequeue();
                fadeIncrement = 5;
                fadeAlpha = 1;
                if (!dirtyParent)
                {
                    parent.HasFocus = true;
                    dirtyParent = true;
                }
            }
            FadeEffect = new Color(FadeEffect.R, FadeEffect.G, FadeEffect.B, (byte)fadeAlpha);
        }

        public override void Draw(GameTime gameTime)
        {
            if (notifications.Count > 0)
            {
                if(background != null)
                    SpriteBatch.Draw(background, BoundingRectangle, FadeEffect);
                notificationHolder.Draw(gameTime);
                foreach (MSGUIObject component in components)
                {
                    if (component is MSGUIClickable)
                    {
                        if ((component as MSGUIClickable).Visible)
                            (component as MSGUIClickable).Draw(gameTime);
                    }
                    else if (component is MSGUIUnclickable)
                    {
                        if ((component as MSGUIUnclickable).Visible)
                            (component as MSGUIUnclickable).Draw(gameTime);
                    }
                }
            }
        }

        public void ClearComponents()
        {
            components.Clear();
            UnclickableComponents.Clear();
            ClickableComponents.Clear();
        }
    }
}
