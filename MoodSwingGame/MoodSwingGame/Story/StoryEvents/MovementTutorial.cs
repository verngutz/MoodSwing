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
    public class MovementTutorial : MSStoryEvent
    {
        public override bool Enabled()
        {
            throw new NotImplementedException();
        }
        public override void PerformAction(Game game)
        {
            (new CloseNotification()).PerformAction(game);
            MoodSwing moodSwing = MoodSwing.GetInstance();
            (moodSwing.CurrentScreen as MSDistrictScreen).Paused = false;
            moodSwing.Notifier.InvokeNotification("Move the mouse to the edges of the screen or drag with the right mouse button to move around.");
            moodSwing.Notifier.InvokeNotification("Drag with the mouse wheel to look around.");
            moodSwing.Notifier.InvokeNotification("Scroll the mouse wheel forward and backward to zoom in and out, respectively.");
        }
    }
}
