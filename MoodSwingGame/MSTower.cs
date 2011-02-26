using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MSTower : MSBuilding
    {
        private int capacity;
        private int range;
        private MSTowerStats stats;

        public MSTower( Model model, Texture2D texture, Effect effect, Vector3 position, int row, int column, MSTowerStats stats)
            : base(model, texture, effect, position, row, column)
        {
            capacity = 5;
            range = 50;
            this.stats = stats;
        }

        public MSVolunteer sentinel( MSMap map )
        {
            if (capacity > 0)
            {
                MSCitizen target = MSUnitHandler.GetInstance().GetTarget(Position, range);
                if (target != null)
                {
                    if (stats.GetEffectiveness(target.MDG) > MSRandom.random.Next(100))
                    {
                        capacity--;
                        target.state = MSCitizen.State.WAITING;

                        Node path1 = map.GetPath(new Vector2(Row, Column), target.TileCoordinate);
                        Node path2 = map.GetPath(target.TileCoordinate, new Vector2(Row, Column)).next;

                        MSVolunteer volunteer = new MSVolunteer(Game.Content.Load<Model>("person"),
                            Game.Content.Load<Texture2D>("MTextures/recruiter"),
                            Game.Content.Load<Effect>("Mood"),
                            Position + new Vector3(0, 0, 20), path1, path2, target as MSCitizen, this);
                        MSUnitHandler.GetInstance().AddVolunteer(volunteer);
                        MSMoodManager.GetInstance().takeHealth();
                        return volunteer;
                    }
                }
            }
            return null;
        }

        public void VolunteerReturned()
        {
            capacity++;
        }
    }

    public abstract class MSTowerStats
    {
        protected int povertyEffectiveness;
        protected int educationEffectiveness;
        protected int genderEqualityEffectiveness;
        protected int childHealthEffectiveness;
        protected int maternalHealthEffectiveness;
        protected int hivAidsEffectiveness;
        protected int environmentEffectiveness;
        protected int globalEffectiveness;

        public int GetEffectiveness(MSTypes type)
        {
            switch (type)
            {
                case MSTypes.POVERTY:
                    return povertyEffectiveness;
                case MSTypes.EDUCATION:
                    return educationEffectiveness;
                case MSTypes.GENDER_EQUALITY:
                    return genderEqualityEffectiveness;
                case MSTypes.CHILD_HEALTH:
                    return childHealthEffectiveness;
                case MSTypes.MATERNAL_HEALTH:
                    return maternalHealthEffectiveness;
                case MSTypes.HIV_AIDS:
                    return hivAidsEffectiveness;
                case MSTypes.ENVIRONMENT:
                    return environmentEffectiveness;
                case MSTypes.GLOBAL_PARTNERSHIP:
                    return globalEffectiveness;
            }
            throw new ArgumentException();
        }
    }

    public class MSFoodCenterStats : MSTowerStats
    {
        public static MSFoodCenterStats instance;
        public static MSFoodCenterStats GetInstance()
        {
            if (instance == null)
                instance = new MSFoodCenterStats();
            return instance;
        }

        private MSFoodCenterStats()
        {
            povertyEffectiveness = 100;
            educationEffectiveness = 2;
            genderEqualityEffectiveness = 2;
            childHealthEffectiveness = 2;
            maternalHealthEffectiveness = 2;
            hivAidsEffectiveness = 2;
            environmentEffectiveness = 2;
            globalEffectiveness = 2;
        }
    }
}
