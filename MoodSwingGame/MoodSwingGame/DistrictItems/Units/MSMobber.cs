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
        private List<MSMobber> mobList;
        public List<MSMobber> GetMobList() { return mobList; }
        public void AddMember(MSMobber m) { mobList.Add(m); }

        public override void Follow(MSUnit unit)
        {
            base.Follow(unit);
            MSMobber mobber = unit as MSMobber;
            mobber.AddMember(this);
            foreach (MSMobber m in mobber.GetMobList())
            {
                mobList.Add(m);
            }
        }

        protected override Model Model
        {
            get { return Game.Content.Load<Model>("personBump"); }
        }

        protected override Texture2D Texture
        {
            get { return Game.Content.Load<Texture2D>("MTextures/tao"); }
        }

        private float speed = 0.3f;
        public override float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private MSMilleniumDevelopmentGoal concern;
        public MSMilleniumDevelopmentGoal Concern { get { return concern; } }

        private MSImageHolder moodFace;
        public MSImageHolder MoodFace { get { return moodFace; } }

        public MSMobber(Vector3 position, Node path, MSMap map, MSMilleniumDevelopmentGoal concern)
            : base(position, path, map, false)
        {
            mobList = new List<MSMobber>();
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


        private float minFollowDistance = 25;
        private float maxFollowDistance = 45;
        public override void Walk(MS3DTile[,] mapArray, List<MSUnit> units )
        {
            if (!IsStopped)
            {
                Vector2 pos = new Vector2(Position.X, Position.Y);
                Vector2 toFollowVector = Vector2.Zero;

                //looks for a Mobber to follow
                foreach( MSMobber mobber in this.mobList ) 
                {
                    Vector2 mobPos = new Vector2(mobber.Position.X, mobber.Position.Y);
                    float dist = Vector2.Distance(mobPos, pos);
                    if (dist < maxFollowDistance && dist > minFollowDistance )
                    {
                        float deltaAngle = Math.Abs(mobber.Rotation - this.Rotation);
                        if (deltaAngle <= MathHelper.ToRadians(10) &&
                            MSRandom.random.Next(4) == 1 )
                        {
                            toFollowVector = mobPos;
                        }
                    }
                    
                }

                if (destination == Vector2.Zero && toFollowVector == Vector2.Zero )
                {
                    Vector3 targetVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
                    if (path.next != null || path.parent != null)
                    {
                        destination = new Vector2(targetVector3.X + MSRandom.random.Next(MSMap.tileDimension / 2),
                                                      targetVector3.Y + MSRandom.random.Next(MSMap.tileDimension / 2));
                    }
                    else
                        destination = new Vector2(targetVector3.X, targetVector3.Y);

                    targetRotation = (float)Math.Atan2(destination.Y - position.Y, destination.X - position.X);

                }
                //change target to position of mobber to follow
                else if( toFollowVector != Vector2.Zero )
                {
                    destination = toFollowVector;
                    targetRotation = (float)Math.Atan2(destination.Y - position.Y, destination.X - position.X);

                }

                //destination reached
                if (Vector2.Distance(pos, destination) < 1)
                {
                    this.position = new Vector3(destination.X, destination.Y, position.Z);
                    Vector3 origDestVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
                    Vector2 origDestination = new Vector2(origDestVector3.X, origDestVector3.Y);
                    float toCheckAngle = (float)Math.Atan2(origDestination.X - position.X, origDestination.Y - position.Y);
                    float deltaToCheckAngle = Math.Abs(toCheckAngle - this.Rotation);

                    //check if original destination is on-route
                    if ( deltaToCheckAngle <= MathHelper.ToRadians(10) )
                    {
                        destination = origDestination;
                        targetRotation = toCheckAngle;
                    }
                    //if it entails going back, check next path instead.
                    else
                    {
                        if (path.next != null)
                        {
                            path = path.next;
                            Vector3 targetVector3 = (mapArray[(int)path.Position.X, (int)path.Position.Y] as MS3DTile).Position;
                            destination = new Vector2(targetVector3.X + MSRandom.random.Next(MSMap.tileDimension / 2) - MSMap.tileDimension / 4,
                                                          targetVector3.Y + MSRandom.random.Next(MSMap.tileDimension / 2) - MSMap.tileDimension / 4);

                            Vector2 direction = destination - new Vector2(position.X, position.Y);
                            float angle = (float)Math.Atan2(direction.Y, direction.X);
                            targetRotation = angle;
                        }
                        else destinationReached = true;
                    }
                    
                }
                else
                {
                    Vector2 unit = destination - pos;
                    unit = Vector2.Normalize(unit);
                    this.position += new Vector3(unit.X * Speed, unit.Y * Speed, 0);
                }


                //smooth rotation
                if (Math.Abs(targetRotation - Rotation) > 0.01)
                {
                    float delta = targetRotation - Rotation;
                    while (delta < -MathHelper.Pi)
                    {
                        delta += MathHelper.TwoPi;
                    }
                    while (delta > MathHelper.Pi)
                    {
                        delta -= MathHelper.TwoPi;
                    }
                    Rotation += delta * rotationSpeed;
                }

                adjustWorldMatrix();

            }

            //base.Walk(mapArray);
                
            if(DestinationReached)
                MSMoodManager.GetInstance().TakeDamage();

            Vector3 screenProjection = Game.GraphicsDevice.Viewport.Project(Position, MSCamera.GetInstance().ProjectionMatrix, MSCamera.GetInstance().GetView(), Matrix.Identity);
            moodFace.Position = new Vector2(screenProjection.X - 50, screenProjection.Y - 50);
        }
    }
}
