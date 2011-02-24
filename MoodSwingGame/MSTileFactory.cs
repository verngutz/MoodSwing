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
        public static MS3DTile CreateMSTile(int tileKey, Vector3 position, int row, int column)
        {
            /*
             * Legend:
             * 0    -   basic building
             * 10   -   buyable building
             * 20   -   district hall
             * 1    -   empty passable land
             * 11   -   road end, prefix with 0 to 3 for counterclockwise rotation
             * 021  -   L road bending from top to left, prefix with 0 to 3 for counterclockwise rotation
             * 121  -   straight vertical road, prefix with 0 to 1 for rotation
             * 31   -   T road vertical with branch to the left, prefix with 0 to 3 for counterclockwise rotation
             * 41   -   crossroads
             */

            ContentManager c = MoodSwing.getInstance().Content;
            switch (tileKey - ((tileKey / 10) * 10))
            {
                case 0:
                    switch ((tileKey / 10) - ((tileKey / 100) * 10))
                    {
                        case 0:
                            return new MSUnbuyableBuilding(c.Load<Model>("building"),
                                c.Load<Texture2D>("MTextures/building_texture"),
                                c.Load<Effect>("Mood"), 
                                position, row, column);
                        case 1:
                            return new MSBuyableBuilding(c.Load<Model>("districthall"), c.Load<Texture2D>("MTextures/cube3_auv"), c.Load<Effect>("Mood"), position, row, column);

                        case 2:
                            MSDistrictHall.instantiate(c.Load<Model>("districthall"), 
                                c.Load<Texture2D>("MTextures/districthall"),
                                c.Load<Effect>("Mood"),
                                position, row, column);
                            MSDistrictHall hall = MSDistrictHall.getInstance();
                            return hall;
                    }
                    break;
                case 1:
                    string modelname = "";
                    string textureName = "";
                    float rotation = 0;
                    switch((tileKey / 10) - ((tileKey / 100) * 10))
                    {
                        case 0:
                            modelname = "Floors/1";
                            textureName = "MTextures/1";
                            rotation = 0;
                            break;
                        case 1:
                            modelname = "Floors/11";
                            textureName = "MTextures/11";
                            rotation = MathHelper.ToRadians(((tileKey / 100) - ((tileKey / 1000) * 10)) * 90);
                            break;
                        case 2:
                            switch((tileKey / 100) - ((tileKey / 1000) * 10))
                            {
                                case 0:
                                    modelname = "Floors/21";
                                    textureName = "MTextures/21";
                                    rotation = MathHelper.ToRadians(((tileKey / 1000) - ((tileKey / 10000) * 10)) * 90);
                                    break;
                                case 1:
                                    modelname = "Floors/121";
                                    textureName = "MTextures/121";
                                    rotation = MathHelper.ToRadians(((tileKey / 1000) - ((tileKey / 10000) * 10)) * 90);
                                    break;
                            }
                            break;
                        case 3:
                            modelname = "Floors/31";
                            textureName = "MTextures/31";
                            rotation = MathHelper.ToRadians(((tileKey / 100) - ((tileKey / 1000) * 10)) * 90);
                            break;
                        case 4:
                            modelname = "Floors/41";
                            textureName = "MTextures/41";
                            rotation = 0;
                            break;
                    }
                    return new MSRoad(c.Load<Model>(modelname), c.Load<Texture2D>(textureName), c.Load<Effect>("Mood"), position, rotation, row, column);
            }
            return null;
        }
    }
}
