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
        private static MSRandom instance;
        public static MSRandom Instance 
        { 
            get 
            {
                if (instance == null)
                    instance = new MSRandom();
                return instance; 
            } 
        }

        private Random random;
        private Normal normal;

        public MSRandom(int seed)
        {
            random = new Random(seed);
            normal = new Normal();
        }

        public MSRandom()
        {
            random = new Random();
            normal = new Normal();
        }

        public int GetUniformInt(int max)
        {
            return random.Next(max);
        }

        public float GetUniform()
        {
            return (float)random.NextDouble();
        }

        public float GetNormal()
        {
            return MathHelper.Clamp((float)normal.Sample(), 0, 0.999f);
        }

        public float GetReverseNormal()
        {
            if (GetUniform() >= 0.5f)
                return MathHelper.Clamp((float)Math.Abs(GetNormal()), 0, 0.999f);
            else
                return MathHelper.Clamp((float)Math.Abs(GetNormal() + 1), 0, 0.999f);
        }
    }
}
