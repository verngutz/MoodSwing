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
        private MS3DTile[,] mapArray;
        public MS3DTile[,] MapArray { get { return mapArray; } }
        public static int tileDimension = 31;

        public Vector3 LightSource { set; get; }

        private int rows;
        private int columns;
        public MSMap(String filename) : base( MoodSwing.getInstance() )
        {
            StreamReader sr = new StreamReader(filename);
            string[] line = sr.ReadLine().Split(' ');
            rows = Int32.Parse(line[0]);
            columns = Int32.Parse(line[1]);
            mapArray = new MS3DTile[rows,columns];
            for(int j = 0; j < columns; j++)
            {
                line = sr.ReadLine().Split(' ');
                for (int i = 0; i < rows; i++)
                {
                    mapArray[i, j] = MSTileFactory.CreateMSTile(Int32.Parse(line[i]), new Vector3(j * tileDimension, i * tileDimension, 0));
                } 
            }
            LightSource = new Vector3(tileDimension * rows << 1, tileDimension * columns << 1, 10000);
        }

        public Vector2 GetRandomBuilding()
        {
            int x = 0;
            int y = 0;
            do
            {
                x = MSRandom.random.Next(rows);
                y = MSRandom.random.Next(columns);

            } while (!(mapArray[x, y] is MSBuilding) || (mapArray[x,y] is MSTower) );
            return new Vector2(x,y);
        }

        public void Change(MS3DTile tile, MS3DTile newTile)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (mapArray[i, j] == tile)
                    {
                        mapArray[i, j] = newTile;
                    }
                }
            }
        }

        //note: This needs revision when the 'dummy' tiles have been implemented.
        public MS3DTile CheckCollision()
        {
            System.Console.WriteLine("CHECKING...");
            float? minDistance = null;
            MS3DTile tile = null;

            foreach ( MS3DTile t in mapArray )
            {
                float? dist = Intersects(t.BoundingSphere, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), MSCamera.GetInstance().GetView(),
                    t.ProjectionMatrix, MoodSwing.getInstance().GraphicsDevice.Viewport);

                if (dist != null)
                {
                    if (minDistance == null || minDistance > dist)
                    {
                        minDistance = dist;
                        tile = t;
                    }
                }   
            }
            if(tile!=null) System.Console.WriteLine(tile.Position / new Vector3(tileDimension, tileDimension, tileDimension));
            return tile;
         
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

        public float? Intersects(BoundingSphere sphere, Vector2 mouseLocation,
            Matrix view, Matrix projection, Viewport viewport)
        {
            Ray mouseRay = CalculateRay(mouseLocation, view, projection, viewport);
            return mouseRay.Intersects(sphere);
            
        }

        /// <summary>
        /// Gets the head of the linked-list representing the shortest path
        /// from start to end in the map coordinate system.
        /// </summary>
        /// <param name="start">The start tile coordinate.</param>
        /// <param name="end">The end tile coordinate.</param>
        
        public Node GetPath(Vector2 start, Vector2 end)
        {
            List<Node> toCheck = new List<Node>();
            List<Node> done = new List<Node>();

            bool [,] hasVis = new  bool[rows,columns];

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
                    if (y + 1 < columns && mapArray[x, y + 1] is MSRoad || new Vector2(x,y+1) == end ) 
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

                    if (y - 1 >= 0 && mapArray[x, y - 1] is MSRoad || new Vector2(x, y - 1) == end) 
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
                    if (x + 1 < rows && mapArray[x + 1, y] is MSRoad || new Vector2(x + 1, y) == end) 
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
                    if (x - 1 >= 0 && mapArray[x - 1, y] is MSRoad || new Vector2(x - 1, y) == end) 
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
