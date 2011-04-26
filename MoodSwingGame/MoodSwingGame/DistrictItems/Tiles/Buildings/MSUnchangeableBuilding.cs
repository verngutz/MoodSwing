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

namespace MoodSwingGame
{
    public class MSUnchangeableBuilding : MSBuilding
    {
        public MSUnchangeableBuilding(String model, String texture, String effect, Vector3 position, float rotation, int row, int column)
            : base(model, texture, effect, position, rotation, row, column, MSMap.shortheight) { }

        public override string toString()
        {
            return "MSUnchangeableBuilding\n" + base.toString();
        }
    }
}
