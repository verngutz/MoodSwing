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
    public class MSUnbuyableBuilding : MSBuilding
    {
        public MSUnbuyableBuilding(Model model, Texture2D texture, Effect effect, Vector3 position, int row, int column)
            : base(model, texture, effect, position, row, column) { }
    }
}
