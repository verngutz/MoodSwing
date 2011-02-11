using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace MoodSwingCoreComponents
{
    /// <summary>
    /// Implement this interface to implement events that may be triggered throughout a Game
    /// </summary>
    public interface MSAction
    {
        /// <summary>
        /// Override this method to specify the details of this MSAction's event.
        /// </summary>
        /// <param name="game">the Game where this event is to be performed</param>
        void PerformAction(Game game);
    }
}
