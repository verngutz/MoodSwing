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
    public class MSAbandonedBuilding : MSChangeableBuilding
    {
        public MSAbandonedBuilding(String model, String texture, String effect, Vector3 position, float rotation, int row, int column)
            : base(model, texture, effect, position, rotation, row, column, MSMap.tallheight) { }

        public override string toString()
        {
            return "MSAbandonedBuilding\n" + base.toString();
        }

        public override void load(System.IO.StreamReader sr)
        {
            System.Console.WriteLine("LOADING AB");
            base.load(sr);
        }
    }
}
