using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MSCitizen : MS3DComponent, MSUnit
    {
        public enum State
        {
            CIVILIAN = 0,
            MOB,
            WAITING,
            SUPPRESSED

        };

        public State state;
        private Model model;
        private Node path;
        public Node Path { get { return path; } set { path = value; targetLocation = Vector2.Zero; isThere = false; } }
        private bool isThere;
        public virtual bool IsThere() { return isThere; }
        private Vector2 targetLocation;
        public Vector2 TargetLocation { get { return targetLocation; } }
        //private bool isMobbing;
        //public bool IsMobbing { get { return isMobbing; }  }
        private bool isWaiting;
        public bool IsWaiting { get { return isWaiting; } set { isWaiting = value; } }
        public Vector2 TileCoordinate { get { return new Vector2((int)(Math.Round(position.Y / MSMap.tileDimension)),(int) (Math.Round((position.X / MSMap.tileDimension))) ); } }
        private MSMoodManager moodManager;

        public MSCitizen(Model m, Vector3 position, Node p, State s)
            : base(position, MoodSwing.getInstance())
        {
            this.model = m;
            this.isThere = false;
            //this.isMobbing = mob;
            //this.isWaiting = false;
            state = s;
            this.path = p;
            this.moodManager = MSMoodManager.getInstance();
        }

        public void Follow(MSCitizen citizen)
        {
            //IsWaiting = false;
            path = citizen.Path;
            targetLocation = citizen.TargetLocation;
            //this.model = MoodSwing.getInstance().Content.Load<Model>("mob");
            //state = State.MOB
        }

        private const float WALK_SPEED = 0.55f;
        public virtual void Walk( MSTile[,] mapArray )
        {
            if ( state != State.WAITING)
            {
                Vector2 pos = new Vector2(Position.X, Position.Y);

                if (targetLocation == Vector2.Zero)
                {
                    Vector3 targetVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
                    if (path.next != null || path.parent != null)
                    {
                        targetLocation = new Vector2(targetVector3.X + MSRandom.random.Next(MSMap.tileDimension / 2),
                                                      targetVector3.Y + MSRandom.random.Next(MSMap.tileDimension / 2));
                    }
                    else
                        targetLocation = new Vector2(targetVector3.X, targetVector3.Y);

                }
                Vector2 unit = targetLocation - pos;
                unit = Vector2.Normalize(unit);

                if (Vector2.Distance(pos, targetLocation) < 1)
                {
                    this.position = new Vector3(targetLocation.X, targetLocation.Y, position.Z);
                    if (path.next != null)
                    {
                        path = path.next;
                        Vector3 targetVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
                        targetLocation = new Vector2(targetVector3.X + MSRandom.random.Next(MSMap.tileDimension / 2) - MSMap.tileDimension / 4,
                                                      targetVector3.Y + MSRandom.random.Next(MSMap.tileDimension / 2) - MSMap.tileDimension / 4);
                    }
                    else isThere = true;
                }
                else
                    this.position += new Vector3(unit.X * WALK_SPEED, unit.Y * WALK_SPEED, 0);

                if (isThere && state == State.MOB)
                    moodManager.takeDamage(1);

                adjustWorldMatrix();
            }

        }

        public MS3DComponent Get3DComponent()
        {
            return this;
        }
        public override void Draw(GameTime gameTime)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = MSCamera.getInstance().getView();
                    effect.Projection = projection;
                    effect.TextureEnabled = true;
                }
                mesh.Draw();
            }
            base.Draw(gameTime);
        }

        public void changeModel(String name)
        {
            model = MoodSwing.getInstance().Content.Load<Model>(name);
        }
    }
}
