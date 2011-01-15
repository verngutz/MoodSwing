using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace MoodSwingCoreComponents
{
    /// <summary>
    /// Extend this class to implement events that may be triggered throughout a Game
    /// </summary>
    public abstract class MSAction
    {
        /// <summary>
        /// Override this method to specify the details of this MSAction's event.
        /// </summary>
        /// <param name="game">the Game where this event is to be performed</param>
        public abstract void PerformAction(Game game);
    }
}
