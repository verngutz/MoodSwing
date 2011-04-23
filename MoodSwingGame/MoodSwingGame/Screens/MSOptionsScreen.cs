using System;
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
    public class MSOptionsScreen : MSScreen
    {
        private static MSOptionsScreen optionsScreen;
        public static MSOptionsScreen getInstance()
        {
            if (optionsScreen == null)
                optionsScreen = new MSOptionsScreen(MoodSwing.GetInstance());
            return optionsScreen;
        }

        public MSScreen ReturnScreen { set; get; }

        private MSOptionsScreen(MoodSwing game)
            : base(game.Content.Load<Texture2D>("CityView"), 150, 150, 150, 150, game.SpriteBatch, game)
        {
            MSTabbedPanel tabbedOptions = new MSTabbedPanel(this);
            MSPanel graphicsTab = new MSPanel(Game.Content.Load<Texture2D>("BlackOut"), new Rectangle(50, 50, 924, 668), 50, 50, 50, 50, null, Shape.RECTANGULAR, SpriteBatch, Game);
            tabbedOptions.AddTab(new MSTab(
                new MSButton(
                    new MSResizingLabel(
                        "Graphics",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        SpriteBatch,
                        Game),
                    new DoNothing(),
                    new Rectangle(0, 0, 50, 50),
                    game.Content.Load<Texture2D>("unchecked"),
                    game.Content.Load<Texture2D>("uncheckedclicked"),
                    game.Content.Load<Texture2D>("uncheckedhovered"),
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game),
                new MSButton(
                    new MSResizingLabel(
                        "Graphics",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        SpriteBatch,
                        Game),
                    new DoNothing(),
                    new Rectangle(0, 0, 50, 50),
                    game.Content.Load<Texture2D>("checked"),
                    game.Content.Load<Texture2D>("checkedclicked"),
                    game.Content.Load<Texture2D>("checkedhovered"),
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game),
                    true,
                    graphicsTab));

            graphicsTab.AddComponent(new MSCheckbox(
                new MSButton(
                    new MSResizingLabel(
                        "Full Screen",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        Color.White,
                        SpriteBatch,
                        Game),
                    new ToggleFullScreen(true),
                    new Rectangle(0, 0, 50, 50),
                    game.Content.Load<Texture2D>("unchecked"),
                    game.Content.Load<Texture2D>("uncheckedclicked"),
                    game.Content.Load<Texture2D>("uncheckedhovered"),
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game),
                new MSButton(
                    new MSResizingLabel(
                        "Full Screen",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        Color.White,
                        SpriteBatch,
                        Game),
                    new ToggleFullScreen(false),
                    new Rectangle(0, 0, 50, 50),
                    game.Content.Load<Texture2D>("checked"),
                    game.Content.Load<Texture2D>("checkedclicked"),
                    game.Content.Load<Texture2D>("checkedhovered"),
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game),
                 MSResolution.IsFullScreen), Alignment.TOP_LEFT);

            graphicsTab.AddComponent(new MSCheckbox(
                new MSButton(
                    new MSResizingLabel(
                        "Bloom",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        Color.White,
                        SpriteBatch,
                        Game),
                    new ToggleBloom(true),
                    new Rectangle(0, 0, 50, 50),
                    game.Content.Load<Texture2D>("unchecked"),
                    game.Content.Load<Texture2D>("uncheckedclicked"),
                    game.Content.Load<Texture2D>("uncheckedhovered"),
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game),
                new MSButton(
                    new MSResizingLabel(
                        "Bloom",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        Color.White,
                        SpriteBatch,
                        Game),
                    new ToggleBloom(false),
                    new Rectangle(0, 0, 50, 50),
                    game.Content.Load<Texture2D>("checked"),
                    game.Content.Load<Texture2D>("checkedclicked"),
                    game.Content.Load<Texture2D>("checkedhovered"),
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game),
                game.Bloom.Visible), Alignment.MIDDLE_LEFT);

            MSPanel resolutionPanel = new MSPanel(null, new Rectangle(500, 60, 150, 200), null, Shape.RECTANGULAR, SpriteBatch, Game);
            MSRadioButtonGroup resolution = new MSRadioButtonGroup(resolutionPanel);
            resolution.AddRadioButton(
                new MSRadioButton(
                    new MSButton(
                        new MSResizingLabel(
                        "800 by 600",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        Color.White,
                        SpriteBatch,
                        Game),
                        new ChangeResolution(800, 600),
                        new Rectangle(0, 0, 50, 50),
                        game.Content.Load<Texture2D>("unchecked"),
                        game.Content.Load<Texture2D>("uncheckedclicked"),
                        game.Content.Load<Texture2D>("uncheckedhovered"),
                        null,
                        Shape.RECTANGULAR,
                        SpriteBatch,
                        Game),
                    new MSButton(
                        new MSResizingLabel(
                        "800 by 600",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        Color.White,
                        SpriteBatch,
                        Game),
                        null,
                        new Rectangle(0, 0, 50, 50),
                        game.Content.Load<Texture2D>("checked"),
                        game.Content.Load<Texture2D>("checkedclicked"),
                        game.Content.Load<Texture2D>("checkedhovered"),
                        null,
                        Shape.RECTANGULAR,
                        SpriteBatch,
                        Game),
                    false), Alignment.TOP_CENTER);

            resolution.AddRadioButton(
                new MSRadioButton(
                    new MSButton(
                        new MSResizingLabel(
                        "1024 by 768",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        Color.White,
                        SpriteBatch,
                        Game),
                        new ChangeResolution(1024, 768),
                        new Rectangle(0, 0, 50, 50),
                        game.Content.Load<Texture2D>("unchecked"),
                        game.Content.Load<Texture2D>("uncheckedclicked"),
                        game.Content.Load<Texture2D>("uncheckedhovered"),
                        null,
                        Shape.RECTANGULAR,
                        SpriteBatch,
                        Game),
                    new MSButton(
                        new MSResizingLabel(
                        "1024 by 768",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        Color.White,
                        SpriteBatch,
                        Game),
                        null,
                        new Rectangle(0, 0, 50, 50),
                        game.Content.Load<Texture2D>("checked"),
                        game.Content.Load<Texture2D>("checkedclicked"),
                        game.Content.Load<Texture2D>("checkedhovered"),
                        null,
                        Shape.RECTANGULAR,
                        SpriteBatch,
                        Game),
                    true), Alignment.MIDDLE_CENTER);

            resolution.AddRadioButton(
                new MSRadioButton(
                    new MSButton(
                        new MSResizingLabel(
                        "1280 by 800",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        Color.White,
                        SpriteBatch,
                        Game),
                        new ChangeResolution(1280, 800),
                        new Rectangle(0, 0, 50, 50),
                        game.Content.Load<Texture2D>("unchecked"),
                        game.Content.Load<Texture2D>("uncheckedclicked"),
                        game.Content.Load<Texture2D>("uncheckedhovered"),
                        null,
                        Shape.RECTANGULAR,
                        SpriteBatch,
                        Game),
                     new MSButton(
                        new MSResizingLabel(
                        "1280 by 800",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        Color.White,
                        SpriteBatch,
                        Game),
                        null,
                        new Rectangle(0, 0, 50, 50),
                        game.Content.Load<Texture2D>("checked"),
                        game.Content.Load<Texture2D>("checkedclicked"),
                        game.Content.Load<Texture2D>("checkedhovered"),
                        null,
                        Shape.RECTANGULAR,
                        SpriteBatch,
                        Game),
                    false), Alignment.BOTTOM_CENTER);
            graphicsTab.AddComponent(resolutionPanel, Alignment.TOP_RIGHT);
            AddComponent(graphicsTab);

            MSPanel audioTab = new MSPanel(Game.Content.Load<Texture2D>("BlackOut"), new Rectangle(50, 50, 924, 668), 50, 50, 50, 50, null, Shape.RECTANGULAR, SpriteBatch, Game);
            tabbedOptions.AddTab(new MSTab(
                new MSButton(
                    new MSResizingLabel(
                        "Audio",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        SpriteBatch,
                        Game),
                    new DoNothing(),
                    new Rectangle(200, 0, 50, 50),
                    game.Content.Load<Texture2D>("unchecked"),
                    game.Content.Load<Texture2D>("uncheckedclicked"),
                    game.Content.Load<Texture2D>("uncheckedhovered"),
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game),
                new MSButton(
                    new MSResizingLabel(
                        "Audio",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        SpriteBatch,
                        Game),
                    new DoNothing(),
                    new Rectangle(200, 0, 50, 50),
                    game.Content.Load<Texture2D>("checked"),
                    game.Content.Load<Texture2D>("checkedclicked"),
                    game.Content.Load<Texture2D>("checkedhovered"),
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game),
                    false,
                    audioTab));
            AddComponent(audioTab);

            MSPanel gameplayTab = new MSPanel(Game.Content.Load<Texture2D>("BlackOut"), new Rectangle(50, 50, 924, 668), 50, 50, 50, 50, null, Shape.RECTANGULAR, SpriteBatch, Game);
            tabbedOptions.AddTab(new MSTab(
                new MSButton(
                    new MSResizingLabel(
                        "Gameplay",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        SpriteBatch,
                        Game),
                    new DoNothing(),
                    new Rectangle(400, 0, 50, 50),
                    game.Content.Load<Texture2D>("unchecked"),
                    game.Content.Load<Texture2D>("uncheckedclicked"),
                    game.Content.Load<Texture2D>("uncheckedhovered"),
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game),
                new MSButton(
                    new MSResizingLabel(
                        "Gameplay",
                        new Rectangle(50, 0, 100, 25),
                        Game.Content.Load<SpriteFont>("Temp"),
                        SpriteBatch,
                        Game),
                    new DoNothing(),
                    new Rectangle(400, 0, 50, 50),
                    game.Content.Load<Texture2D>("checked"),
                    game.Content.Load<Texture2D>("checkedclicked"),
                    game.Content.Load<Texture2D>("checkedhovered"),
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game),
                    false,
                    gameplayTab));
            AddComponent(gameplayTab);

            AddComponent(new MSButton(
                    null,
                    new ReturnFromOptionsScreen(),
                    new Rectangle(0, 0, 574, 60),
                    game.Content.Load<Texture2D>("exit"),
                    game.Content.Load<Texture2D>("exitclicked"),
                    game.Content.Load<Texture2D>("exit"),
                    Color.White,
                    null,
                    Shape.RECTANGULAR,
                    SpriteBatch,
                    Game)
                    , Alignment.BOTTOM_CENTER);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleMouseInput((Game as MoodSwing).OldMouseState, false);
            HandleKeyboardInput((Game as MoodSwing).OldKeyboardState);
        }
    }
}
