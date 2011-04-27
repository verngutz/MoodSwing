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
    public class Exit : MSAction
    {
        public void PerformAction(Game game)
        {
            if( (game as MoodSwing).CurrentScreen is MSDistrictScreen ) 
                ((game as MoodSwing).CurrentScreen as MSDistrictScreen).save();
            game.Exit();
        }
    }
}
