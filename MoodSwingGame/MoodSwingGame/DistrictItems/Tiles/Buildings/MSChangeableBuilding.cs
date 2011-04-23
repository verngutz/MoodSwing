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
using MoodSwingCoreComponents;
using MoodSwingGUI;

namespace MoodSwingGame
{
    public enum MSChangeableBuildingState
    {
        IDLE,
        WAITING,
        TRANSFORMING,
        DONE
    }

    public abstract class MSChangeableBuilding : MSBuilding
    {
        public MSChangeableBuildingState State { get; set; }
        private double startTime;
        private double timeCount;
        public void StartBuilding( GameTime gameTime)
        {
            startTime = gameTime.TotalGameTime.TotalSeconds;
            State = MSChangeableBuildingState.TRANSFORMING;
        }

        private int expectedWorkers;
        public void StartBuildProcess(int number, MS3DTile tile)
        {
            expectedWorkers = number;
            futureSelf = tile;
            State = MSChangeableBuildingState.WAITING;
        }
        public void AddWorkers()
        {
            expectedWorkers--;
            if (expectedWorkers == 0) StartBuilding(MoodSwing.GetInstance().prevGameTime);
        }
        private MSProgressBar progressBar;
        private double buildTime;

        private MS3DTile futureSelf;
        public MS3DTile FutureSelf { get { return futureSelf; } }

        public MSChangeableBuilding(Model model, Texture2D texture, Effect effect, Vector3 position, float rotation, int row, int column, int height)
            : base(model, texture, effect, position, rotation, row, column, height) 
        {
            buildTime = 5;
            timeCount = 0;
            Texture2D borderTexture = MoodSwing.GetInstance().Content.Load<Texture2D>("BorderTexture");
            Texture2D loadingTexture = MoodSwing.GetInstance().Content.Load<Texture2D>("LoadingTexture");

            progressBar = new MSProgressBar(new Rectangle((int)Position.X, (int)Position.Y, 50, 10), 
                MoodSwing.GetInstance().SpriteBatch,
                MoodSwing.GetInstance(), 
                borderTexture, loadingTexture, null, MSProgressBar.Orientation.HORIZONTAL);

            State = MSChangeableBuildingState.IDLE;
        }

        public override void Update(GameTime gameTime)
        {
            timeCount = 0;
            if (State == MSChangeableBuildingState.TRANSFORMING)
            {
                timeCount = gameTime.TotalGameTime.TotalSeconds - startTime;
                (Game as MoodSwing).SmokeParticles.AddParticle(position, Vector3.Zero);
                if (timeCount >= buildTime) State = MSChangeableBuildingState.DONE;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            
            base.Draw(gameTime);
        }

        public void DrawLoadingBar(GameTime gameTime)
        {
            if (State == MSChangeableBuildingState.TRANSFORMING || State == MSChangeableBuildingState.WAITING)
            {
                Vector3 v = MoodSwing.GetInstance().GraphicsDevice.Viewport.Project(Position + new Vector3(0, 0, 20), // <- offset for progress bar
                    MSCamera.GetInstance().ProjectionMatrix, MSCamera.GetInstance().GetView(),
                    Matrix.Identity);
                progressBar.Progress = timeCount / buildTime;
                progressBar.Position = new Vector2(v.X - MSMap.tileDimension/2, v.Y - MSMap.tileDimension);
                progressBar.Draw(gameTime);
            }
        }
    }
}