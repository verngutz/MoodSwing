using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingCoreComponents
{
    public interface MSParametricCurve
    {
        int X(int t);
        int X(int t, int x0);
        int Dx(int t);
    }
}
