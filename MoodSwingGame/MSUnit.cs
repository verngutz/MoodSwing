using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MoodSwingCoreComponents;
namespace MoodSwingGame
{
    public interface MSUnit 
    {
        void Walk(MS3DTile[,] mapArray);
        bool IsThere();
    }
}
