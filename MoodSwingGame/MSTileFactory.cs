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
        public static MSTile CreateMSTile(int tileKey, Vector3 position)
        {
            ContentManager c = MoodSwing.getInstance().Content;
            switch (tileKey - ((tileKey / 10) * 10))
            {
                case 0:
                    switch ((tileKey / 10) - ((tileKey / 100) * 10))
                    {
                        case 0:
                            return new MSBuilding(c.Load<Model>("building"), position );
                        case 1:
                            return new MSBuilding(c.Load<Model>("cubeWithColors"), position);
                        case 2:
                            MSDistrictHall.instantiate(c.Load<Model>("districthall"), position);
                            MSDistrictHall hall = MSDistrictHall.getInstance();
                            
                            
                            return hall;
                            
                    }
                    break;
                case 1:
                    string modelname = "";
                    float rotation = 0;
                    switch((tileKey / 10) - ((tileKey / 100) * 10))
                    {
                        case 0:
                            modelname = "cubeWithColors";
                            rotation = 0;
                            break;
                        case 1:
                            modelname = "cubeWithColors";
                            rotation = MathHelper.ToRadians(((tileKey / 100) - ((tileKey / 1000) * 10)) * 90);
                            break;
                        case 2:
                            switch((tileKey / 100) - ((tileKey / 1000) * 10))
                            {
                                case 0:
                                    modelname = "cubeWithColors";
                                    rotation = MathHelper.ToRadians(((tileKey / 1000) - ((tileKey / 10000) * 10)) * 90);
                                    break;
                                case 1:
                                    modelname = "cubeWithColors";
                                    rotation = MathHelper.ToRadians(((tileKey / 1000) - ((tileKey / 10000) * 10)) * 90);
                                    break;
                            }
                            break;
                        case 3:
                            modelname = "cubeWithColors";
                            rotation = MathHelper.ToRadians(((tileKey / 100) - ((tileKey / 1000) * 10)) * 90);
                            break;
                        case 4:
                            modelname = "cubeWithColors";
                            rotation = 0;
                            break;
                    }
                    return new MSRoad(c.Load<Model>(modelname), position, rotation);
            }
            return null;
        }
    }
}
