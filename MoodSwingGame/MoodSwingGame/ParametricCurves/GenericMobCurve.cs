using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using MoodSwingCoreComponents;

namespace MoodSwingGame
{
    public class GenericMobCurve : MSParametricCurve
    {
        public int X(int t) { return X(t, 0); }

        public int X(int t, int x0) { return (int)(MathHelper.Clamp((float)(0.0015f*(Math.Log(t)*Math.Cos(t*t) + x0) * 100f), 0f, 100f)); }

        public int Dx(int t) { return -1; }
    }
}
