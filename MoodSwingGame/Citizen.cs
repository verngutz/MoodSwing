﻿using System;
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

namespace MoodSwingGame
{
    class Citizen
    {
        private Model model;
        public Model GetModel { get { return model; } }
        public Vector3 Position { set; get; }

        public Citizen(Model model, Vector3 initialPosition)
        {
            this.model = model;
            Position = initialPosition;
        }

        public void move(Vector3 velocity)
        {
            Position = velocity;
        }
    }
}
