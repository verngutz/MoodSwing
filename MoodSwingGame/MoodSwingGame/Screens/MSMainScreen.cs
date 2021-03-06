﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoodSwingCoreComponents;
using MoodSwingGUI;

namespace MoodSwingGame
{
    public class MSMainScreen : MSScreen
    {
        private static MSMainScreen mainScreen;
        public static MSMainScreen getInstance()
        {
            if (mainScreen == null)
                mainScreen = new MSMainScreen(MoodSwing.GetInstance());
            return mainScreen;
        }

        private MSMainScreen(MoodSwing game)
            : base(game.Content.Load<Texture2D>("Main Menu"), 200, 200, 200, 200, Color.White, game.SpriteBatch, game)
        {
            MSAnimatingButton startGameButton = new MSAnimatingButton(
                    null,
                    OpenCityScreen.GetInstance(),
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

            MSAnimatingButton optionsButton = new MSAnimatingButton(
                    null,
                    OpenOptionsScreen.GetInstance(),
                    new Rectangle(350, 350, 472, 60),
                    game.Content.Load<Texture2D>("options"),
                    game.Content.Load<Texture2D>("optionsclicked"),
                    game.Content.Load<Texture2D>("optionsclicked"),
                    Color.White,
                    null,
                    Shape.AMORPHOUS,
                    SpriteBatch,
                    Game);

            optionsButton.HoverPosition = new ShiftRight75();
            optionsButton.UnhoverPosition = new ShiftLeft75();

            MSAnimatingButton res = new MSAnimatingButton(
                    null,
                    (new ClearSave()),
                    new Rectangle(300, 450, 574, 60),
                    game.Content.Load<Texture2D>("back"),
                    game.Content.Load<Texture2D>("backHovered"),
                    game.Content.Load<Texture2D>("backHovered"),
                    Color.White,
                    null,
                    Shape.AMORPHOUS,
                    SpriteBatch,
                    Game);

            res.HoverPosition = new ShiftRight75();
            res.UnhoverPosition = new ShiftLeft75();

            MSAnimatingButton exitButton = new MSAnimatingButton(
                    null,
                    new OpenExitConfirmation(OpenExitConfirmation.ExitType.ToWindows),
                    new Rectangle(300, 550, 574, 60),
                    game.Content.Load<Texture2D>("exit"),
                    game.Content.Load<Texture2D>("exitclicked"),
                    game.Content.Load<Texture2D>("exitclicked"),
                    Color.White,
                    null,
                    Shape.AMORPHOUS,
                    SpriteBatch,
                    Game);

            exitButton.HoverPosition = new ShiftRight75();
            exitButton.UnhoverPosition = new ShiftLeft75();

            AddComponent(startGameButton);

            AddComponent(optionsButton);
            AddComponent(res);
            AddComponent(exitButton);
            AddComponent(new MSPanel(Game.Content.Load<Texture2D>("gamescreen"), new Rectangle(0, 0, MSResolution.VirtualWidth, MSResolution.VirtualHeight), null, Shape.AMORPHOUS, SpriteBatch, Game));
        }

        public override void Update(GameTime gameTime)
        {
            HandleMouseInput();
            HandleKeyboardInput((Game as MoodSwing).OldKeyboardState);
            base.Update(gameTime);
        }
    }
}
