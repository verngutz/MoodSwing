using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using MoodSwingCoreComponents;

namespace MoodSwingGame
{
    public class Shift50pxRight : MS2DParametricEquation
    {
        public int X(int t)
        {
            if (t < 50) return 3 * t;
            else return 0;
        }
        public int Y(int t)
        {
            return 0;
        }

        public int Dx(int t)
        {
            if (t < 50) return 3;
            else return 0;
        }
        public int Dy(int t)
        {
            return 0;
        }
    }

    public class Shift50pxLeft : MS2DParametricEquation
    {
        public int X(int t)
        {
            if (t < 50) return -3 * t;
            else return 0;
        }
        public int Y(int t)
        {
            return 0;
        }

        public int Dx(int t)
        {
            if (t < 50) return -3;
            else return 0;
        }
        public int Dy(int t)
        {
            return 0;
        }
    }
}
