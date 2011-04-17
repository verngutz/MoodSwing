using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingCoreComponents
{
    public interface MS2DParametricCurve
    {
        int X(int t);
        int Y(int t);

        int X(int t, int x0);
        int Y(int t, int y0);

        int Dx(int t);
        int Dy(int t);
    }
}
