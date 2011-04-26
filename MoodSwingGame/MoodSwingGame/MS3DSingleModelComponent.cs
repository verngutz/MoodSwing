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
    public abstract class MS3DSingleModelComponent : MS3DComponent
    {
        protected abstract Model Model { get; }
        protected abstract Texture2D Texture { get; }

        private Effect effect;
        public Effect Effect 
        { 
            get { return effect; } 
            set { effect = value; } 
        }

        public Vector3 LightSource { set; get; }
        private Matrix[] boneTranforms;
        private bool boneTransformsUnset;


        public MS3DSingleModelComponent(Vector3 position)
            : base(position, MoodSwing.GetInstance()) 
        {
            effect = Game.Content.Load<Effect>("Mood");
            boneTransformsUnset = true;
        }

        public override void Draw(GameTime gameTime)
        {
            if (boneTransformsUnset)
            {
                boneTranforms = new Matrix[Model.Bones.Count];
                Model.Root.Transform *= Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(90), MathHelper.ToRadians(0), MathHelper.ToRadians(90));
                boneTransformsUnset = false;
            }
            Model.CopyAbsoluteBoneTransformsTo(boneTranforms);
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = Effect;
                    Effect.Parameters["World"].SetValue(boneTranforms[mesh.ParentBone.Index] * world);
                    Effect.Parameters["View"].SetValue(MSCamera.GetInstance().GetView());
                    Effect.Parameters["Projection"].SetValue(MSCamera.GetInstance().ProjectionMatrix);
                    Matrix worldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * world));
                    Effect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTransposeMatrix);
                    //Effect.Parameters["ViewVector"].SetValue(MSCamera.GetInstance().NormalizedViewVector);
                    Effect.Parameters["DiffuseLightDirection"].SetValue(LightSource - Position);
                    Effect.Parameters["Saturation"].SetValue(MSMoodManager.GetInstance().Mood);
                    Effect.Parameters["ModelTexture"].SetValue(Texture);
                }
                mesh.Draw();
            }
            base.Draw(gameTime);
        }

        public virtual String toString()
        {
            String toReturn = "";
            
            return toReturn;
        }
    }
}
