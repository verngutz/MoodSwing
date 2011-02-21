﻿using System;
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
        private Texture2D texture;
        private Effect effect;
        private Vector3 lightSource;

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

        public MSCitizen(Model m, Texture2D texture, Effect effect, Vector3 position, Node p, State s)
            : base(position, MoodSwing.getInstance())
        {
            this.model = m;
            this.texture = texture;
            this.effect = effect;
            this.isThere = false;
            //this.isMobbing = mob;
            //this.isWaiting = false;
            state = s;
            this.path = p;
            this.moodManager = MSMoodManager.GetInstance();
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
        public virtual void Walk( MS3DTile[,] mapArray )
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
                    moodManager.takeDamage(0.01f);

                adjustWorldMatrix();
            }

        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = effect;
                    effect.Parameters["World"].SetValue(world);
                    effect.Parameters["View"].SetValue(MSCamera.GetInstance().GetView());
                    effect.Parameters["Projection"].SetValue(projection);
                    Matrix worldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * world));
                    effect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTransposeMatrix);
                    //effect.Parameters["ViewVector"].SetValue(MSCamera.GetInstance().NormalizedViewVector);
                    effect.Parameters["DiffuseLightDirection"].SetValue(MSCamera.UNIVERSAL_LIGHT_SOURCE - Position);
                    effect.Parameters["Saturation"].SetValue(MSMoodManager.GetInstance().Mood);
                    effect.Parameters["ModelTexture"].SetValue(texture);

                }
                mesh.Draw();
            }
            base.Draw(gameTime);
        }

        public void changeModel(String modelName, String textureName)
        {
            model = MoodSwing.getInstance().Content.Load<Model>(modelName);
            if (textureName == null)
                texture = null;
            else
                texture = MoodSwing.getInstance().Content.Load<Texture2D>(textureName);
        }
    }
}
