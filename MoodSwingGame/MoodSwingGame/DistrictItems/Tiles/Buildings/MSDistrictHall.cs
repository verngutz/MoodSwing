using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MSDistrictHall : MSBuilding
    {
        private static MSDistrictHall districtHall;
        public static MSDistrictHall getInstance()
        {
            return districtHall;
        }
        public static void instantiate(String model, String texture, String effect, Vector3 position, float rotation, int row, int column)
        {
            districtHall = new MSDistrictHall(model, texture, effect, position, rotation, row, column);
        }

        private MSDistrictHall(String model, String texture, String effect, Vector3 position, float rotation, int row, int column)
            : base(model, texture, effect, position, rotation, row, column, MSMap.tallheight)

        {
                
        }
    }
}
