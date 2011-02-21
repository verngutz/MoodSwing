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
    public abstract class MS3DTile : MS3DComponent
    {
        private Model model;
        private Texture2D texture;
        private Effect effect;
        private Vector3 lightSource;

        public Model TileModel { get { return model; } }
        public MS3DTile(Model model, Texture2D texture, Effect effect, Vector3 position)
            : base(position, MoodSwing.getInstance())
        {
            this.model = model;
            this.texture = texture;
            this.effect = effect;
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

        

    }
}
