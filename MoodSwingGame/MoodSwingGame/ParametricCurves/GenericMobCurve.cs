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
        private float constant = 0.25f;
        public int X(int t) { return X(t, 0); }

        public int X(int t, int x0) { return (int)(MathHelper.Clamp((float)(constant * (Math.Log(t) * Math.Cos(t) + x0)), 0f, 50)); }

        public int Dx(int t) { return -1; }
    }
}
