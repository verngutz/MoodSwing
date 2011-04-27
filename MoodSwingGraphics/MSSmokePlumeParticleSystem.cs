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
        public const int THICKNESS = 10;
        public MSSmokePlumeParticleSystem(Game game)
            : base(game)
        { }


        protected override void InitializeSettings(MSParticleSettings settings)
        {
            settings.TextureName = "smoke2";

            settings.MaxParticles = 600;

            settings.Duration = TimeSpan.FromSeconds(7);

            // Create a wind effect by tilting the gravity vector sideways.
            settings.Gravity = new Vector3(0, 0, -2);

            settings.EndVelocity = 0;

            settings.MinStartSize = 20;
            settings.MaxStartSize = 50;

            settings.MinEndSize = 12;
            settings.MaxEndSize = 16;

            settings.MinColor = new Color(255, 255, 255, 60);
            settings.MaxColor = new Color(255, 255, 255, 90);

            settings.BlendState = BlendState.Additive;
        }
    }
}
