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

using System.IO;

namespace MoodSwingGame
{
    public class MSMap : DrawableGameComponent
    {
        private MSTile[,] mapArray;
        private const int tileDimension = 32;
        public MSMap(String filename) : base( MoodSwing.getInstance() )
        {
            StreamReader sr = new StreamReader(filename);
            string[] line = sr.ReadLine().Split(' ');
            int rows = Int32.Parse(line[0]);
            int columns = Int32.Parse(line[1]);
            mapArray = new MSTile[rows,columns];
            for(int i = 0; i < rows; i++)
            {
                line = sr.ReadLine().Split(' ');
                for(int j = 0; j < columns; j++)
                {
                    mapArray[i, j] = MSTileFactory.createMSTile(Int32.Parse(line[j]), new Vector3(j * tileDimension, i * tileDimension, 0));
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach( MSTile tile in mapArray ) 
            {
                tile.Draw(gameTime);
                break;
            }
        }
    }
}
