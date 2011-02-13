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
    public abstract class MS3DTile : MS3DComponent, MSTile
    {
        private Model model;
        public Model TileModel { get { return model; } }
        public MS3DTile(Model model, Vector3 position)
            : base(position, MoodSwing.getInstance())
        {
            this.model = model;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    //effect.LightingEnabled = false;
                    effect.World = world;
                    effect.View = MSCamera.getInstance().getView();
                    effect.Projection = projection;
                    effect.TextureEnabled = true;
                }
                mesh.Draw();
            }
            base.Draw(gameTime);
        }

        

    }
}
