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

        private Vector3 upCamera;
        public Vector3 UpCamera { get { return upCamera; } }
        private Vector3 cameraPosition;
        public Vector3 Position { get { return cameraPosition+shiftVector; } }
        private Vector3 cameraTarget;
        private Vector3 pitchAxis;
        private Vector3 shiftVector;
        public void adjustPitchAxis()
        {
            pitchAxis = Vector3.Normalize(Vector3.Cross(cameraPosition - cameraTarget, upCamera));
        }

        private float initAngle;
        
        private MSCamera()
        {
            upCamera = Vector3.UnitZ;
            cameraPosition = new Vector3(200, 200, 200);
            cameraTarget = new Vector3(0, 0, 0);
            shiftVector = Vector3.Zero;
            adjustPitchAxis();
            initAngle = (float)Math.Acos((float)(Vector3.Dot(cameraPosition - cameraTarget, upCamera) / (float)(Vector3.Distance(cameraPosition, cameraTarget)) ) );
        }

        public Matrix getView()
        {
            return Matrix.CreateLookAt(cameraPosition+shiftVector, cameraTarget+shiftVector, upCamera);
        }

        private const int ZOOM_MIN_DIST = 100;
        private const int ZOOM_MAX_DIST = 500;
        private const int ZOOM_SPEED = 10;
        public void zoom(int direction)
        {
            Vector3 unit = Vector3.Normalize(cameraPosition - cameraTarget);
            Vector3 newCameraPosition = cameraPosition - (unit * direction * ZOOM_SPEED);
            if (Vector3.Distance(newCameraPosition, cameraTarget) >= ZOOM_MIN_DIST &&
                Vector3.Distance(newCameraPosition, cameraTarget) <= ZOOM_MAX_DIST)
            {
                cameraPosition = newCameraPosition;
            }
            
        }

        private const int SHIFT_SPEED = 3;
        public void shift(Vector2 dV)
        {
            Vector3 shift = dV.X * pitchAxis + dV.Y * Vector3.Normalize(Vector3.Cross(upCamera,pitchAxis));
            shiftVector += shift * SHIFT_SPEED;
        }

        public void rotate( Vector2 rotation )
        {
            float angle = .005f;
            Vector3 transformedReference;
            Matrix pitchRotationMatrix = Matrix.CreateFromAxisAngle(pitchAxis, angle * rotation.Y);
            
            transformedReference = Vector3.Transform(cameraPosition, pitchRotationMatrix);
            Vector3 transformedUpCamera = Vector3.Transform(upCamera, pitchRotationMatrix);
            upCamera = transformedUpCamera;

            Matrix yawRotationMatrix = Matrix.CreateFromAxisAngle(Vector3.UnitZ, angle * rotation.X);

            
            transformedReference = Vector3.Transform(transformedReference, yawRotationMatrix);
            upCamera = Vector3.Transform(upCamera, yawRotationMatrix);
            cameraPosition = transformedReference ;
            adjustPitchAxis();

        }
    
    }

}
