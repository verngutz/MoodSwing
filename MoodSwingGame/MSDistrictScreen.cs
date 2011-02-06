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
        private MSCitizenHandler civilianHandler;
        private MSDistrictScreen(MoodSwing game)
            : base(null, 150, 150, 150, 150, game.SpriteBatch, game) 
        {
            map = new MSMap(@"Content\mapinfo.txt");
            elementsList = new List<MS3DComponent>();
            civilianHandler = MSCitizenHandler.getInstance();

            foreach (MSTile tile in map.MapArray)
            {
                if (tile is MS3DComponent) elementsList.Add(tile as MS3DComponent);
            }

            AddElement(
                new MSButton(
                    null,
                    new Exit(),
                    Vector2.Zero,
                    new Vector2(574, 60),
                    game.Content.Load<Texture2D>("exit"),
                    game.Content.Load<Texture2D>("exitclicked"),
                    game.Content.Load<Texture2D>("exit"),
                    SpriteBatch,
                    Color.White,
                    Shape.RECTANGULAR,
                    Game)
                    , Alignment.TOP_CENTER);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            elementsList.Sort();
            foreach( MS3DComponent temp in elementsList ) 
            {
                temp.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            checkCollision();
            CheckMouseClick((Game as MoodSwing).OldMouseState);
            map.Update(gameTime);
            MSCitizen person = civilianHandler.TryForBaby(map);
            if (person != null)
            {
                elementsList.Add(person);
            }

            List<MS3DComponent> toRemove = civilianHandler.Update(map);
            foreach (MS3DComponent temp in toRemove)
            {
                elementsList.Remove(temp);
            }
        }

        public void checkCollision()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Released 
                && MoodSwing.getInstance().OldMouseState.LeftButton == ButtonState.Pressed)
            {
                map.CheckCollision();
            }
        }

        
    }
}
