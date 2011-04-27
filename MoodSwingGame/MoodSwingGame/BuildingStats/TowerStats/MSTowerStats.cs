using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoodSwingGame
{
    public abstract class MSTowerStats : MSBuildingStats
    {
        protected bool povertyEffectiveness;
        protected bool educationEffectiveness;
        protected bool genderEqualityEffectiveness;
        protected bool childHealthEffectiveness;
        protected bool maternalHealthEffectiveness;
        protected bool hivAidsEffectiveness;
        protected bool environmentEffectiveness;
        protected bool globalEffectiveness;

        public virtual int GetVolunteerCost() { return 1; }
        public virtual int GetFundsCost() { return 0; }
        public virtual int GetRange() { return 2; }
        public virtual float GetBuildTime() { return 5; }
        
        public bool GetEffectiveness(MSMilleniumDevelopmentGoal type)
        {
            switch (type)
            {
                case MSMilleniumDevelopmentGoal.POVERTY:
                    return povertyEffectiveness;
                case MSMilleniumDevelopmentGoal.EDUCATION:
                    return educationEffectiveness;
                case MSMilleniumDevelopmentGoal.GENDER_EQUALITY:
                    return genderEqualityEffectiveness;
                case MSMilleniumDevelopmentGoal.CHILD_HEALTH:
                    return childHealthEffectiveness;
                case MSMilleniumDevelopmentGoal.MATERNAL_HEALTH:
                    return maternalHealthEffectiveness;
                case MSMilleniumDevelopmentGoal.HIV_AIDS:
                    return hivAidsEffectiveness;
                case MSMilleniumDevelopmentGoal.ENVIRONMENT:
                    return environmentEffectiveness;
                case MSMilleniumDevelopmentGoal.GLOBAL_PARTNERSHIP:
                    return globalEffectiveness;
            }
            throw new ArgumentException();
        }
    }
}
