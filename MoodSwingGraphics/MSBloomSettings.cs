#region File Description
//-----------------------------------------------------------------------------
// BloomSettings.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

namespace MoodSwingGraphics
{
    /// <summary>
    /// Class holds all the settings used to tweak the bloom effect.
    /// </summary>
    public class MSBloomSettings
    {
        #region Fields


        // Name of a preset bloom setting, for display to the user.
        public /*readonly*/ string Name;


        // Controls how bright a pixel needs to be before it will bloom.
        // Zero makes everything bloom equally, while higher values select
        // only brighter colors. Somewhere between 0.25 and 0.5 is good.
        public /*readonly*/ float BloomThreshold;


        // Controls how much blurring is applied to the bloom image.
        // The typical range is from 1 up to 10 or so.
        public /*readonly*/ float BlurAmount;


        // Controls the amount of the bloom and base images that
        // will be mixed into the final scene. Range 0 to 1.
        public /*readonly*/ float BloomIntensity;
        public /*readonly*/ float BaseIntensity;


        // Independently control the color saturation of the bloom and
        // base images. Zero is totally desaturated, 1.0 leaves saturation
        // unchanged, while higher values increase the saturation level.
        public /*readonly*/ float BloomSaturation;
        public /*readonly*/ float BaseSaturation;


        #endregion


        /// <summary>
        /// Constructs a new bloom settings descriptor.
        /// </summary>
        public MSBloomSettings(string name, float bloomThreshold, float blurAmount,
                             float bloomIntensity, float baseIntensity,
                             float bloomSaturation, float baseSaturation)
        {
            Name = name;
            BloomThreshold = bloomThreshold;
            BlurAmount = blurAmount;
            BloomIntensity = bloomIntensity;
            BaseIntensity = baseIntensity;
            BloomSaturation = bloomSaturation;
            BaseSaturation = baseSaturation;
        }
        

        /// <summary>
        /// Table of preset bloom settings, used by the sample program.
        /// </summary>
        public static MSBloomSettings[] PresetSettings =
        {
            //                  Name           Thresh  Blur  Bloom  Base  BloomSat BaseSat
            new MSBloomSettings("Default",     0.25f,  4,    1.25f, 1,    1,       1),
            new MSBloomSettings("Soft",        0,      3,    1,     1,    1,       1),
            new MSBloomSettings("Desaturated", 0.5f,   8,    2,     1,    0,       1),
            new MSBloomSettings("Saturated",   0.25f,  4,    2,     1,    2,       0),
            new MSBloomSettings("Blurry",      0,      2,    1,     0.1f, 1,       1),
            new MSBloomSettings("Subtle",      0.62f,  1,    1.2f,  1,    1,       1),
        };
    }
}
