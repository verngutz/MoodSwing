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
    public abstract class MSTile : MS3DComponent, IComparable
    {

        private Model model;

        public MSTile(Model model, Vector3 position)
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
                    effect.World = world;
                    effect.View = MSCamera.getInstance().getView();
                    effect.Projection = projection;
                    effect.TextureEnabled = true;
                }
                mesh.Draw();
            }
            base.Draw(gameTime);
        }

        public int CompareTo(object ob)
        {
            MSTile t = (ob as MSTile);
            return (int)(Vector3.Distance(t.Position, MSCamera.getInstance().Position) -
                    Vector3.Distance(Position, MSCamera.getInstance().Position));
        }
    }
}
