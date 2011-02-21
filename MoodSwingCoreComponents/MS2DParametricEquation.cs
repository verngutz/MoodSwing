﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingCoreComponents
{
    public interface MS2DParametricEquation
    {
        int X(int t);
        int Y(int t);

        int X(int t, int x0);
        int Y(int t, int y0);

        int Dx(int t);
        int Dy(int t);
    }

    public class ConstantParametricCurve : MS2DParametricEquation
    {
        public int X(int t) { return 0; }
        public int Y(int t) { return 0; }
        public int Dx(int t) { return 0; }
        public int Dy(int t) { return 0; }

        public int X(int t, int x0) { return x0; }
        public int Y(int t, int y0) { return y0; }
    }
}