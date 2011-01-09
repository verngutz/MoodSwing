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
        private Model model;

        protected Vector3 position;
        public Vector3 Position { get { return position; } }

        protected float yaw;
        public float Yaw { get { return yaw; } }

        protected float pitch;
        public float Pitch { get { return pitch; } }

        protected float roll;
        public float Roll { get { return roll; } }


        public MS3DComponent(Model model, Vector3 position, float yaw, float pitch, float roll, Game game) 
            : base(game) 
        {
            this.model = model;
            this.position = position;
            this.yaw = yaw;
            this.pitch = pitch;
            this.roll = roll;
        }
    }
}
