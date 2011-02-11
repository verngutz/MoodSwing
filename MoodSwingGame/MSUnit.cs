using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MoodSwingCoreComponents;
namespace MoodSwingGame
{
    public interface MSUnit 
    {
        void Walk(MSTile[,] mapArray);
        bool IsThere();
        MS3DComponent Get3DComponent();
    }
}
