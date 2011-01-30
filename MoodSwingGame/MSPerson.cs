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
    public class MSPerson : MS3DComponent
    {
        private Model model;
        private Node path;

        public MSPerson( Model m, Vector3 position, Node p )
            : base(position, MoodSwing.getInstance())
        {
            this.model = m;
            this.path = p.next;
        }

        public void walk( MSTile[,] mapArray )
        {
            //System.Console.WriteLine("TARGET: " + path.Position);
            Vector2 pos = new Vector2(Position.X, Position.Y);
            
            Vector3 target = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MSRoad).Position;
            Vector2 tar = new Vector2(target.X, target.Y);
            
            Vector2 unit = tar-pos;
            unit = Vector2.Normalize(unit);

            if (Vector2.Distance(pos, tar) < 1)
            {
                this.position = new Vector3( tar.X, tar.Y, position.Z );
                if( path.next != null ) path = path.next;
            }
            else 
                this.position += new Vector3(unit.X, unit.Y, 0);


            adjustWorldMatrix();

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

    }
}
