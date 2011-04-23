#region File Description
//-----------------------------------------------------------------------------
// SmokePlumeParticleSystem.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace MoodSwingGraphics
{
    /// <summary>
    /// Custom particle system for creating a giant plume of long lasting smoke.
    /// </summary>
    public class MSSmokePlumeParticleSystem : MSParticleSystem
    {
        public const int THICKNESS = 30;
        public MSSmokePlumeParticleSystem(Game game)
            : base(game)
        { }


        protected override void InitializeSettings(MSParticleSettings settings)
        {
            settings.TextureName = "smoke2";

            settings.MaxParticles = 6000;

            settings.Duration = TimeSpan.FromSeconds(2);

            settings.MinHorizontalVelocity = -5;
            settings.MaxHorizontalVelocity = 5;

            settings.MinVerticalVelocity = -5;
            settings.MaxVerticalVelocity = 5;

            // Create a wind effect by tilting the gravity vector sideways.
            settings.Gravity = new Vector3(0, 0, 0);

            settings.EndVelocity = 0;

            settings.MinStartSize = 20;
            settings.MaxStartSize = 40;

            settings.MinEndSize = 20;
            settings.MaxEndSize = 40;
        }
    }
}
