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

namespace MoodSwingCoreComponents
{
    public static class MSMouse
    {
        public static MouseState GetState()
        {
            Vector2 realMouseCoordinates = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 innerViewport = new Vector2(MSResolution.InnerViewPort.X, MSResolution.InnerViewPort.Y);
            Vector2 transformedMouse = Vector2.Transform(realMouseCoordinates - innerViewport, Matrix.Invert(MSResolution.GetTransformationMatrix()));
            return new MouseState((int)transformedMouse.X, (int)transformedMouse.Y, Mouse.GetState().ScrollWheelValue, Mouse.GetState().LeftButton, Mouse.GetState().MiddleButton, Mouse.GetState().RightButton, Mouse.GetState().XButton1, Mouse.GetState().XButton2);
        }
    }
}
