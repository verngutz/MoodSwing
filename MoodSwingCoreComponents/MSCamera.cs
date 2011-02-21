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
        private float initAngle;

        private const int SHIFT_SPEED = 3;

        private const int ZOOM_MIN_DIST = 100;
        private const int ZOOM_MAX_DIST = 500;
        private const int ZOOM_SPEED = 10;

        public static Vector3 UNIVERSAL_LIGHT_SOURCE = new Vector3(0, 0, 1000);
        
        private MSCamera()
        {
            upCamera = Vector3.UnitZ;
            cameraPosition = new Vector3(200, 200, 200);
            cameraTarget = new Vector3(0, 0, 0);
            viewVector = cameraPosition - cameraTarget;
            normalizedViewVector = Vector3.Normalize(viewVector);
            shiftVector = Vector3.Zero;
            AdjustPitchAxis();
            initAngle = (float)Math.Acos((float)(Vector3.Dot(viewVector, upCamera) / (float)(Vector3.Distance(cameraPosition, cameraTarget)) ) );
        }

        public void AdjustPitchAxis()
        {
            pitchAxis = Vector3.Normalize(Vector3.Cross(viewVector, upCamera));
        }

        public Matrix GetView()
        {
            return Matrix.CreateLookAt(cameraPosition+shiftVector, cameraTarget+shiftVector, upCamera);
        }

        public void Rotate( Vector2 rotation )
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
            cameraPosition = transformedReference;

            viewVector = cameraPosition - cameraTarget;
            normalizedViewVector = Vector3.Normalize(viewVector);

            AdjustPitchAxis();
        }

        public void Shift(Vector2 dV)
        {
            Vector3 shift = dV.X * pitchAxis + dV.Y * Vector3.Normalize(Vector3.Cross(Vector3.UnitZ, pitchAxis));
            shiftVector += shift * SHIFT_SPEED;
        }

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
