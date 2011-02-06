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
    public class MSCitizen : MS3DComponent
    {
        private Model model;
        private Node path;
        private bool isThere;
        public bool IsThere { get { return isThere; } }
        private Vector2 targetLocation;
        Random random;
        public MSCitizen( Model m, Vector3 position, Node p )
            : base(position, MoodSwing.getInstance())
        {
            this.model = m;
            this.path = p;
            this.isThere = false;
            this.random = new Random();
        }

        private const float WALK_SPEED = 0.55f;
        public void Walk( MSTile[,] mapArray )
        {
            Vector2 pos = new Vector2(Position.X, Position.Y);

            if (targetLocation == Vector2.Zero )
            {
                Vector3 targetVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
                targetLocation = new Vector2(targetVector3.X + random.Next(MSMap.tileDimension / 2) ,
                                              targetVector3.Y + random.Next(MSMap.tileDimension / 2));

            }
            Vector2 unit = targetLocation-pos;
            unit = Vector2.Normalize(unit);

            if (Vector2.Distance(pos, targetLocation) < 1)
            {
                this.position = new Vector3(targetLocation.X, targetLocation.Y, position.Z);
                if (path.next != null)
                {
                    path = path.next;
                    Vector3 targetVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
                    targetLocation = new Vector2(targetVector3.X + random.Next(MSMap.tileDimension / 2) - MSMap.tileDimension / 4,
                                                  targetVector3.Y + random.Next(MSMap.tileDimension / 2) - MSMap.tileDimension / 4);
                }
                else isThere = true;
            }
            else
                this.position += new Vector3(unit.X * WALK_SPEED, unit.Y * WALK_SPEED, 0);


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
