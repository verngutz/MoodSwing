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

namespace MoodSwingGUI
{
    public class MSTabbedPanel : MSGUITypable
    {
        public MSTabbedPanel(SpriteBatch spritebatch, Game game)
            : base(new Rectangle(), null, Shape.RECTANGULAR, spritebatch, game)
        {
        }
        private MSRadioButtonGroup activatorGroup;
        private Dictionary<MSCheckbox, MSPanel> activatorPanelPairs;

        public override void Hover() { }
        public override void LeftClick() { }
        public override void MiddleClick() { }
        public override void RightClick() { }
        public override void UnHover() { }
        public override void UnLeftClick() { }
        public override void UnMiddleClick() { }
        public override void UnRightClick() { }
        public override void HandleKeyboardInput(KeyboardState oldKeyboardState) { }
    }
}
