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
    public abstract class MS3DTile : MS3DSingleModelComponent
    {
        private Model model;
        private Texture2D texture;

        protected override Model Model { get { return model; } }
        protected override Texture2D Texture { get { return texture; } }

        private float rotation;
        public float Rotation { get { return rotation; } }

        private int row;
        public int Row { get { return row; } }

        private int column;
        public int Column { get { return column; } }

        public Vector2 TileCoordinate { get { return new Vector2(row, column); } }

        public MS3DTile(Model model, Texture2D texture, Effect effect, Vector3 position, float rotation, int row, int column, int height)
            : base(position)
        {
            this.model = model;
            this.texture = texture;
            this.Effect = effect;

            int tileDimension = MSMap.tileDimension;
            foreach(ModelMesh mesh in Model.Meshes)
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
    }
}
