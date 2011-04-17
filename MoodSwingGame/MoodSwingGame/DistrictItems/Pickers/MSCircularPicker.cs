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
    public abstract class MSCircularPicker : MSPanel
    {
        protected MSBuilding selected;
        protected Effect selectedEffect;

        public MSCircularPicker(Texture2D background, Rectangle boundingRectangle, MSBuilding selected, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, selected, Color.White, shape, spriteBatch, game) { }

        public MSCircularPicker(Texture2D background, Rectangle boundingRectangle, MSBuilding selected, Color highlight, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, 0, 0, 0, 0, selected, shape, spriteBatch, game) { }

        public MSCircularPicker(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, MSBuilding selected, Shape shape, SpriteBatch spriteBatch, Game game)
            : this(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, selected, Color.White, shape, spriteBatch, game) { }

        public MSCircularPicker(Texture2D background, Rectangle boundingRectangle, float topPadding, float bottomPadding, float leftPadding, float rightPadding, MSBuilding selected, Color highlight, Shape shape, SpriteBatch spriteBatch, Game game)
            : base(background, boundingRectangle, topPadding, bottomPadding, leftPadding, rightPadding, null, shape, spriteBatch, game)
        {
            this.selected = selected;
            selectedEffect = selected.Effect;
            selected.Effect = Game.Content.Load<Effect>("highlight");
        }

        public void UnhighlightSelected()
        {
            selected.Effect = selectedEffect;
        }
    }
}
