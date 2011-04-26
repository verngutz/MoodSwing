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
using MoodSwingGraphics;

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

        public MSChangeableBuilding(String model, String texture, String effect, Vector3 position, float rotation, int row, int column, int height)
            : base(model, texture, effect, position, rotation, row, column, height) 
        {
            futureSelf = null;
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

                for (int i = 0; i < MSSmokePlumeParticleSystem.THICKNESS / 5; i++)
                {
                    Vector3 min = BoundingBox.Min;
                    Vector3 max = boundingBox.Max;
                    (Game as MoodSwing).SmokeParticles.AddParticle(
                        new Vector3(min.X,
                                    MathHelper.Lerp(min.Y, max.Y, MSRandom.GetUniform()),
                                    MathHelper.Lerp(min.Z, max.Z, MSRandom.GetUniform())),
                        new Vector3(0, 0, 5));
                    (Game as MoodSwing).SmokeParticles.AddParticle(
                        new Vector3(MathHelper.Lerp(min.X, max.X, MSRandom.GetUniform()),
                                    min.Y,
                                    MathHelper.Lerp(min.Z, max.Z, MSRandom.GetUniform())),
                        new Vector3(0, 0, 5));
                    (Game as MoodSwing).SmokeParticles.AddParticle(
                        new Vector3(max.X,
                                    MathHelper.Lerp(min.Y, max.Y, MSRandom.GetUniform()),
                                    MathHelper.Lerp(min.Z, max.Z, MSRandom.GetUniform())),
                        new Vector3(0, 0, 5));
                    (Game as MoodSwing).SmokeParticles.AddParticle(
                        new Vector3(MathHelper.Lerp(min.X, max.X, MSRandom.GetUniform()),
                                    max.Y,
                                    MathHelper.Lerp(min.Z, max.Z, MSRandom.GetUniform())),
                        new Vector3(0, 0, 5));
                    (Game as MoodSwing).SmokeParticles.AddParticle(
                        new Vector3(MathHelper.Lerp(min.X, max.X, MSRandom.GetUniform()),
                                    MathHelper.Lerp(min.Y, max.Y, MSRandom.GetUniform()),
                                    min.Z),
                        new Vector3(0, 0, 5));
                }

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

        public override string toString()
        {
            String toReturn = "";
            switch (this.State)
            {
                case MSChangeableBuildingState.DONE:
                    toReturn += "DONE";
                    break;
                case MSChangeableBuildingState.IDLE:
                    toReturn += "IDLE";
                    break;
                case MSChangeableBuildingState.TRANSFORMING:
                    toReturn += "TRANSFORMING";
                    break;
                case MSChangeableBuildingState.WAITING:
                    toReturn += "WAITING";
                    break;
            }
            toReturn += "\n";
            toReturn += startTime + "\n";
            toReturn += timeCount + "\n";
            toReturn += expectedWorkers + "\n";
            toReturn += buildTime + "\n";
            if (futureSelf == null)
                toReturn += "null\n";
            else
                toReturn += futureSelf.toString();

            toReturn += base.toString();
            return toReturn;
        }
    }
}