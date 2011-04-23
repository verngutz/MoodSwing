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

using MoodSwingGUI;
using MoodSwingCoreComponents;

namespace MoodSwingGame
{
    public class OpenExitConfirmation : MSAction
    {
        public enum ExitType { ToWindows, ToMainMenu };

        private ExitType type;
        public OpenExitConfirmation(ExitType type)
        {
            this.type = type;
        }

        public void PerformAction(Game game)
        {

            MoodSwing moodSwing = game as MoodSwing;

            MSScreen screen = moodSwing.CurrentScreen;
            screen.HasFocus = false;
            screen.ResetHovers();
            moodSwing.Notifier.HasFocus = true;
            moodSwing.Notifier.FreezeNotifications = false;
            moodSwing.Notifier.ClearComponents();
            //moodSwing.Notifier.ClearNotifications();

            moodSwing.Notifier.AddComponent(
                new MSButton(
                    null,
                    new CloseNotification(),
                    new Rectangle(0, 0, 50, 50),
                    moodSwing.Content.Load<Texture2D>("NoWay"),
                    moodSwing.Content.Load<Texture2D>("NoWayClicked"),
                    moodSwing.Content.Load<Texture2D>("NoWayHovered"),
                    null,
                    Shape.RECTANGULAR,
                    moodSwing.SpriteBatch,
                    moodSwing), Alignment.BOTTOM_RIGHT);

            if (type == ExitType.ToWindows)
            {
                moodSwing.Notifier.AddComponent(
                    new MSButton(
                        null,
                        new Exit(),
                        new Rectangle(0, 0, 50, 50),
                        moodSwing.Content.Load<Texture2D>("Okay"),
                        moodSwing.Content.Load<Texture2D>("OkayClicked"),
                        moodSwing.Content.Load<Texture2D>("OkayHovered"),
                        null,
                        Shape.RECTANGULAR,
                        moodSwing.SpriteBatch,
                        moodSwing), Alignment.BOTTOM_LEFT);

                moodSwing.Notifier.InvokeNotification("Are you sure you want to quit to Windows?");
            }
            else if (type == ExitType.ToMainMenu)
            {
                moodSwing.Notifier.AddComponent(
                    new MSButton(
                        null,
                        OpenMainScreen.GetInstance(),
                        new Rectangle(0, 0, 50, 50),
                        moodSwing.Content.Load<Texture2D>("Okay"),
                        moodSwing.Content.Load<Texture2D>("OkayClicked"),
                        moodSwing.Content.Load<Texture2D>("OkayHovered"),
                        null,
                        Shape.RECTANGULAR,
                        moodSwing.SpriteBatch,
                        moodSwing), Alignment.BOTTOM_LEFT);

                moodSwing.Notifier.InvokeNotification("Are you sure you want to quit to the main menu?");
            }

            moodSwing.Notifier.FreezeNotifications = true;
        }
    }
}
