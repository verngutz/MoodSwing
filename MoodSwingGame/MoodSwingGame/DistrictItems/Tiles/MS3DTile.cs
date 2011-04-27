using System;
using System.IO;
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

        String modelName;
        String textureName;
        String effectName;

        public MS3DTile(String mName, String tName, String eName, Vector3 position, float rotation, int row, int column, int height)
            : base(position)
        {
            this.model = MoodSwing.GetInstance().Content.Load<Model>(mName);
            this.texture = null; //MoodSwing.GetInstance().Content.Load<Texture2D>(tName); 
            this.Effect = MoodSwing.GetInstance().Content.Load<Effect>(eName);

            this.modelName = mName;
            this.textureName = tName;
            this.effectName = eName;

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

        public virtual void load(StreamReader sr)
        {
            modelName = sr.ReadLine();
            textureName = sr.ReadLine();
            effectName = sr.ReadLine();
            string[] position = sr.ReadLine().Split(' ');
            this.position.X = float.Parse(position[0]);
            this.position.Y = float.Parse(position[1]);
            this.position.Z = float.Parse(position[2]);
            rotation = float.Parse(sr.ReadLine());
            row = Int32.Parse(sr.ReadLine());
            column = Int32.Parse(sr.ReadLine());
        }

        public override String toString() {
            String toReturn = "";
            toReturn += modelName + "\n";
            toReturn += textureName + "\n";
            toReturn += effectName + "\n";
            toReturn += position.X + " " + position.Y + " " + position.Z + "\n";
            toReturn += rotation + "\n";
            toReturn += row + "\n";
            toReturn += column + "\n";

            return toReturn;
        }
    }
}
