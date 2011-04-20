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
    public class MSMobber : MSUnit
    {
        protected override Model Model
        {
            get { return Game.Content.Load<Model>("personBump"); }
        }

        protected override Effect Effect
        {
            get { return Game.Content.Load<Effect>("Mood"); }
        }

        protected override Texture2D Texture
        {
            get { return Game.Content.Load<Texture2D>("MTextures/tao"); }
        }

        protected override float Speed
        {
            get { return 0.3f; }
        }

        private MSMilleniumDevelopmentGoal concern;
        public MSMilleniumDevelopmentGoal Concern { get { return concern; } }

        private MSImageHolder moodFace;
        public MSImageHolder MoodFace { get { return moodFace; } }

        public MSMobber(Vector3 position, Node path, MSMap map, MSMilleniumDevelopmentGoal concern)
            : base(position, path, map, false)
        {
            this.concern = concern;
            string moodFaceAssetName = "";
            switch (concern)
            {
                case MSMilleniumDevelopmentGoal.POVERTY:
                    moodFaceAssetName = "Moods/Hunger";
                    break;
                case MSMilleniumDevelopmentGoal.EDUCATION:
                    moodFaceAssetName = "Moods/Education";
                    break;
                case MSMilleniumDevelopmentGoal.GENDER_EQUALITY:
                    moodFaceAssetName = "Moods/Equality";
                    break;
                case MSMilleniumDevelopmentGoal.CHILD_HEALTH:
                    moodFaceAssetName = "Moods/Children";
                    break;
                case MSMilleniumDevelopmentGoal.MATERNAL_HEALTH:
                    moodFaceAssetName = "Moods/Maternity";
                    break;
                case MSMilleniumDevelopmentGoal.HIV_AIDS:
                    moodFaceAssetName = "Moods/Health";
                    break;
                case MSMilleniumDevelopmentGoal.ENVIRONMENT:
                    moodFaceAssetName = "Moods/Environment";
                    break;
                case MSMilleniumDevelopmentGoal.GLOBAL_PARTNERSHIP:
                    moodFaceAssetName = "Moods/Partnership";
                    break;
            }

            Vector3 screenProjection = Game.GraphicsDevice.Viewport.Project(Position, MSCamera.GetInstance().ProjectionMatrix, MSCamera.GetInstance().GetView(), Matrix.Identity);
            moodFace = new MSImageHolder(new Rectangle((int)screenProjection.X - 50, (int)screenProjection.Y - 50, 50, 50), Game.Content.Load<Texture2D>(moodFaceAssetName), MoodSwing.GetInstance().SpriteBatch, Game);
        }

        public override void Walk(MS3DTile[,] map_array)
        {
            base.Walk(map_array);

            if(DestinationReached)
                MSMoodManager.GetInstance().TakeDamage();

            Vector3 screenProjection = Game.GraphicsDevice.Viewport.Project(Position, MSCamera.GetInstance().ProjectionMatrix, MSCamera.GetInstance().GetView(), Matrix.Identity);
            moodFace.Position = new Vector2(screenProjection.X - 50, screenProjection.Y - 50);
        }
    }
}
