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
        public MSTowerStats Stats { set; get; }

        private int capacity;
        private List<MSUnit> targetList;
        public void remove(MSUnit unit)
        {
            targetList.Remove(unit);
        }

        public MSTower( Model model, Texture2D texture, Effect effect, Vector3 position, float rotation, int row, int column, int height, MSTowerStats stats)
            : base(model, texture, effect, position, rotation, row, column, height)
        {
            this.Stats = stats;
            capacity = stats.GetVolunteerCost();
            this.targetList = new List<MSUnit>();
        }

        public void sentinel(MSMap map, MSUnitHandler unitHandler)
        {
            if (capacity > 0 && this.State == MSChangeableBuildingState.IDLE)
            {
                for (int i = 0; i < unitHandler.Units.Count; i++)
                {
                    MSUnit unit = unitHandler.Units[i];
                    if (unit is MSMobber && !targetList.Contains(unit) )
                    {
                        Vector2 targetTileCoords = unit.TileCoordinate;
                        //int distance = Math.Abs(Row - (int)targetTileCoords.X) + Math.Abs(Column - (int)targetTileCoords.Y);
                        MS3DTile tile = (unit as MSMobber).Map.MapArray[(int)targetTileCoords.X, (int)targetTileCoords.Y];

                        Node path = 
                            (MoodSwing.GetInstance().CurrentScreen as MSDistrictScreen).Map.GetPath(TileCoordinate, targetTileCoords);

                        int distance = 0;
                        while (path.next != null)
                        {
                            distance++;
                            path = path.next;
                        }

                        if (tile is MSRoad &&
                            distance <= Stats.GetRange())
                        {
                            MSMilleniumDevelopmentGoal goal = (unit as MSMobber).Concern;
                            if (Stats.GetEffectiveness(goal) == true)
                            {
                                capacity--;

                                /*unitHandler.Units[i] = new MSCitizen
                                (
                                    unit.Position,
                                    unit.Path,
                                    unit.Map,
                                    false,
                                    unit.Rotation
                                );

                                unit = unitHandler.Units[i];*/
                                //unit.IsStopped = true;

                                MSUnitHandler.GetInstance().SendVolunteer(map, unit, this);
                                targetList.Add(unit);
                                //MSMoodManager.GetInstance().TakeHealth();
                                //MSMoodManager.GetInstance().AddMDGScore(goal);
                                
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
