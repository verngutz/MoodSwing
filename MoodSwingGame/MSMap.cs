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
        private MSTile[] mapList;
        private const int tileDimension = 32;
        private int rows;
        private int columns;
        public MSMap(String filename) : base( MoodSwing.getInstance() )
        {
            StreamReader sr = new StreamReader(filename);
            string[] line = sr.ReadLine().Split(' ');
            rows = Int32.Parse(line[0]);
            columns = Int32.Parse(line[1]);
            mapArray = new MSTile[rows,columns];
            mapList = new MSTile[rows * columns];
            for(int i = 0; i < rows; i++)
            {
                line = sr.ReadLine().Split(' ');
                for(int j = 0; j < columns; j++)
                {
                    mapArray[i, j] = MSTileFactory.createMSTile(Int32.Parse(line[j]), new Vector3(j * tileDimension, i * tileDimension, 0));
                    mapList[i * columns + j] = mapArray[i, j];
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Array.Sort(mapList);
            foreach( MSTile tile in mapList ) 
            {
                tile.Draw(gameTime);
                
            }
        }

        public void checkCollision()
        {
            System.Console.WriteLine("CHECKING...");
            float? minDistance = null;
            MSTile tile = null;

            for( int i = 0; i < rows*columns; i++ )
            {
                BoundingBox b = new BoundingBox( mapList[i].Position, mapList[i].Position+new Vector3(tileDimension, tileDimension, tileDimension));
                float? dist = Intersects(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), mapList[i].TileModel,
                    mapList[i].WorldMatrix, MSCamera.getInstance().getView(),
                    mapList[i].ProjectionMatrix, MoodSwing.getInstance().GraphicsDevice.Viewport, b);
                if (dist != null)
                {
                    if (minDistance == null || minDistance > dist)
                    {
                        minDistance = dist;
                        tile = mapList[i];
                    }
                }
                
            }

            if (tile != null) 
            {
                System.Console.WriteLine("found: " + tile.Position.Y / tileDimension + " " + tile.Position.X / tileDimension);
            }
                
         
        }
        public Ray CalculateRay(Vector2 mouseLocation, Matrix view, Matrix projection, Viewport viewport)
        {
            Vector3 nearPoint = viewport.Unproject(new Vector3(mouseLocation.X,
                    mouseLocation.Y, 5f),
                    projection,
                    view,
                    Matrix.Identity);
            
            Vector3 farPoint = viewport.Unproject(new Vector3(mouseLocation.X,
                    mouseLocation.Y, 5000.0f),
                    projection,
                    view,
                    Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }

        public float? IntersectDistance(BoundingBox sphere, Vector2 mouseLocation,
            Matrix view, Matrix projection, Viewport viewport)
        {
            Ray mouseRay = CalculateRay(mouseLocation, view, projection, viewport);
            return mouseRay.Intersects(sphere);
            
        }

        public float? Intersects(Vector2 mouseLocation,
            Model model, Matrix world,
            Matrix view, Matrix projection,
            Viewport viewport, BoundingBox b)
        {
            /*float? minDist = null;
            for (int index = 0; index < model.Meshes.Count; index++)
            {
                BoundingSphere sphere = model.Meshes[index].BoundingSphere;
                sphere = sphere.Transform(world);
                float? distance = IntersectDistance(b, mouseLocation, view, projection, viewport);

                return distance;
            }

            return minDist; ;*/
            return IntersectDistance(b, mouseLocation, view, projection, viewport);
        }
    }
}
