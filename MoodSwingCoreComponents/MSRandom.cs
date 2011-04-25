using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Interpolation;

namespace MoodSwingCoreComponents
{
    public class MSRandom
    {
        public static Random random = new Random();
        private static Normal normal = new Normal();

        public static float GetUniform()
        {
            return (float)random.NextDouble();
        }
        public static float GetNormal()
        {
            return MathHelper.Clamp((float)normal.Sample(), 0, 0.999f);
        }
        public static float GetReverseNormal()
        {
            if (GetUniform() >= 0.5f)
                return MathHelper.Clamp((float)Math.Abs(GetNormal()), 0, 0.999f);
            else
                return MathHelper.Clamp((float)Math.Abs(GetNormal() + 1), 0, 0.999f);
        }
    }
}
