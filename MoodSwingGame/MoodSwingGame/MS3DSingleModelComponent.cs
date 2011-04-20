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

        public MS3DSingleModelComponent(Vector3 position)
            : base(position, MoodSwing.GetInstance()) 
        {
            effect = Game.Content.Load<Effect>("Mood"); 
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ModelMesh mesh in Model.Meshes)
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
                    Effect.Parameters["ModelTexture"].SetValue(Texture);
                }
                mesh.Draw();
            }
            base.Draw(gameTime);
        }
    }
}
