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

        private float initAngle;
        
        private MSCamera()
        {
            cameraPosition = new Vector3(200, 200, 200);
            cameraTarget = new Vector3(0, 0, 0);
            adjustPitchAxis();
            initAngle = (float)Math.Acos((float)(Vector3.Dot(cameraPosition - cameraTarget, Vector3.UnitZ) / (float)(Vector3.Distance(cameraPosition, cameraTarget)) ) );
        }

        public Matrix getView()
        {
            return Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.UnitZ);
        }

        private const int ZOOM_MIN_DIST = 100;
        private const int ZOOM_MAX_DIST = 500;
        public void zoom(int direction)
        {
            Vector3 unit = Vector3.Normalize(cameraPosition - cameraTarget);
            Vector3 newCameraPosition = cameraPosition - (unit * direction * 8);
            if (Vector3.Distance(newCameraPosition, cameraTarget) >= ZOOM_MIN_DIST &&
                Vector3.Distance(newCameraPosition, cameraTarget) <= ZOOM_MAX_DIST)
            {
                cameraPosition = newCameraPosition;
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
            float angle = .001f;
            float distance = Vector3.Distance(cameraPosition - cameraTarget, Vector3.UnitZ);

            Vector3 transformedReference;
            Matrix pitchRotationMatrix = Matrix.CreateFromAxisAngle(pitchAxis, angle* rotation.Y);
            Matrix yawRotationMatrix = Matrix.CreateFromAxisAngle(Vector3.UnitZ, angle * rotation.X);

            transformedReference = Vector3.Transform(cameraPosition, pitchRotationMatrix);

            if (initAngle + (rotation.Y * angle) >= 0 && initAngle + (rotation.Y * angle) < Math.PI / 2)
            {
                transformedReference = Vector3.Transform(transformedReference, yawRotationMatrix);
                initAngle = initAngle + rotation.Y * angle;
            }
            else
                transformedReference = Vector3.Transform(cameraPosition, yawRotationMatrix);
            cameraPosition = transformedReference ;
            adjustPitchAxis();

        }
    
    }

}
