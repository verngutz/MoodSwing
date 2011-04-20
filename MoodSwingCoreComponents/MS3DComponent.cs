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

namespace MoodSwingCoreComponents
{
    public abstract class MS3DComponent : DrawableGameComponent
    {
        protected Vector3 position;
        public Vector3 Position { get { return position; } }

        protected BoundingSphere boundingSphere;
        public BoundingSphere BoundingSphere { get { return boundingSphere; } }

        protected BoundingBox boundingBox;
        public BoundingBox BoundingBox { get { return boundingBox; } }

        protected Matrix world;
        public Matrix WorldMatrix { get { return world; } }
        public void adjustWorldMatrix()
        {
            world = Matrix.CreateTranslation(position);
        }

        public MS3DComponent(Vector3 position, Game game)
            : base(game)
        {
            this.position = position;
            world = Matrix.CreateTranslation(position);
        }
    }
}
