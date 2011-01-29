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
        private Vector3 position;
        public Vector3 Position { get { return position; } }

        private BoundingBox boundingBox;
        public BoundingBox BoundingBox { get { return boundingBox; } }

        protected Matrix world;
        protected Matrix projection;

        public MS3DComponent(Vector3 position, Game game)
            : base(game)
        {
            this.position = position;
            world = Matrix.CreateTranslation(position);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), game.GraphicsDevice.Viewport.AspectRatio, 5, 5000);
        }
    }
}
