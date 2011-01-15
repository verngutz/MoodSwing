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

        private MSDistrictScreen(MoodSwing game)
            : base(game.Content.Load<Texture2D>("DistrictView"), 150, 150, 150, 150, game.SpriteBatch, game) 
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            CheckMouseClick((Game as MoodSwing).OldMouseState);
            base.Update(gameTime);
        }
    }
}
