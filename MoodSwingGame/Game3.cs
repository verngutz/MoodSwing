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

namespace MoodSwingGame
{
    public class Game3 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 600f, 0.1f, 10000f);
        private static Vector3 cameraPosition = new Vector3(0, 0, 50);
        private Citizen c;
        private Matrix view;
        
        public Game3()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            c = new Citizen(Content.Load<Model>("person"), new Vector3(0, 0, 0));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
                c.Position += new Vector3(50, 0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                c.Position -= new Vector3(50, 0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                c.Position += new Vector3(0, 0, 50);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                c.Position -= new Vector3(0, 0, 50);

            view = Matrix.CreateLookAt(cameraPosition, c.Position, Vector3.UnitY);
            world = Matrix.CreateRotationY(0) * Matrix.CreateTranslation(c.Position);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //GraphicsDevice.RenderState.FillMode = FillMode.WireFrame;
            DrawModel(c.GetModel, world, view, projection);

            base.Draw(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            for(int i = 0; i < model.Meshes.Count; i++)
            {
                ModelMesh mesh = model.Meshes.ElementAt<ModelMesh>(i);
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    /**
                    if (i > 0)
                    {
                        world += 
                    }**/
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
        }
    }
}