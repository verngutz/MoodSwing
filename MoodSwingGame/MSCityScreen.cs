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
                    null,
                    new OpenMainScreen(),
                    Vector2.Zero,
                    new Vector2(574, 60),
                    game.Content.Load<Texture2D>("exit"),
                    game.Content.Load<Texture2D>("exitClicked"),
                    game.Content.Load<Texture2D>("exit"),
                    SpriteBatch,
                    Color.White,
                    Shape.RECTANGULAR,
                    Game)
                    , Alignment.BOTTOM_CENTER);

            AddElement(
                new MSButton(
                    null,
                    new OpenDistrictScreen(),
                    Vector2.Zero,
                    new Vector2(559, 60),
                    game.Content.Load<Texture2D>("startgame"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    game.Content.Load<Texture2D>("startgame"),
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
