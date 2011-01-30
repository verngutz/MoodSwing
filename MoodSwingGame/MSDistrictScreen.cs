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
        private List<MS3DComponent> elementsList;
        private MSCivilianHandler civilianHandler;
        private MSDistrictScreen(MoodSwing game)
            : base(null, 150, 150, 150, 150, game.SpriteBatch, game) 
        {
            map = new MSMap(@"Content\mapinfo.txt");
            elementsList = new List<MS3DComponent>();
            civilianHandler = MSCivilianHandler.getInstance();

            foreach (MSTile tile in map.MapArray)
            {
                if (tile is MS3DComponent) elementsList.Add(tile as MS3DComponent);
            }

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
            elementsList.Sort();
            foreach( MS3DComponent temp in elementsList ) 
            {
                temp.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Released &&
                MoodSwing.getInstance().OldMouseState.LeftButton == ButtonState.Pressed)
            {
                map.CheckCollision();
            }

            CheckMouseClick((Game as MoodSwing).OldMouseState);
            map.Update(gameTime);
            MSPerson person = civilianHandler.TryForBaby(map);
            if (person != null)
            {
                elementsList.Add(person);
            }

            List<MS3DComponent> toRemove = civilianHandler.Update(map);
            foreach (MS3DComponent temp in toRemove)
            {
                elementsList.Remove(temp);
            }
            base.Update(gameTime);
        }

        
    }
}
