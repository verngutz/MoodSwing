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
    public class MSFundraiser : MSBuilding
    {
        public const int FUNDRAISING_DIFFICULTY = 20;

        private int fundraiseCounter;
        private MSResourceManager resourceManager;

        public MSFundraiser(String model, String texture, String effect, Vector3 position, float rotation, int row, int column, MSResourceManager resource_manager)
            : base(model, texture, effect, position, rotation, row, column, MSMap.tallheight) 
        {
            fundraiseCounter = 0;
            resourceManager = resource_manager;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (fundraiseCounter++ > FUNDRAISING_DIFFICULTY)
            {
                resourceManager.Funds++;
                fundraiseCounter = 0;
            }
        }
    }
}
