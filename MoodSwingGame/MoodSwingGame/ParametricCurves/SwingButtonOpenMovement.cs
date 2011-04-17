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
    public class SwingButtonOpenMovement : MS2DParametricCurve
    {
        public int X(int t) { return X(t, 0); }

        public int X(int t, int x0)
        {
            int t1 = (int)MathHelper.Clamp(t, 0, 12);
            return x0 + t1 * (639 - x0) / 12;
        }

        public int Y(int t) { return Y(t, 0); }

        public int Y(int t, int y0)
        {
            int t1 = (int)MathHelper.Clamp(t, 0, 12);
            return y0 + t1 * (235 - y0) / 12;
        }

        public int Dx(int t) { return 639 / 12; }
        public int Dy(int t) { return 235 / 12; }
    }
}
