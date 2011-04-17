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
    public class MSTower : MSChangeableBuilding
    {
        //Incorporate Texture, Model, Effect, and height into TowerStats later
        public MSTowerStats Stats
        {
            set;
            get;
        }

        private int capacity;

        public MSTower( Model model, Texture2D texture, Effect effect, Vector3 position, float rotation, int row, int column, int height, MSTowerStats stats)
            : base(model, texture, effect, position, rotation, row, column, height)
        {
            this.Stats = stats;
            capacity = stats.GetVolunteerCost();
        }

        public void sentinel(MSMap map, MSUnitHandler unitHandler)
        {
            if (capacity > 0)
            {
                for (int i = 0; i < unitHandler.Units.Count; i++)
                {
                    MSUnit unit = unitHandler.Units[i];
                    if (unit is MSMobber)
                    {
                        Vector2 position1 = new Vector2
                        (
                            position.X + MSMap.tileDimension / 2,
                            position.Y + MSMap.tileDimension / 2
                        );

                        Vector2 position2 = new Vector2
                        (
                            unit.Position.X,
                            unit.Position.Y
                        );

                        Vector2 tileCoords = unit.TileCoordinate;
                        int distance = Math.Abs(Row - (int)tileCoords.X) + Math.Abs(Column - (int)tileCoords.Y);
                        MS3DTile tile = (unit as MSMobber).Map.MapArray[(int)tileCoords.X, (int)tileCoords.Y];
                        if (tile is MSRoad &&
                            distance <= Stats.GetRange())
                        {
                            MSMilleniumDevelopmentGoal goal = (unit as MSMobber).Concern;
                            if (Stats.GetEffectiveness(goal) == true)
                            {
                                capacity--;

                                unitHandler.Units[i] = new MSCitizen
                                (
                                    unit.Position,
                                    unit.Path,
                                    unit.Map,
                                    false
                                );

                                unit = unitHandler.Units[i];
                                unit.IsStopped = true;

                                MSUnitHandler.GetInstance().SendVolunteer(map, unit, this);
                                MSMoodManager.GetInstance().TakeHealth();
                                MSMoodManager.GetInstance().AddMDGScore(goal);
                                
                            }
                        }
                    }
                }
            }
        }

        public void VolunteerReturned()
        {
            capacity++;
        }
    }
}
