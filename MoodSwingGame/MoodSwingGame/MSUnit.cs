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
    public abstract class MSUnit : MS3DComponent
    {
        public static Vector3 UNITZ_POSITION = new Vector3(0, 0, MSMap.floorheight / 2 - 4);
        protected abstract Model Model { get; }
        protected abstract Effect Effect { get; }
        protected abstract Texture2D Texture { get; }

        public bool IsStopped { get; set; }
        protected bool isMobbable;
        public bool IsMobbable { get { return isMobbable; } }

        protected MSMap map;
        public MSMap Map { get { return map; } }

        private Node path;
        public Node Path { get { return path; } set { path = value; destination = Vector2.Zero; destinationReached = false; } }

        private Vector2 destination;
        public Vector2 Destination { get { return destination; } }

        private bool destinationReached;
        public virtual bool DestinationReached { get { return destinationReached; } }

        protected abstract float Speed { get; }

        public Vector2 TileCoordinate { get { return new Vector2((int)(Math.Round(position.Y / MSMap.tileDimension)), (int)(Math.Round((position.X / MSMap.tileDimension)))); } }

        public MSUnit(Vector3 position, Node path, MSMap map, bool mobbable) 
            : base(position, MoodSwing.GetInstance()) 
        {
            this.path = path;
            this.map = map;
            this.isMobbable = mobbable;

        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    /*part.Effect = Effect;
                    Effect.Parameters["World"].SetValue(world);
                    Effect.Parameters["View"].SetValue(MSCamera.GetInstance().GetView());
                    Effect.Parameters["Projection"].SetValue(MSCamera.GetInstance().ProjectionMatrix);
                    Matrix worldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * world));
                    Effect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTransposeMatrix);
                    //Effect.Parameters["ViewVector"].SetValue(MSCamera.GetInstance().NormalizedViewVector);
                    Effect.Parameters["DiffuseLightDirection"].SetValue(map.LightSource - Position);
                    Effect.Parameters["Saturation"].SetValue(MSMoodManager.GetInstance().Mood);
                    Effect.Parameters["ModelTexture"].SetValue(Texture);*/

                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = this.WorldMatrix;
                        effect.View = MSCamera.GetInstance().GetView();
                        effect.Projection = MSCamera.GetInstance().ProjectionMatrix;

                    }
                }
                mesh.Draw();
            }
            base.Draw(gameTime);
        }

        public virtual void Walk(MS3DTile[,] mapArray)
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

                }

                if (Vector2.Distance(pos, destination) < 1)
                {
                    this.position = new Vector3(destination.X, destination.Y, position.Z);
                    if (path.next != null)
                    {
                        path = path.next;
                        Vector3 targetVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
                        destination = new Vector2(targetVector3.X + MSRandom.random.Next(MSMap.tileDimension / 2) - MSMap.tileDimension / 4,
                                                      targetVector3.Y + MSRandom.random.Next(MSMap.tileDimension / 2) - MSMap.tileDimension / 4);
                    }
                    else destinationReached = true;
                }
                else
                {
                    Vector2 unit = destination - pos;
                    unit = Vector2.Normalize(unit);
                    this.position += new Vector3(unit.X * Speed, unit.Y * Speed, 0);
                }

                adjustWorldMatrix();
            }
        }

        public void Follow(MSUnit unit)
        {
            path = unit.Path;
            destination = unit.Destination;
        }

        public void ChangePath(Node path)
        {
            this.path = path;
            this.destination = Vector2.Zero;
        }
    }
}
