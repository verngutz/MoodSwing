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
        public enum CitizenState
        {
            CIVILIAN = 0,
            MOB,
            WAITING,
            SUPPRESSED
        };

        public CitizenState state;
        public MSTypes MDG { get; set; }

        private Model model;
        private Texture2D texture;
        private Effect effect;
        public Vector3 LightSource { set; get; }

        private Node path;
        public Node Path { get { return path; } set { path = value; targetLocation = Vector2.Zero; isThere = false; } }
        private bool isThere;
        public virtual bool IsThere() { return isThere; }
        private Vector2 targetLocation;
        public Vector2 TargetLocation { get { return targetLocation; } }
        private bool isWaiting;
        public bool IsWaiting { get { return isWaiting; } set { isWaiting = value; } }
        public Vector2 TileCoordinate { get { return new Vector2((int)(Math.Round(position.Y / MSMap.tileDimension)),(int) (Math.Round((position.X / MSMap.tileDimension))) ); } }

        private MSImageHolder moodFace;
        public MSImageHolder MoodFace { get { return moodFace; } }

        public MSCitizen(Model m, Texture2D texture, Effect effect, Vector3 position, Node p, CitizenState s, MSTypes mst)
            : base(position, MoodSwing.getInstance())
        {
            this.model = m;
            this.texture = texture;
            this.effect = effect;
            this.isThere = false;
            state = s;
            MDG = mst;
            this.path = p;
            Vector3 screenProjection = Game.GraphicsDevice.Viewport.Project(Position, ProjectionMatrix, MSCamera.GetInstance().GetView(), WorldMatrix);
            moodFace = new MSImageHolder(new Rectangle(0, 0, 50, 50), Game.Content.Load<Texture2D>("moodFace"), (Game as MoodSwing).SpriteBatch, Game);
            moodFace.Position = new Vector2(screenProjection.X, screenProjection.Y);
        }

        public void Follow(MSCitizen citizen)
        {
            path = citizen.Path;
            targetLocation = citizen.TargetLocation;
        }

        private const float WALK_SPEED = 0.35f;

        /// <summary>
        /// Moves a position of the citizen along a specified path.
        /// </summary>
        /// <param name="mapArray"> The map array where the citizen is in.</param>
        public virtual void Walk( MS3DTile[,] mapArray )
        {
            if ( state != CitizenState.WAITING)
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

                if (isThere && state == CitizenState.MOB)
                    MSMoodManager.GetInstance().takeDamage();
                adjustWorldMatrix();

                if (state == CitizenState.MOB)
                {
                    Viewport v = new Viewport();
                    Vector3 screenProjection = Game.GraphicsDevice.Viewport.Project(Position, ProjectionMatrix, MSCamera.GetInstance().GetView(), Matrix.Identity);
                    moodFace.Position = new Vector2(screenProjection.X - moodFace.Size.X / 3, screenProjection.Y - moodFace.Size.Y);
                }
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
                    effect.Parameters["DiffuseLightDirection"].SetValue(LightSource - Position);
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

        public void changeModel(String modelName, MSTypes mdg)
        {
            String textureName = "MTextures/tao";
            switch (mdg)
            {
                case MSTypes.POVERTY:
                    textureName = "MTextures/mob_A";
                    break;
                case MSTypes.EDUCATION:
                    textureName = "MTextures/mob_B";
                    break;
                case MSTypes.GENDER_EQUALITY:
                    textureName = "MTextures/mob_C";
                    break;
                case MSTypes.CHILD_HEALTH:
                    textureName = "MTextures/mob_D";
                    break;
                case MSTypes.MATERNAL_HEALTH:
                    textureName = "MTextures/mob_E";
                    break;
                case MSTypes.HIV_AIDS:
                    textureName = "MTextures/mob_F";
                    break;
                case MSTypes.ENVIRONMENT:
                    textureName = "MTextures/mob_G";
                    break;
                case MSTypes.GLOBAL_PARTNERSHIP:
                    textureName = "MTextures/mob_H";
                    break;
            }
            changeModel(modelName, textureName);
        }
    }
}
