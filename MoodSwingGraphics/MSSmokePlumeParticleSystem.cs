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

            settings.MaxParticles = 10000;

            settings.Duration = TimeSpan.FromSeconds(2);

            settings.MinHorizontalVelocity = -2;
            settings.MaxHorizontalVelocity = 2;

            settings.MinVerticalVelocity = -2;
            settings.MaxVerticalVelocity = 2;

            // Create a wind effect by tilting the gravity vector sideways.
            settings.Gravity = new Vector3(0, 0, 0);

            settings.EndVelocity = 0;

            settings.MinStartSize = 5;
            settings.MaxStartSize = 10;

            settings.MinEndSize = 10;
            settings.MaxEndSize = 20;
        }
    }
}
