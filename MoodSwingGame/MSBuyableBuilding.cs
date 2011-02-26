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

        public bool IsTransforming { get; set; }
        public bool IsDoneTransforming { get; set; }

        private int startTime;
        private int timeCount;
        public void StartTransform( GameTime gameTime)
        {
            startTime = gameTime.TotalGameTime.Seconds;
            IsTransforming = true;
        }

        private int expectedWorkers;
        public void WaitForWorkers(int number)
        {
            expectedWorkers = number;
        }
        public void AddWorkers()
        {
            expectedWorkers--;
            if (expectedWorkers == 0) StartTransform(MoodSwing.getInstance().prevGameTime);
        }
        private Texture2D borderTexture;
        private Texture2D loadingTexture;
        private int buildTime;
        public MSBuyableBuilding(Model model, Texture2D texture, Effect effect, Vector3 position, int row, int column)
            : base(model, texture, effect, position, row, column) 
        {
            buildTime = 20;
            timeCount = 0;
            borderTexture = MoodSwing.getInstance().Content.Load<Texture2D>("BorderTexture");
            loadingTexture = MoodSwing.getInstance().Content.Load<Texture2D>("LoadingTexture");
        }

        public override void Update(GameTime gameTime)
        {
            timeCount = gameTime.TotalGameTime.Seconds - startTime;
            if (timeCount >= buildTime)
            {
                IsTransforming = false;
                IsDoneTransforming = true;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (IsTransforming)
            {
                int width = 50;
                int height = 10;
                SpriteBatch sb = MoodSwing.getInstance().SpriteBatch;
                Vector3 v = MoodSwing.getInstance().GraphicsDevice.Viewport.Project( Position + new Vector3(0,0,20), 
                    ProjectionMatrix, MSCamera.GetInstance().GetView(),
                    Matrix.Identity);
                sb.Draw(borderTexture, 
                    new Rectangle((int)v.X - MSMap.tileDimension / 2, (int)v.Y - MSMap.tileDimension, width, height), Color.White);
                sb.Draw(loadingTexture, 
                    new Rectangle((int)v.X - MSMap.tileDimension / 2, (int)v.Y - MSMap.tileDimension, (int)((float)(timeCount * width / buildTime)), height), Color.White);
                System.Console.WriteLine((float)(timeCount*width / buildTime));
            }
            base.Draw(gameTime);
        }
    }
}