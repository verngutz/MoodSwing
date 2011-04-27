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
                cityScreen = new MSCityScreen(MoodSwing.GetInstance());
            return cityScreen;
        }


        private MSCityScreen(MoodSwing game)
            : base(game.Content.Load<Texture2D>("Main Menu"), 200, 200, 200, 200, Color.White, game.SpriteBatch, game)
        {
            MSAnimatingButton startGameButton = new MSAnimatingButton(
                    null,
                    new OpenDistrictScreen(MSDistrictScreen.DistrictName.FEAR),
                    new Rectangle(330, 250, 559, 60),
                    game.Content.Load<Texture2D>("startgame"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    Color.White,
                    null,
                    Shape.AMORPHOUS,
                    SpriteBatch,
                    Game);

            startGameButton.HoverPosition = new ShiftRight75();
            startGameButton.UnhoverPosition = new ShiftLeft75();

            MSAnimatingButton startGame2 = new MSAnimatingButton(
                    null,
                    new OpenDistrictScreen(MSDistrictScreen.DistrictName.SADNESS),
                    new Rectangle(350, 350, 472, 60),
                    game.Content.Load<Texture2D>("startgame"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    Color.White,
                    null,
                    Shape.AMORPHOUS,
                    SpriteBatch,
                    Game);

            startGame2.HoverPosition = new ShiftRight75();
            startGame2.UnhoverPosition = new ShiftLeft75();

            MSAnimatingButton startGame3 = new MSAnimatingButton(
                    null,
                    new OpenDistrictScreen(MSDistrictScreen.DistrictName.ANGER),
                    new Rectangle(300, 450, 574, 60),
                    game.Content.Load<Texture2D>("startgame"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    game.Content.Load<Texture2D>("startgameclicked"),
                    Color.White,
                    null,
                    Shape.AMORPHOUS,
                    SpriteBatch,
                    Game);

            startGame3.HoverPosition = new ShiftRight75();
            startGame3.UnhoverPosition = new ShiftLeft75();

            AddComponent(startGameButton);

            AddComponent(startGame2);

            AddComponent(startGame3);
            AddComponent(new MSPanel(Game.Content.Load<Texture2D>("gamescreen"), new Rectangle(0, 0, MSResolution.VirtualWidth, MSResolution.VirtualHeight), null, Shape.AMORPHOUS, SpriteBatch, Game));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleMouseInput();
            HandleKeyboardInput((Game as MoodSwing).OldKeyboardState);
        }
    }
}
