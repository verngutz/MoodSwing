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
        private List<MS3DComponent> mapList;
        private const int tileDimension = 32;
        private int rows;
        private int columns;
        private MSPerson person;
        public MSMap(String filename) : base( MoodSwing.getInstance() )
        {
            StreamReader sr = new StreamReader(filename);
            string[] line = sr.ReadLine().Split(' ');
            rows = Int32.Parse(line[0]);
            columns = Int32.Parse(line[1]);
            mapArray = new MSTile[rows,columns];
            mapList = new List<MS3DComponent>();
            for(int i = 0; i < rows; i++)
            {
                line = sr.ReadLine().Split(' ');
                for(int j = 0; j < columns; j++)
                {
                    mapArray[i, j] = MSTileFactory.createMSTile(Int32.Parse(line[j]), new Vector3(j * tileDimension, i * tileDimension, 0));
                    if (mapArray[i, j] is MS3DComponent)
                        mapList.Add(mapArray[i, j] as MS3DComponent);
                }
            }

            Node temp = this.getPath(mapArray, new Vector2(2, 2), new Vector2(12, 6), rows, columns);
            person = new MSPerson( MoodSwing.getInstance().Content.Load<Model>("person"), (mapArray[2,2] as MS3DTile).Position + new Vector3(0,0,10), temp );
            mapList.Add(person);
        }

        public override void Update(GameTime gameTime)
        {
            person.walk(mapArray);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            mapList.Sort();
            foreach( MS3DComponent tile in mapList ) 
            {
                tile.Draw(gameTime);
                
            }
        }

        public void checkCollision()
        {
            System.Console.WriteLine("CHECKING...");
            float? minDistance = null;
            MS3DTile tile = null;

            foreach ( MSTile t in mapArray )
            {
                if ( t is MS3DTile)
                {
                    MS3DTile tempTile = t as MS3DTile;
                    BoundingBox b = new BoundingBox(tempTile.Position, tempTile.Position + new Vector3(tileDimension, tileDimension, tileDimension));
                    float? dist = Intersects(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), tempTile.TileModel,
                        tempTile.WorldMatrix, MSCamera.getInstance().getView(),
                        tempTile.ProjectionMatrix, MoodSwing.getInstance().GraphicsDevice.Viewport, b);
                    if (dist != null)
                    {
                        if (minDistance == null || minDistance > dist)
                        {
                            minDistance = dist;
                            tile = tempTile;
                        }
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
            return IntersectDistance(b, mouseLocation, view, projection, viewport);
        }

        public Node getPath(MSTile[,] mapArray, Vector2 start, Vector2 end, int row, int col)
        {
            List<Node> toCheck = new List<Node>();
            List<Node> done = new List<Node>();

            bool [,] hasVis = new  bool[row,col];

            toCheck.Add(new Node((int)start.X, (int)start.Y, 1, 0, null) );
            hasVis[(int)start.X, (int)start.Y] = true;
            Node last = null;
            while (toCheck.Count != 0)
            {
                toCheck.Sort();
                Node visiting = toCheck.ElementAt<Node>(0);
                toCheck.RemoveAt(0);
                done.Add(visiting);

                if (visiting.Position == end)
                {
                    last = visiting;
                    break;
                }
                else
                {
                    int x = (int)visiting.Position.X;
                    int y = (int)visiting.Position.Y;

                    if (y + 1 < columns && mapArray[x, y + 1] is MSRoad) 
                    {
                        if (hasVis[x, y + 1] == false)
                        {
                            hasVis[x, y + 1] = true;
                            toCheck.Add(new Node(x, y + 1, visiting.G + 1, (int)Math.Abs(x - end.X) + (int)Math.Abs(y + 1 - end.Y), visiting));
                        }
                        else
                        {
                            foreach (Node temp in toCheck)
                            {
                                if (temp.Position == new Vector2(x, y + 1))
                                {
                                    temp.G = visiting.G + 1;
                                    temp.parent = visiting;
                                }
                            }
                        }
                    }
                    if ( y-1 >= 0 && mapArray[x, y - 1] is MSRoad) 
                    {
                        if (hasVis[x, y - 1] == false)
                        {
                            hasVis[x, y - 1] = true;
                            toCheck.Add(new Node(x, y - 1, visiting.G + 1, (int)Math.Abs(x - end.X) + (int)Math.Abs(y - 1 - end.Y), visiting));
                        }
                        else
                        {
                            foreach (Node temp in toCheck)
                            {
                                if (temp.Position == new Vector2(x, y - 1))
                                {
                                    temp.G = visiting.G + 1;
                                    temp.parent = visiting;
                                }
                            }
                        }
                    }
                    if (x + 1 < rows && mapArray[x + 1, y] is MSRoad) 
                    {
                        if (hasVis[x + 1, y] == false)
                        {
                            hasVis[x + 1, y] = true;
                            toCheck.Add(new Node(x + 1, y, visiting.G + 1, (int)Math.Abs(x + 1 - end.X) + (int)Math.Abs(y - end.Y), visiting));
                        }
                        else
                        {
                            foreach (Node temp in toCheck)
                            {
                                if (temp.Position == new Vector2(x + 1, y))
                                {
                                    temp.G = visiting.G + 1;
                                    temp.parent = visiting;
                                }
                            }
                        }
                    }
                    if ( x-1 >= 0 && mapArray[x - 1, y] is MSRoad  ) 
                    {
                        if (hasVis[x - 1, y] == false)
                        {
                            hasVis[x - 1, y] = true;
                            toCheck.Add(new Node(x - 1, y, visiting.G + 1, (int)Math.Abs(x - 1 - end.X) + (int)Math.Abs(y - end.Y), visiting));
                        }
                        else
                        {
                            foreach (Node temp in toCheck)
                            {
                                if (temp.Position == new Vector2(x - 1, y))
                                {
                                    temp.G = visiting.G + 1;
                                    temp.parent = visiting;
                                }
                            }
                        }
                    }
                }
            }

            while (last.parent != null)
            {
                Node par = last.parent;
                par.next = last;
                last = par;
            }

            return last;
        }


    }

    public class Node : IComparable
    {
        int x;
        int y;
        public Node next;
        public Node parent;
        int g;
        int h;

        public Vector2 Position { get { return new Vector2(x, y); } }
        public int G { get { return g; } set { if (value < g) g = value; } }
        public int F { get { return g + h; } }

        public Node(int a, int b, int value, int est, Node par )
        {
            x = a;
            y = b;
            g = value;
            h = est;
            parent = par;
        }

        public int CompareTo(object ob)
        {
            Node n = ob as Node;
            return F - n.F;
        }
    }
}
