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
    public class MSCityScreen : MSScreen
    {
        private static MSCityScreen cityScreen;
        public static MSCityScreen getInstance()
        {
            if (cityScreen == null)
                cityScreen = new MSCityScreen(MoodSwing.getInstance());
            return cityScreen;
        }


        private MSCityScreen(MoodSwing game)
            : base(game.Content.Load<Texture2D>("CityView"), 150, 150, 150, 150, game.SpriteBatch, game) 
        {
            AddElement(
                new MSButton(
                    new MSLabel("Return", new Vector2(20, 10), new Vector2(60, 30), game.Content.Load<SpriteFont>("Temp"), Color.Black, SpriteBatch, game),
                    new OpenMainScreen(),
                    Vector2.Zero,
                    new Vector2(100, 50),
                    game.Content.Load<Texture2D>("Button"),
                    game.Content.Load<Texture2D>("ButtonClicked"),
                    game.Content.Load<Texture2D>("ButtonHover"),
                    SpriteBatch,
                    Color.White,
                    Shape.RECTANGULAR,
                    Game)
                    , Alignment.BOTTOM_CENTER);

            AddElement(
                new MSButton(
                    new MSLabel("District View", new Vector2(20, 10), new Vector2(60, 30), game.Content.Load<SpriteFont>("Temp"), Color.Black, SpriteBatch, game),
                    new OpenDistrictScreen(),
                    Vector2.Zero,
                    new Vector2(100, 50),
                    game.Content.Load<Texture2D>("Button"),
                    game.Content.Load<Texture2D>("ButtonClicked"),
                    game.Content.Load<Texture2D>("ButtonHover"),
                    SpriteBatch,
                    Color.White,
                    Shape.RECTANGULAR,
                    Game)
                    , Alignment.MIDDLE_CENTER);
        }

        public override void Update(GameTime gameTime)
        {
            CheckMouseClick((Game as MoodSwing).OldMouseState);
            base.Update(gameTime);
        }
    }
}
