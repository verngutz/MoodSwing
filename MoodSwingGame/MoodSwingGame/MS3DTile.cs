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
    public abstract class MS3DTile : MS3DComponent
    {
        private Model model;
        private Texture2D texture;
        public Effect Effect { set; get; }
        public Vector3 LightSource { set; get; }

        private float rotation;
        public float Rotation { get { return rotation; } }

        private int row;
        public int Row { get { return row; } }

        private int column;
        public int Column { get { return column; } }

        public Vector2 TileCoordinate { get { return new Vector2(row, column); } }

        public Model TileModel { get { return model; } }

        public MS3DTile(Model model, Texture2D texture, Effect effect, Vector3 position, float rotation, int row, int column, int height)
            : base(position, MoodSwing.GetInstance())
        {
            this.model = model;
            this.texture = texture;
            this.Effect = effect;
            int tileDimension = MSMap.tileDimension;
            foreach(ModelMesh mesh in model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(boundingSphere, mesh.BoundingSphere);
            }
            boundingSphere = boundingSphere.Transform(world);


            boundingBox = new BoundingBox(Position + new Vector3(-tileDimension / 2, -tileDimension / 2, height/2),
                    Position + new Vector3(tileDimension / 2, tileDimension / 2, -height/2));
            
            world = Matrix.CreateRotationZ(rotation) * world;

            this.rotation = rotation;
            this.row = row;
            this.column = column;
            
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = Effect;
                    Effect.Parameters["World"].SetValue(world);
                    Effect.Parameters["View"].SetValue(MSCamera.GetInstance().GetView());
                    Effect.Parameters["Projection"].SetValue(MSCamera.GetInstance().ProjectionMatrix);
                    Matrix worldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * world));
                    Effect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTransposeMatrix);
                    //Effect.Parameters["ViewVector"].SetValue(MSCamera.GetInstance().NormalizedViewVector);
                    Effect.Parameters["DiffuseLightDirection"].SetValue(LightSource - Position);
                    Effect.Parameters["Saturation"].SetValue(MSMoodManager.GetInstance().Mood);
                    Effect.Parameters["ModelTexture"].SetValue(texture);
                }
                mesh.Draw();
            }
            base.Draw(gameTime);
        }

        

    }
}
