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
    public abstract class MSUnit : MS3DSingleModelComponent
    {
        public static Vector3 UNITZ_POSITION = new Vector3(0, 0, MSMap.floorheight / 2 - 4);


        public float Rotation { get; set; }
        public bool IsStopped { get; set; }
        protected bool isMobbable;
        public bool IsMobbable { get { return isMobbable; } }

        protected MSMap map;
        public MSMap Map { get { return map; } }

        protected Node path;
        public Node Path { get { return path; } set { path = value; destination = Vector2.Zero; destinationReached = false; } }

        protected Vector2 destination;
        public Vector2 Destination { get { return destination; } }

        protected bool destinationReached;
        public virtual bool DestinationReached { get { return destinationReached; } }

        public abstract float Speed { get; set; }

        public Vector2 TileCoordinate { get { return new Vector2((int)(Math.Round(position.Y / MSMap.tileDimension)), (int)(Math.Round((position.X / MSMap.tileDimension)))); } }


        protected float targetRotation;
        protected float rotationSpeed;
        public MSUnit(Vector3 position, Node path, MSMap map, bool mobbable, float initRotation)
            : base(position)
        {
            this.path = path;
            this.map = map;
            this.isMobbable = mobbable;
            Vector2 coord = Vector2.Zero;
            targetRotation = 0f;
            rotationSpeed = 0.1f;
            Rotation = initRotation;
            
        }

        public override void Draw(GameTime gameTime)
        {
            /**
             * Temporary Solution for the error:
             * "The current vertex declaration does not include all the elements required by the current vertex shader. 
             * TextureCoordinate0 is missing."
             */            
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = Matrix.CreateRotationZ(Rotation) * this.WorldMatrix;
                        effect.View = MSCamera.GetInstance().GetView();
                        effect.Projection = MSCamera.GetInstance().ProjectionMatrix;

                    }
                }
                mesh.Draw();
            }

            /**
             * End Temporary Solution
             */
        }

        public virtual void Walk(MS3DTile[,] mapArray, List<MSUnit> units )
        {
            if (!IsStopped)
            {
                Vector2 pos = new Vector2(Position.X, Position.Y);

                if (destination == Vector2.Zero)
                {
                    Vector3 targetVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
                    if (path.next != null || path.parent != null)
                    {
                        destination = new Vector2(targetVector3.X + MSRandom.random.Next(MSMap.tileDimension / 2),
                                                      targetVector3.Y + MSRandom.random.Next(MSMap.tileDimension / 2));
                    }
                    else
                        destination = new Vector2(targetVector3.X, targetVector3.Y);

                    targetRotation = (float)Math.Atan2(destination.Y, destination.X);
                }

                //destination reached
                if (Vector2.Distance(pos, destination) < 1)
                {
                    this.position = new Vector3(destination.X, destination.Y, position.Z);
                    if (path.next != null)
                    {
                        path = path.next;
                        Vector3 targetVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
                        destination = new Vector2(targetVector3.X + MSRandom.random.Next(MSMap.tileDimension / 2) - MSMap.tileDimension / 4,
                                                      targetVector3.Y + MSRandom.random.Next(MSMap.tileDimension / 2) - MSMap.tileDimension / 4);

                        Vector2 direction = destination - new Vector2(position.X, position.Y);
                        float angle = (float)Math.Atan2(direction.Y, direction.X);
                        targetRotation = angle;
                    }
                    else destinationReached = true;
                }
                else
                {
                    Vector2 unit = destination - pos;
                    unit = Vector2.Normalize(unit);
                    this.position += new Vector3(unit.X * Speed, unit.Y * Speed, 0);
                }


                if (Math.Abs(targetRotation - Rotation) > 0.01)
                {
                    float delta = targetRotation - Rotation;
                    while (delta < -MathHelper.Pi)
                    {
                        delta += MathHelper.TwoPi;
                    }
                    while (delta > MathHelper.Pi)
                    {
                        delta -= MathHelper.TwoPi;
                    }
                    Rotation += delta * rotationSpeed;
                }

                adjustWorldMatrix();

            }
        }

        public virtual void Follow(MSUnit unit)
        {
            path = unit.Path;
            destination = Vector2.Zero;
            //targetRotation = (float)Math.Atan2(destination.Y - position.Y, destination.X - position.X);
        }

        public void ChangePath(Node path)
        {
            this.path = path;
            this.destination = Vector2.Zero;
        }

        public override String toString()
        {
            return "";
        }

        

    }
}
