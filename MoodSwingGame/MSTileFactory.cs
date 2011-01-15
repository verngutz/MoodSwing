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
    public class MSTileFactory
    {
        
        public MSTileFactory()
        {

        }
        public static MSTile createMSTile(int tileKey, Vector2 position, Vector2 size)
        {
            switch (tileKey - ((tilekey / 10) * 10))
            {
                case 0:
                    return (new MSBuilding(position,size,MoodSwing.getInstance().SpriteBatch,MoodSwing.getInstance().Content.Load<Texture2D>("file")));
                    break;
                case 1:
                    switch((tileKey / 10) - ((tilekey / 100) * 10))
                    {
                        case 0:
                            return (new MSRoad(position,size,MoodSwing.getInstance().SpriteBatch,MoodSwing.getInstance().Content.Load<Texture2D>("Road_0"),0));
                            break;
                        case 1:
                            int rotation = 0;
                            switch((tileKey / 100) - ((tilekey / 1000) * 10))
                            {
                                case 1:
                                    rotation = MathHelper.ToRadians(90);
                                    break;
                                case 2:
                                    rotation = MathHelper.ToRadians(180);
                                    break;
                                case 3:
                                    rotation = MathHelper.ToRadians(270);
                                    break;
                            }
                            return (new MSRoad(position,size,MoodSwing.getInstance().SpriteBatch,MoodSwing.getInstance().Content.Load<Texture2D>("Road_1"),rotation));
                            break;
                        case 2:
                            switch((tileKey / 100) - ((tilekey / 1000) * 10))
                            {
                                case 0:
                                    int rotation = 0;
                                    switch ((tileKey / 1000) - ((tilekey / 10000) * 10))
                                    {
                                        case 1:
                                            rotation = MathHelper.ToRadians(90);
                                            break;
                                        case 2:
                                            rotation = MathHelper.ToRadians(180);
                                            break;
                                        case 3:
                                            rotation = MathHelper.ToRadians(270);
                                            break;
                                    }
                                    return (new MSRoad(position, size, MoodSwing.getInstance().SpriteBatch, MoodSwing.getInstance().Content.Load<Texture2D>("Road_2_0"), rotation));
                                    break;
                                case 1:
                                    int rotation = 0;
                                    switch ((tileKey / 1000) - ((tilekey / 10000) * 10))
                                    {
                                        case 1:
                                            rotation = MathHelper.ToRadians(90);
                                            break;
                                    }
                                    return (new MSRoad(position, size, MoodSwing.getInstance().SpriteBatch, MoodSwing.getInstance().Content.Load<Texture2D>("Road_2_1"), rotation));
                                    break;
                            }
                            break;
                        case 3:
                            int rotation = 0;
                            switch ((tileKey / 100) - ((tilekey / 1000) * 10))
                            {
                                case 1:
                                    rotation = MathHelper.ToRadians(90);
                                    break;
                                case 2:
                                    rotation = MathHelper.ToRadians(180);
                                    break;
                                case 3:
                                    rotation = MathHelper.ToRadians(270);
                                    break;
                            }
                            return (new MSRoad(position, size, MoodSwing.getInstance().SpriteBatch, MoodSwing.getInstance().Content.Load<Texture2D>("Road_3"), rotation));
                            break;
                        case 4:
                            return (new MSRoad(position,size,MoodSwing.getInstance().SpriteBatch,MoodSwing.getInstance().Content.Load<Texture2D>("Road_4"),0));
                            break;
                    }
                    return (new MSRoad(position,size,MoodSwing.getInstance().SpriteBatch,MoodSwing.getInstance().Content.Load<Texture2D>("Road_4_0"),rotation));
                    break;
            }
        }
    }
}