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
    public class Welcome : MSStoryEvent
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
                    new MSResizingLabel(
                        "Where Am I", 
                        new Rectangle(0, 0, 100, 50), 
                        moodSwing.Content.Load<SpriteFont>("Temp"),
                        moodSwing.SpriteBatch,
                        moodSwing),
                    new CloseNotification(),
                    new Rectangle(0, 0, 100, 50),
                    moodSwing.Content.Load<Texture2D>("Button"),
                    moodSwing.Content.Load<Texture2D>("ButtonClicked"),
                    moodSwing.Content.Load<Texture2D>("ButtonHovered"),
                    null,
                    Shape.RECTANGULAR,
                    moodSwing.SpriteBatch,
                    moodSwing), Alignment.BOTTOM_CENTER);

            moodSwing.Notifier.InvokeNotification("");

            moodSwing.Notifier.FreezeNotifications = true;

            MSStory.RemoveStoryEvent(this);
        }
    }
}
