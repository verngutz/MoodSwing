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
        public static int floorheight = 31;
        public static int shortheight = 47;
        public static int tallheight = 54;

        public Vector3 LightSource { set; get; }

        private List<MSUnbuyableBuilding> citizenSources;

        private int rows;
        private int columns;
        private int initialVolunteerCenters;
        public int InitialVolunteerCenters { get { return (initialVolunteerCenters); } }

        public Vector2 Dimension { get { return (new Vector2(rows, columns) * tileDimension); } }
        public MSMap(String filename) : base( MoodSwing.GetInstance() )
        {
            StreamReader sr = new StreamReader(filename);
            string[] line = sr.ReadLine().Split(' ');
            rows = Int32.Parse(line[0]);
            columns = Int32.Parse(line[1]);
            initialVolunteerCenters = 0;
            mapArray = new MS3DTile[rows,columns];
            citizenSources = new List<MSUnbuyableBuilding>();
            for(int j = 0; j < columns; j++)
            {
                line = sr.ReadLine().Split(' ');
                for (int i = 0; i < rows; i++)
                {
                    MS3DTile toAdd = MSTileFactory.CreateMSTile(Int32.Parse(line[i]), new Vector3(j * tileDimension, i * tileDimension, 0), i, j);
                    mapArray[i, j] = toAdd;
                    if (toAdd is MSUnbuyableBuilding)
                        citizenSources.Add(toAdd as MSUnbuyableBuilding);
                    if (toAdd is MSVolunteerCenter)
                        initialVolunteerCenters++;
                } 
            }
            LightSource = new Vector3(tileDimension * rows << 1, tileDimension * columns << 1, 10000);
        }

        public MSUnbuyableBuilding GetRandomCitizenSource()
        {
            return citizenSources.ElementAt<MSUnbuyableBuilding>(MSRandom.random.Next(citizenSources.Count));
        }

        /// <summary>
        /// Gets the nearest volunteer center with respect to a specific tile.
        /// </summary>
        /// <param name="reference">Tile used as a reference point to find the nearest volunteer center</param>
        /// <returns>The nearest volunteer center. Return null if there is no volunteer center</returns>
        public MSVolunteerCenter GetNearestVolunteerCenter(MS3DTile reference)
        {
            float? minDist = null;
            MSVolunteerCenter center = null;
            foreach (MS3DTile tile in MapArray)
            {
                if (tile is MSVolunteerCenter)
                {
                    MSVolunteerCenter vc = tile as MSVolunteerCenter;
                    float distance = Vector3.Distance(vc.Position, reference.Position);
                    if (minDist == null || minDist > distance)
                    {
                        minDist = distance;
                        center = vc;
                    }
                }
            }
            return center;
        }

        public MSVolunteerCenter GetNearestVolunteerCenter(Vector2 coord)
        {
            return GetNearestVolunteerCenter(mapArray[(int)coord.X, (int)coord.Y]);
        }
        /// <summary>
        /// Picking Algo. Looks for the closest object that intersects the mouse ray
        /// </summary>
        /// <returns>The tile which the mouse intersects. Null if it doesnt intersect anything</returns>
        public MS3DTile CheckCollision()
        {
            float? minDistance = null;
            MS3DTile tile = null;
            foreach ( MS3DTile t in mapArray )
            {
                if (true/*!(t is MSRoad)*/)
                {
                    float? dist = Intersects(t.BoundingBox, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), MSCamera.GetInstance().GetView(),
                    t.ProjectionMatrix, MoodSwing.GetInstance().GraphicsDevice.Viewport);

                    if (dist != null)
                    {
                        if (minDistance == null || minDistance > dist)
                        {
                            minDistance = dist;
                            tile = t;
                        }
                    }  
                }
            }
            return tile;
        }

        /// <summary>
        /// Calculates the ray formed from the mouse to the world space.
        /// </summary>
        /// <param name="mouseLocation">Vector2D corresponding to the coordinate of the mouse on the screen.</param>
        /// <param name="view"> View matrix used</param>
        /// <param name="projection"> Projection matrix used</param>
        /// <param name="viewport"> Viewport of the game</param>
        /// <returns>The Ray pointing from the mouse to the world space</returns>
        public Ray CalculateRay(Vector2 mouseLocation, Matrix view, Matrix projection, Viewport viewport)
        {
            Vector3 nearPoint = viewport.Unproject(new Vector3(mouseLocation.X,
                    mouseLocation.Y, 5),
                    projection,
                    view,
                    Matrix.Identity);
            Vector3 farPoint = viewport.Unproject(new Vector3(mouseLocation.X,
                    mouseLocation.Y, 5000),
                    projection,
                    view,
                    Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction = Vector3.Normalize(direction);

            return new Ray(nearPoint, direction);
        }
        
        /// <summary>
        /// Checks whether the mouse intersects the bounding box of an object.
        /// </summary>
        /// <param name="box">Bounding box of the object to check for intersection.</param>
        /// <param name="mouseLocation">Vector2D corresponding to the coordinate of the mouse on the screen.</param>
        /// <param name="view"> View matrix used</param>
        /// <param name="projection"> Projection matrix used</param>
        /// <param name="viewport"> Viewport of the game</param>
        /// <returns>The distance at which the mouse intersects the bounding box of the object. Null if it doesnt intersect</returns>
       
        public float? Intersects(BoundingBox box, Vector2 mouseLocation,
            Matrix view, Matrix projection, Viewport viewport)
        {
            Ray mouseRay = CalculateRay(mouseLocation, view, projection, viewport);
            return mouseRay.Intersects(box);
            
        }



        /// <summary>
        /// Gets the head of the linked-list representing the shortest path
        /// from start to end in the map coordinate system.
        /// </summary>
        /// <param name="start">The start tile coordinate.</param>
        /// <param name="end">The end tile coordinate.</param>
        /// <returns> The head of the linked-list of nodes. </returns>
        public Node GetPath(Vector2 start, Vector2 end)
        {
            List<Node> toCheck = new List<Node>();
            List<Node> done = new List<Node>();

            bool [,] hasVis = new  bool[rows,columns];

            toCheck.Add(new Node((int)start.X, (int)start.Y, 1, 0, null) );
            hasVis[(int)start.X, (int)start.Y] = true;
            Node last = null;
            bool getRoadFirst = true;
            if (mapArray[(int)start.X, (int)start.Y] is MSRoad) getRoadFirst = false;
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
                    
                    if (y + 1 < columns && mapArray[x, y + 1] is MSRoad || (!getRoadFirst && new Vector2(x,y+1) == end) ) 
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

                    if (y - 1 >= 0 && mapArray[x, y - 1] is MSRoad || ( !getRoadFirst && new Vector2(x, y - 1) == end) ) 
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
                    if (x + 1 < rows && mapArray[x + 1, y] is MSRoad || ( !getRoadFirst && new Vector2(x + 1, y) == end) ) 
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
                    if (x - 1 >= 0 && mapArray[x - 1, y] is MSRoad || (!getRoadFirst && new Vector2(x - 1, y) == end) )
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
                    if (getRoadFirst == true) getRoadFirst = false;
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            List<Vector2> toTransform = new List<Vector2>();
            foreach (MS3DTile tile in mapArray)
            {
                tile.Update(gameTime);
                if (tile is MSBuyableBuilding && (tile as MSBuyableBuilding).State == MSBuyableBuilding.BuyableBuildingState.DONE)
                {
                    toTransform.Add(tile.TileCoordinate);
                }
            }
            foreach (Vector2 coord in toTransform)
            {
                if( (mapArray[(int)coord.X, (int)coord.Y] as MSBuyableBuilding).FutureSelf is MSVolunteerCenter)
                    MSResourceManager.GetInstance().VolunteerCapacity += MSResourceManager.VOLUNTEER_CENTER_GAIN;
                mapArray[(int)coord.X, (int)coord.Y] = 
                    (mapArray[(int)coord.X, (int)coord.Y] as MSBuyableBuilding).FutureSelf;
                
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (MS3DTile tile in mapArray)
            {
                tile.Draw(gameTime);
            }
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
