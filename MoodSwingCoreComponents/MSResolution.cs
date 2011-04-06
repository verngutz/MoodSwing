using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MoodSwingCoreComponents
{
    public static class MSResolution
    {
        private static GraphicsDeviceManager device = null;

        private static int width = 800;
        private static int height = 600;
        private static int virtualWidth = 1024;
        private static int virtualHeight = 768;
        private static Matrix scaleMatrix;
        private static bool isFullScreen = false;
        private static bool dirtyMatrix = true;
        private static Viewport innerViewport;

        public static int Width { get { return width; } }
        public static int Height { get { return height; } }
        public static int VirtualWidth { get { return virtualWidth; } }
        public static int VirtualHeight { get { return virtualHeight; } }
        public static Viewport InnerViewPort { get { return innerViewport; } }
        public static bool IsFullScreen 
        {
            set
            {
                isFullScreen = value;
                ApplyResolutionSettings();
            }
            get { return isFullScreen; } 
        }

        public static void Init(ref GraphicsDeviceManager device)
        {
            width = device.PreferredBackBufferWidth;
            height = device.PreferredBackBufferHeight;
            MSResolution.device = device;
            dirtyMatrix = true;
            ApplyResolutionSettings();
        }


        public static Matrix GetTransformationMatrix()
        {
            if (dirtyMatrix) RecreateScaleMatrix();

            return scaleMatrix;
        }

        public static void SetResolution(int new_width, int new_height)
        {
            width = new_width;
            height = new_height;
            ApplyResolutionSettings();
        }

        public static void SetResolution(int new_width, int new_height, bool fullScreen)
        {
            width = new_width;
            height = new_height;
            isFullScreen = fullScreen;
            ApplyResolutionSettings();
        }

        public static void SetVirtualResolution(int Width, int Height)
        {
            virtualWidth = Width;
            virtualHeight = Height;

            dirtyMatrix = true;
        }

        static private void ApplyResolutionSettings()
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (isFullScreen == false)
            {
                if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    device.PreferredBackBufferWidth = width;
                    device.PreferredBackBufferHeight = height;
                    device.IsFullScreen = isFullScreen;
                    device.ApplyChanges();
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate through the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    if ((dm.Width == width) && (dm.Height == height))
                    {
                        // The mode is supported, so set the buffer formats, apply changes and return
                        device.PreferredBackBufferWidth = width;
                        device.PreferredBackBufferHeight = height;
                        device.IsFullScreen = isFullScreen;
                        device.ApplyChanges();
                    }
                }
            }

            dirtyMatrix = true;

            width = device.PreferredBackBufferWidth;
            height = device.PreferredBackBufferHeight;
        }

        /// <summary>
        /// Sets the device to use the draw pump
        /// Sets correct aspect ratio
        /// </summary>
        public static void BeginDraw()
        {
            // Start by reseting viewport to (0,0,1,1)
            FullViewport();
            // Clear to Black
            device.GraphicsDevice.Clear(Color.Black);
            // Calculate Proper Viewport according to Aspect Ratio
            ResetViewport();
            // and clear that
            // This way we are gonna have black bars if aspect ratio requires it and
            // the clear color on the rest
            device.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        static private void RecreateScaleMatrix()
        {
            dirtyMatrix = false;
            scaleMatrix = Matrix.CreateScale(
                           (float)device.GraphicsDevice.Viewport.Width / virtualWidth,
                           (float)device.GraphicsDevice.Viewport.Width / virtualWidth,
                           1f);
        }


        public static void FullViewport()
        {
            Viewport vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = width;
            vp.Height = height;
            device.GraphicsDevice.Viewport = vp;
        }

        /// <summary>
        /// Get virtual Mode Aspect Ratio
        /// </summary>
        /// <returns>aspect ratio</returns>
        public static float GetVirtualAspectRatio()
        {
            return (float)virtualWidth / (float)virtualHeight;
        }

        public static void ResetViewport()
        {
            float targetAspectRatio = GetVirtualAspectRatio();
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            int width = device.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);
            bool changed = false;

            if (height > device.PreferredBackBufferHeight)
            {
                height = device.PreferredBackBufferHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;
            }

            // set up the new viewport centered in the backbuffer
            Viewport viewport = new Viewport();

            viewport.X = (device.PreferredBackBufferWidth / 2) - (width / 2);
            viewport.Y = (device.PreferredBackBufferHeight / 2) - (height / 2);
            viewport.Width = width;
            viewport.Height = height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            if (changed)
            {
                dirtyMatrix = true;
            }

            device.GraphicsDevice.Viewport = viewport;
            innerViewport = viewport;
        }

    }
}