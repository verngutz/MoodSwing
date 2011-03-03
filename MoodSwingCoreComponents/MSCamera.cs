﻿using System;
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

        private Vector3 pitchAxis;
        private float minAngle;
        private float currAngle;
        private float maxAngle;
        private const int SHIFT_SPEED = 3;

        private const int ZOOM_MIN_DIST = 100;
        private const int ZOOM_MAX_DIST = 500;
        private const int ZOOM_SPEED = 10;

        private Matrix projectionMatrix;
        public Matrix ProjectionMatrix { get { return projectionMatrix; } }

        private BoundingFrustum frustum;

        private MSCamera()
        {}

        /// <summary>
        /// Initializes the position of the camera.
        /// </summary>
        public static void initialize( Viewport viewport )
        {
            if (camera == null) camera = new MSCamera();
            camera.upCamera = Vector3.UnitZ;
            camera.cameraPosition = new Vector3(200, 200, 150);
            camera.cameraTarget = new Vector3(0, 0, 0);
            camera.viewVector = camera.cameraPosition - camera.cameraTarget;
            camera.normalizedViewVector = Vector3.Normalize(camera.viewVector);
            camera.shiftVector = Vector3.Zero;
            camera.AdjustPitchAxis();
            camera.currAngle = (float)Math.PI / 2 - (float)Math.Acos((float)(Vector3.Dot(camera.viewVector, camera.upCamera) / (float)(Vector3.Distance(camera.cameraPosition, camera.cameraTarget))));
            camera.minAngle = camera.currAngle;
            camera.maxAngle = (float)Math.PI / 2;
            camera.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), viewport.AspectRatio, 5, 5000);
            camera.frustum = new BoundingFrustum(camera.GetView() * camera.ProjectionMatrix);
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
            if (currAngle + pitchRotationAngle > maxAngle)
                pitchRotationAngle = maxAngle - currAngle;
            else if (currAngle + pitchRotationAngle < minAngle)
                pitchRotationAngle = minAngle - currAngle;
            Matrix pitchRotationMatrix = Matrix.CreateFromAxisAngle(pitchAxis, pitchRotationAngle);
            currAngle += pitchRotationAngle;
            transformedReference = Vector3.Transform(cameraPosition, pitchRotationMatrix);
            Vector3 transformedUpCamera = Vector3.Transform(upCamera, pitchRotationMatrix);
            upCamera = transformedUpCamera;

            Matrix yawRotationMatrix = Matrix.CreateFromAxisAngle(Vector3.UnitZ, angle * rotation.X);

            
            transformedReference = Vector3.Transform(transformedReference, yawRotationMatrix);
            upCamera = Vector3.Transform(upCamera, yawRotationMatrix);
            cameraPosition = transformedReference;

            viewVector = cameraPosition - cameraTarget;
            normalizedViewVector = Vector3.Normalize(viewVector);

            AdjustPitchAxis();
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
        }

        /// <summary>
        /// Shifts the camera position closer to the camera target.
        /// </summary>
        /// <param name="direction"> 1 to zoom closer. -1 to zoom further.</param>
        public void Zoom(int direction)
        {
            Vector3 newCameraPosition = cameraPosition - (normalizedViewVector * direction * ZOOM_SPEED);
            if (Vector3.Distance(newCameraPosition, cameraTarget) >= ZOOM_MIN_DIST &&
                Vector3.Distance(newCameraPosition, cameraTarget) <= ZOOM_MAX_DIST)
            {
                cameraPosition = newCameraPosition;
                viewVector = cameraPosition - cameraTarget;
                normalizedViewVector = Vector3.Normalize(viewVector);
            }
        }
    }
}
