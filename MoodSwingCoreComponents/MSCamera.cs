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
    public class MSCamera
    {
        private static MSCamera camera;
        public static MSCamera getInstance()
        {
            if (camera == null)
                camera = new MSCamera();
            return camera;
        }

        private Vector3 cameraPosition;
        private Vector3 cameraTarget;

        private MSCamera()
        {
            cameraPosition = new Vector3(200, 200, 200);
            cameraTarget = new Vector3(50, 50, 0);
        }

        public Matrix getView()
        {
            return Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.UnitZ);
        }

        public void shift(Vector3 dV)
        {
            cameraPosition += dV;
            cameraTarget += dV;
        }
    }
}
