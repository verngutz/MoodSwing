using System;
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
            System.Console.WriteLine(tileKey);
            switch (tileKey - ((tileKey / 10) * 10))
            {
                case 0:
                    return (new MSBuilding(position,size,MoodSwing.getInstance().SpriteBatch,MoodSwing.getInstance().Content.Load<Texture2D>("file")));
                case 1:
                    float rotation = 0;
                    switch((tileKey / 10) - ((tileKey / 100) * 10))
                    {
                        case 0:
                            System.Console.WriteLine("Road 0");
                            return (new MSRoad(position,size,MoodSwing.getInstance().SpriteBatch,MoodSwing.getInstance().Content.Load<Texture2D>("Floors/Road_0"),0));
                        case 1:
                            switch((tileKey / 100) - ((tileKey / 1000) * 10))
                            {
                                case 0:
                                    rotation = 0;
                                    break;
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
                            System.Console.WriteLine("Road 1");
                            return (new MSRoad(position, size, MoodSwing.getInstance().SpriteBatch, MoodSwing.getInstance().Content.Load<Texture2D>("Floors/Road_1"), rotation));
                        case 2:
                            switch((tileKey / 100) - ((tileKey / 1000) * 10))
                            {
                                case 0:
                                    switch ((tileKey / 1000) - ((tileKey / 10000) * 10))
                                    {
                                        case 0:
                                            rotation = 0;
                                            break;
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
                                    System.Console.WriteLine("Road 2.0");
                                    return (new MSRoad(position, size, MoodSwing.getInstance().SpriteBatch, MoodSwing.getInstance().Content.Load<Texture2D>("Floors/Road_2_0"), rotation));
                                case 1:
                                    switch ((tileKey / 1000) - ((tileKey / 10000) * 10))
                                    {
                                        case 0:
                                            rotation = 0;
                                            break;
                                        case 1:
                                            rotation = MathHelper.ToRadians(90);
                                            break;
                                    }
                                    System.Console.WriteLine("Road 2.1");
                                    return (new MSRoad(position, size, MoodSwing.getInstance().SpriteBatch, MoodSwing.getInstance().Content.Load<Texture2D>("Floors/Road_2_1"), rotation));
                            }
                            break;
                        case 3:
                            switch ((tileKey / 100) - ((tileKey / 1000) * 10))
                            {
                                case 0:
                                    rotation = 0;
                                    break;
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
                            System.Console.WriteLine("Road 3");
                            return (new MSRoad(position, size, MoodSwing.getInstance().SpriteBatch, MoodSwing.getInstance().Content.Load<Texture2D>("Floors/Road_3"), rotation));
                        case 4:
                            System.Console.WriteLine("Road 4");
                            return (new MSRoad(position, size, MoodSwing.getInstance().SpriteBatch, MoodSwing.getInstance().Content.Load<Texture2D>("Floors/Road_4"), 0));
                    }
                    System.Console.WriteLine("Road 5");
                    return (new MSRoad(position, size, MoodSwing.getInstance().SpriteBatch, MoodSwing.getInstance().Content.Load<Texture2D>("Floors/Road_4_0"), rotation));
            }
            return null;
        }
    }
}
