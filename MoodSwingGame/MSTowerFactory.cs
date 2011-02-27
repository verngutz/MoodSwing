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
using MoodSwingCoreComponents;
using MoodSwingGUI;

namespace MoodSwingGame
{
    public class MSTowerFactory
    {
        public static MSTower CreateMSTower( MSTowerStats stats, Vector3 position, Vector2 tileCoords )
        {
            MoodSwing moodSwing = MoodSwing.getInstance();
            Model model = null;
            Texture2D texture = null;
            Effect effect = null;

            if (stats is MSFoodCenterStats)
            {
                model = moodSwing.Content.Load<Model>("districthall");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/building_A");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSTutorialCenterStats)
            {
                model = moodSwing.Content.Load<Model>("districthall");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/building_B");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSWomensOrgStats)
            {
                model = moodSwing.Content.Load<Model>("districthall");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/building_C");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSHealthCenterStats)
            {
                model = moodSwing.Content.Load<Model>("districthall");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/building_D");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSEcoParkStats)
            {
                model = moodSwing.Content.Load<Model>("districthall");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/building_E");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSGlobalCenterStats)
            {
                model = moodSwing.Content.Load<Model>("districthall");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/building_B");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            return new MSTower(model,texture, effect,position, (int)tileCoords.X, (int)tileCoords.Y, stats);
                    
        }
    }
}
