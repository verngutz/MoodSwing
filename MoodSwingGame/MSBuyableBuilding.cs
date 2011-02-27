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
    public class MSBuyableBuilding : MSBuilding
    {

        public enum BuyableBuildingState
        {
            BUYABLE,
            WAITING,
            TRANSFORMING,
            DONE
        }

        public BuyableBuildingState State { get; set; }
        private double startTime;
        private double timeCount;
        public void StartBuilding( GameTime gameTime)
        {
            startTime = gameTime.TotalGameTime.TotalSeconds;
            State = BuyableBuildingState.TRANSFORMING;
        }

        private int expectedWorkers;
        public void StartBuildProcess(int number, MS3DTile tile)
        {
            expectedWorkers = number;
            futureSelf = tile;
            State = BuyableBuildingState.WAITING;
        }
        public void AddWorkers()
        {
            expectedWorkers--;
            if (expectedWorkers == 0) StartBuilding(MoodSwing.getInstance().prevGameTime);
        }
        private Texture2D borderTexture;
        private Texture2D loadingTexture;
        private double buildTime;

        private MS3DTile futureSelf;
        public MS3DTile FutureSelf { get { return futureSelf; } }

        public MSBuyableBuilding(Model model, Texture2D texture, Effect effect, Vector3 position, int row, int column)
            : base(model, texture, effect, position, row, column) 
        {
            buildTime = 5;
            timeCount = 0;
            borderTexture = MoodSwing.getInstance().Content.Load<Texture2D>("BorderTexture");
            loadingTexture = MoodSwing.getInstance().Content.Load<Texture2D>("LoadingTexture");
            State = BuyableBuildingState.BUYABLE;
        }

        public override void Update(GameTime gameTime)
        {
            timeCount = gameTime.TotalGameTime.TotalSeconds - startTime;
            if (State == BuyableBuildingState.TRANSFORMING && timeCount >= buildTime)
            {
                State = BuyableBuildingState.DONE;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            
            base.Draw(gameTime);
        }

        public void DrawLoadingBar(GameTime gameTime)
        {
            if (State == BuyableBuildingState.TRANSFORMING)
            {
                int width = 50;
                int height = 10;
                SpriteBatch sb = MoodSwing.getInstance().SpriteBatch;
                Vector3 v = MoodSwing.getInstance().GraphicsDevice.Viewport.Project(Position + new Vector3(0, 0, 20),
                    ProjectionMatrix, MSCamera.GetInstance().GetView(),
                    Matrix.Identity);
                sb.Draw(borderTexture,
                    new Rectangle((int)v.X - MSMap.tileDimension / 2, (int)v.Y - MSMap.tileDimension, width, height), Color.White);
                sb.Draw(loadingTexture,
                    new Rectangle((int)v.X - MSMap.tileDimension / 2, (int)v.Y - MSMap.tileDimension, (int)((float)(timeCount * width / buildTime)), height), Color.White);
            }
        }
    }
}