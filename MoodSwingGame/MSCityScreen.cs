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
            AddComponent(new MSButton(
                    null,
                    new OpenMainScreen(),
                    new Rectangle(0, 0, 574, 60),
                    game.Content.Load<Texture2D>("exit"),
                    game.Content.Load<Texture2D>("exitClicked"),
                    game.Content.Load<Texture2D>("exitClicked"),   
                    Color.White,
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game)
                    , Alignment.BOTTOM_CENTER);

            AddComponent(new MSButton(
                    null,
                    new OpenDistrictScreen(),
                    new Rectangle(0, 0, 559, 60),
                    game.Content.Load<Texture2D>("startgame"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    game.Content.Load<Texture2D>("startgame"),                    
                    Color.White,
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game)
                    , Alignment.MIDDLE_CENTER);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleMouseInput((Game as MoodSwing).OldMouseState);
            HandleKeyboardInput((Game as MoodSwing).OldKeyboardState);
        }
    }
}
