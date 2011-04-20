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
    public class ChangeResolution : MSAction
    {
        private int width;
        private int height;

        public ChangeResolution(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void PerformAction(Game game)
        {
            MSResolution.SetResolution(width, height);
        }
    }
}
