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
        public static void instantiate(Vector2 pos, Vector2 size, Texture2D t)
        {
            districtHall = new MSDistrictHall(pos, size, MoodSwing.getInstance().SpriteBatch, t );
        }
        private MSDistrictHall( Vector2 position, Vector2 size, SpriteBatch spriteBatch, Texture2D t ) 
            : base( position, size, spriteBatch, t )
        {
                
        }
    }
}
