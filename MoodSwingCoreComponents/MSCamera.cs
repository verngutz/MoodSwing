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
            float angle = .01f;
           // Matrix rotationMatrix = Matrix.CreateRotationZ(angle);
            Vector3 transformedReference;


            //Matrix x = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up) * Matrix.CreateFromYawPitchRoll(0, 0, 0);


            Matrix pitchRotationMatrix = Matrix.CreateFromAxisAngle(pitchAxis, angle);
        
            transformedReference = Vector3.Transform(cameraPosition, pitchRotationMatrix);
            //cameraPosition = transformedReference;
            Vector3 a = Vector3.Normalize(transformedReference);
            Vector3 b = Vector3.Normalize(cameraPosition);

            System.Console.WriteLine(MathHelper.ToDegrees((float)Math.Acos(Vector3.Dot(a, b))));
            if (Math.Acos(Vector3.Dot(a, b)) < MathHelper.ToRadians(1))
            {
                cameraPosition = transformedReference;
            }

        }
    
    }

}
