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
        public MSSmokePlumeParticleSystem(Game game)
            : base(game)
        { }


        protected override void InitializeSettings(MSParticleSettings settings)
        {
            settings.TextureName = "smoke";

            settings.MaxParticles = 600;

            settings.Duration = TimeSpan.FromSeconds(2);

            settings.MinHorizontalVelocity = -14;
            settings.MaxHorizontalVelocity = 14;

            settings.MinVerticalVelocity = -14;
            settings.MaxVerticalVelocity = 14;

            // Create a wind effect by tilting the gravity vector sideways.
            settings.Gravity = new Vector3(0, 0, 0);

            settings.EndVelocity = 0.75f;

            settings.MinRotateSpeed = -1;
            settings.MaxRotateSpeed = 1;

            settings.MinStartSize = 50;
            settings.MaxStartSize = 100;

            settings.MinEndSize = 10;
            settings.MaxEndSize = 20;
        }
    }
}
