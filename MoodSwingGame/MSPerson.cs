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
        private bool isThere;
        public bool IsThere { get { return isThere; } }

        public MSPerson( Model m, Vector3 position, Node p )
            : base(position, MoodSwing.getInstance())
        {
            this.model = m;
            this.path = p;
            this.isThere = false;
        }

        public void walk( MSTile[,] mapArray )
        {
            Vector2 pos = new Vector2(Position.X, Position.Y);
            
            Vector3 targetVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
            Vector2 targetVector2 = new Vector2(targetVector3.X, targetVector3.Y);
            
            Vector2 unit = targetVector2-pos;
            unit = Vector2.Normalize(unit);

            if (Vector2.Distance(pos, targetVector2) < 1)
            {
                this.position = new Vector3( targetVector2.X, targetVector2.Y, position.Z );
                if (path.next != null) path = path.next;
                else isThere = true;
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
