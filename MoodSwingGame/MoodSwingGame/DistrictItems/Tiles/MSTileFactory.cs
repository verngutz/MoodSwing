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
             * Legend: (suffix with 0 to 3 for counterclockwise rotation)
             * 0    -   basic building
             * 10   -   buyable building
             * 20   -   district hall
             * 30   -   volunteer centre
             * 1    -   empty passable land
             * 11   -   road end
             * 21  -   L road bending from top to left
             * 121  -   straight vertical road
             * 31   -   T road vertical with branch to the left
             * 41   -   crossroads
             */

            Vector3 shortBuildingOffset = new Vector3(0, 0, (MSMap.shortheight - MSMap.floorheight) / 2);
            Vector3 tallBuildingOffset = new Vector3(0, 0, (MSMap.tallheight - MSMap.floorheight) / 2);
            Vector3 hallBuildingOffset = new Vector3(0, 0, (MSMap.hallheight - MSMap.floorheight) / 2);

            ContentManager c = MoodSwing.GetInstance().Content;
            float rotation = MathHelper.ToRadians((tileKey - ((tileKey / 10) * 10)) * 90);

            tileKey /= 10;

            switch (tileKey - ((tileKey / 10) * 10))
            {
                case 0:
                    switch ((tileKey / 10) - ((tileKey / 100) * 10))
                    {
                        case 0:
                            return new MSUnchangeableBuilding("MModels/BuildingSmall",
                                "MTextures/BuildingSmall",
                                "Mood",
                                position + shortBuildingOffset, rotation, row, column);
                        case 1:
                            return new MSAbandonedBuilding("MModels/BuildingBig",
                                "MTextures/BuildingBig",
                                "Mood",
                                position + tallBuildingOffset, rotation, row, column);
                        case 2:
                            MSDistrictHall.instantiate("MModels/DistrictHall",
                                "MTextures/DistrictHall",
                                "Mood",
                                position + hallBuildingOffset, rotation, row, column);
                            return (MSDistrictHall.getInstance());
                        case 3:
                            return new MSVolunteerCenter("MModels/BuildingBig",
                                "MTextures/BuildingVolunteer",
                                "Mood",
                                position + tallBuildingOffset, rotation , row, column);
                    }
                    break;
                case 1:
                    string textureName = "";
                    switch((tileKey / 10) - ((tileKey / 100) * 10))
                    {
                        case 0:
                            textureName = "MTextures/1";
                            break;
                        case 1:
                            textureName = "MTextures/11";
                            break;
                        case 2:
                            switch((tileKey / 100) - ((tileKey / 1000) * 10))
                            {
                                case 0:
                                    textureName = "MTextures/21";
                                    break;
                                case 1:
                                    textureName = "MTextures/121";
                                    break;
                            }
                            break;
                        case 3:
                            textureName = "MTextures/31";
                            break;
                        case 4:
                            textureName = "MTextures/41";
                            break;
                    }
                    return new MSRoad("MModels/Floor",
                        textureName,
                        "Mood",
                        position, rotation, row, column); 
            }
            return null;
        }
    }
}
