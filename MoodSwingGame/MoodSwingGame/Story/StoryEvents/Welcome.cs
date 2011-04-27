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
    public class District1 : MSStoryEvent
    {
        public override bool Enabled()
        {
            return true;
        }

        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = game as MoodSwing;

            MSScreen screen = moodSwing.CurrentScreen;
            (screen as MSDistrictScreen).Paused = true;
            screen.HasFocus = false;
            screen.ResetHovers();
            moodSwing.Notifier.HasFocus = true;

            moodSwing.Notifier.AddComponent(
                new MSButton(
                    null,
                    new CloseNotification(),
                    new Rectangle(0, 0, 100, 50),
                    moodSwing.Content.Load<Texture2D>("Okay"),
                    moodSwing.Content.Load<Texture2D>("OkayClicked"),
                    moodSwing.Content.Load<Texture2D>("OkayHovered"),
                    null,
                    Shape.RECTANGULAR,
                    moodSwing.SpriteBatch,
                    moodSwing), Alignment.BOTTOM_CENTER);

            moodSwing.Notifier.InvokeNotification("");

            moodSwing.Notifier.FreezeNotifications = true;
            MSStory.StoryEnabled = false;
        }
    }

    public class District2 : MSStoryEvent
    {
        public override bool Enabled()
        {
            return true;
        }

        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = game as MoodSwing;

            MSScreen screen = moodSwing.CurrentScreen;
            (screen as MSDistrictScreen).Paused = true;
            screen.HasFocus = false;
            screen.ResetHovers();
            moodSwing.Notifier.HasFocus = true;

            moodSwing.Notifier.AddComponent(
                new MSButton(
                    null,
                    new CloseNotification(),
                    new Rectangle(0, 0, 100, 50),
                    moodSwing.Content.Load<Texture2D>("Okay"),
                    moodSwing.Content.Load<Texture2D>("OkayClicked"),
                    moodSwing.Content.Load<Texture2D>("OkayHovered"),
                    null,
                    Shape.RECTANGULAR,
                    moodSwing.SpriteBatch,
                    moodSwing), Alignment.BOTTOM_CENTER);

            moodSwing.Notifier.InvokeNotification("");

            moodSwing.Notifier.FreezeNotifications = true; MSStory.StoryEnabled = false;
        }
    }

    public class District3 : MSStoryEvent
    {
        public override bool Enabled()
        {
            return true;
        }

        public override void PerformAction(Game game)
        {
            MoodSwing moodSwing = game as MoodSwing;

            MSScreen screen = moodSwing.CurrentScreen;
            (screen as MSDistrictScreen).Paused = true;
            screen.HasFocus = false;
            screen.ResetHovers();
            moodSwing.Notifier.HasFocus = true;

            moodSwing.Notifier.AddComponent(
                new MSButton(
                    null,
                    new CloseNotification(),
                    new Rectangle(0, 0, 100, 50),
                    moodSwing.Content.Load<Texture2D>("Okay"),
                    moodSwing.Content.Load<Texture2D>("OkayClicked"),
                    moodSwing.Content.Load<Texture2D>("OkayHovered"),
                    null,
                    Shape.RECTANGULAR,
                    moodSwing.SpriteBatch,
                    moodSwing), Alignment.BOTTOM_CENTER);

            moodSwing.Notifier.InvokeNotification("");

            moodSwing.Notifier.FreezeNotifications = true;MSStory.StoryEnabled = false;
        }
    }
}
