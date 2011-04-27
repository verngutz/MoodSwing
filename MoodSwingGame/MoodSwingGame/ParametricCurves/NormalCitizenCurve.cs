﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MoodSwingCoreComponents;
using Microsoft.Xna.Framework;
namespace MoodSwingGame
{
    public class NormalCitizenCurve : MSParametricCurve
    {
        private float constant = .1f;
        public int X(int t) { return X(t, 0); }

        public int X(int t, int x0) { return (int)(MathHelper.Clamp((float)((0.005*Math.Log(t)) * 100f), 0f, 100f)); }

        public int Dx(int t) { return -1; }
    }
}
