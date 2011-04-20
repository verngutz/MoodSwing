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
        public static MSCamera GetInstance()
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
        public Vector3 CameraTarget { get { return cameraTarget; } }

        private Vector3 viewVector;
        private Vector3 normalizedViewVector;
        public Vector3 NormalizedViewVector { get { return normalizedViewVector; } }

        private Vector3 shiftVector;
        private Vector3 startingLookAt;

        private Vector3 pitchAxis;
        private float minPitchAngle;
        private float maxPitchAngle;

        private float currYawRotation;
        private float currPitchAngle;
        private const int SHIFT_SPEED = 3;

        private const int ZOOM_MIN_DIST = 200;
        private const int ZOOM_MAX_DIST = 1000;
        private const int ZOOM_SPEED = 10;

        private Matrix projectionMatrix;
        public Matrix ProjectionMatrix { get { return projectionMatrix; } }

        private BoundingFrustum frustum;
        public BoundingFrustum Frustum { get { return frustum; } }
        private MSCamera()
        {}

        /// <summary>
        /// Initializes the position of the camera.
        /// </summary>
        public static void initialize( Viewport viewport, Vector3 initialLookAt, float rotation )
        {
            if (camera == null) camera = new MSCamera();
            camera.upCamera = Vector3.UnitZ;
            camera.cameraPosition = new Vector3(282f, 4.5f, 150);
            camera.cameraTarget = new Vector3(0, 0, 0);
            camera.viewVector = camera.cameraPosition - camera.cameraTarget;
            camera.normalizedViewVector = Vector3.Normalize(camera.viewVector);
            camera.shiftVector = initialLookAt;
            camera.startingLookAt = initialLookAt;
            camera.AdjustPitchAxis();
            camera.currPitchAngle = (float)Math.PI / 2 - (float)Math.Acos((float)(Vector3.Dot(camera.viewVector, camera.upCamera) / (float)(Vector3.Distance(camera.cameraPosition, camera.cameraTarget))));
            camera.minPitchAngle = (float)Math.PI / 9;
            camera.maxPitchAngle = (float)Math.PI / 2;
            camera.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), viewport.AspectRatio, 5, 5000);
            camera.frustum = new BoundingFrustum(camera.GetView() * camera.ProjectionMatrix);

            Matrix yawRotationMatrix = Matrix.CreateFromAxisAngle(Vector3.UnitZ, rotation);
            Vector3 transformedReference = Vector3.Transform(camera.cameraPosition, yawRotationMatrix);
            camera.upCamera = Vector3.Transform(camera.upCamera, yawRotationMatrix);
            camera.cameraPosition = transformedReference;
            camera.currYawRotation = rotation / 0.005f;
        }

        public void AdjustPitchAxis()
        {
            pitchAxis = Vector3.Normalize(Vector3.Cross(viewVector, upCamera));
        }

        public Matrix GetView()
        {
            return Matrix.CreateLookAt(cameraPosition+shiftVector, cameraTarget+shiftVector, upCamera);
        }

        /// <summary>
        /// Rotates the camera.
        /// </summary>
        /// <param name="rotation">A Vector2D whose X coordinate represents the direction of the angle of the yaw rotation 
        /// and whose Y coordinate represents the direction of the angle of the pitch rotation</param>
        public void Rotate( Vector2 rotation )
        {
            float angle = .005f;
            Vector3 transformedReference;
            
            float pitchRotationAngle = angle * rotation.Y;
            if (currPitchAngle + pitchRotationAngle > maxPitchAngle)
                pitchRotationAngle = maxPitchAngle - currPitchAngle;
            else if (currPitchAngle + pitchRotationAngle < minPitchAngle)
                pitchRotationAngle = minPitchAngle - currPitchAngle;
            Matrix pitchRotationMatrix = Matrix.CreateFromAxisAngle(pitchAxis, pitchRotationAngle);
            currPitchAngle += pitchRotationAngle;
            transformedReference = Vector3.Transform(cameraPosition, pitchRotationMatrix);
            Vector3 transformedUpCamera = Vector3.Transform(upCamera, pitchRotationMatrix);
            upCamera = transformedUpCamera;

            Matrix yawRotationMatrix = Matrix.CreateFromAxisAngle(Vector3.UnitZ, angle * rotation.X);   
            transformedReference = Vector3.Transform(transformedReference, yawRotationMatrix);
            upCamera = Vector3.Transform(upCamera, yawRotationMatrix);
            cameraPosition = transformedReference;
            currYawRotation += rotation.X;

            viewVector = cameraPosition - cameraTarget;
            normalizedViewVector = Vector3.Normalize(viewVector);

            AdjustPitchAxis();
            frustum = new BoundingFrustum(camera.GetView() * camera.ProjectionMatrix);
        }

        /// <summary>
        /// Shifts the camera position and camera target in a specific direction.
        /// </summary>
        /// <param name="dV"> A Vector2D whose X coordinate represents the direction of the movements from left to right and 
        /// whose Y coordinate represents the direction of the up and down movement.</param>
        /// <param name="dim">A Vector2D representing the dimension of the map. Used for clamping.</param>
        public void Shift(Vector2 dV, Vector2 dim )
        {
            Vector3 shift = dV.X * pitchAxis + dV.Y * Vector3.Normalize(Vector3.Cross(Vector3.UnitZ, pitchAxis));
            
            shiftVector += shift * SHIFT_SPEED;
            Vector3 dim3 = new Vector3(dim.X, dim.Y, 0);
            shiftVector = Vector3.Clamp(shiftVector, Vector3.Zero, dim3);
            frustum = new BoundingFrustum(camera.GetView() * camera.ProjectionMatrix);
        }

        /// <summary>
        /// Shifts the camera position closer to the camera target.
        /// </summary>
        /// <param name="direction"> 1 to zoom closer. -1 to zoom further.</param>
        public bool Zoom(int direction)
        {
            bool toReturn = false;
            Vector3 newCameraPosition = cameraPosition - (normalizedViewVector * direction * ZOOM_SPEED);
            if (Vector3.Distance(newCameraPosition, cameraTarget) >= ZOOM_MIN_DIST &&
                Vector3.Distance(newCameraPosition, cameraTarget) <= ZOOM_MAX_DIST)
            {
                cameraPosition = newCameraPosition;
                viewVector = cameraPosition - cameraTarget;
                normalizedViewVector = Vector3.Normalize(viewVector);
                toReturn = true;
            }
            frustum = new BoundingFrustum(camera.GetView() * camera.ProjectionMatrix);

            return toReturn;
        }


        public void BirdsEyeView()
        {
            shiftVector = startingLookAt;
            Rotate( new Vector2(-currYawRotation, (float)(Math.PI/.005)));
            while (Zoom(-1)) { }
            currYawRotation = 0;
        }
    }
}
