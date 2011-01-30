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
    public class MSDistrictScreen: MSScreen
    {
        private static MSDistrictScreen districtScreen;
        public static MSDistrictScreen getInstance()
        {
            if (districtScreen == null)
                districtScreen = new MSDistrictScreen(MoodSwing.getInstance());
            return districtScreen;
        }

        private MSMap map;
        private MSDistrictScreen(MoodSwing game)
            : base(null, 150, 150, 150, 150, game.SpriteBatch, game) 
        {
            map = new MSMap(@"Content\mapinfo.txt");
            
            AddElement(
                new MSButton(
                    new MSLabel("Exit", new Vector2(20, 10), new Vector2(60, 30), game.Content.Load<SpriteFont>("Temp"), Color.Black, SpriteBatch, game),
                    new Exit(),
                    Vector2.Zero,
                    new Vector2(100, 50),
                    game.Content.Load<Texture2D>("Button"),
                    game.Content.Load<Texture2D>("ButtonClicked"),
                    game.Content.Load<Texture2D>("ButtonHover"),
                    SpriteBatch,
                    Color.White,
                    Shape.RECTANGULAR,
                    Game)
                    , Alignment.TOP_CENTER);
        }

        public override void Draw(GameTime gameTime)
        {
            
            base.Draw(gameTime);
            map.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            checkCollision();
            CheckMouseClick((Game as MoodSwing).OldMouseState);
            map.Update(gameTime);
            base.Update(gameTime);
        }

        public void checkCollision()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Released &&
                MoodSwing.getInstance().OldMouseState.LeftButton == ButtonState.Pressed)
            {
                map.checkCollision();
            }
        }

        
    }
}
