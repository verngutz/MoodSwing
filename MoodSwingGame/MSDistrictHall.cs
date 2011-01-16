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
        public static void instantiate(Model model, Texture2D wrapper, Vector3 position)
        {
            districtHall = new MSDistrictHall(model, wrapper, position);
        }



        private MSDistrictHall( Model model, Texture2D wrapper, Vector3 position ) 
            : base( model, wrapper, position )

        {
                
        }
    }
}
