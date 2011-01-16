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
    public abstract class MSTile : MS3DComponent
    {

        private Model model;
        private Texture2D wrapper;

        public MSTile(Model model, Texture2D wrapper, Vector3 position)
            : base(position, MoodSwing.getInstance())
        {
            this.model = model;
            this.wrapper = wrapper;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.TextureEnabled = true;
                    effect.Texture = wrapper;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
                System.Console.WriteLine(Position);
            }
            base.Draw(gameTime);
        }

    }
}
