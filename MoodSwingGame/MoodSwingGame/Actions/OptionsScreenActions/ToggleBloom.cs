using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using MoodSwingCoreComponents;

namespace MoodSwingGame
{
    public class ToggleBloom : MSAction
    {
        private bool bloom;

        public ToggleBloom(bool bloom)
        {
            this.bloom = bloom;
        }

        public void PerformAction(Game game)
        {
            (game as MoodSwing).Bloom.Visible = bloom;
        }
    }
}
