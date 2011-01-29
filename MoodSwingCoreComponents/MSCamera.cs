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
        public Vector3 Position { get { return cameraPosition; } }
        private Vector3 cameraTarget;
        private Vector3 pitchAxis;
        public void adjustPitchAxis()
        {
            System.Console.WriteLine("HELLO");
            pitchAxis = Vector3.Normalize(Vector3.Cross(cameraPosition - cameraTarget, Vector3.UnitZ));
        }

        private MSCamera()
        {
            cameraPosition = new Vector3(200, 200, 200);
            cameraTarget = new Vector3(0, 0, 0);
            adjustPitchAxis();
        }

        public Matrix getView()
        {
            return Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.UnitZ);
        }

        private const int ZOOM_LIMIT = 20;
        private int zoomCount = 0;
        public void zoom(int direction)
        {

            if (Math.Abs(zoomCount + direction) < ZOOM_LIMIT)
            {
                zoomCount += direction;
                Vector3 unit = Vector3.Normalize(cameraPosition - cameraTarget);
                cameraPosition -= unit * direction * 5;
            }
        }
        public void shift(Vector2 dV)
        {
            Vector3 shift = dV.X * pitchAxis + dV.Y * Vector3.Normalize(Vector3.Cross(Vector3.UnitZ,pitchAxis));
            cameraPosition += shift;
            cameraTarget += shift;
        }

        public void rotate( Vector2 rotation )
        {
            float angle = 0.01f;
           // Matrix rotationMatrix = Matrix.CreateRotationZ(angle);
            Vector3 transformedReference;
            
            
            Matrix pitchRotationMatrix = Matrix.CreateFromAxisAngle(pitchAxis, angle * 1);
            transformedReference = Vector3.Transform(cameraPosition, pitchRotationMatrix);
            cameraPosition = transformedReference;



        }
    
    }

}
