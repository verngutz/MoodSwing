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
        public void increaseCapacity(int i)
        {
            capacity += i;
        }
        private List<MSUnit> targetList;
        public void remove(MSUnit unit)
        {
            targetList.Remove(unit);
        }

        public MSTower(String model, String texture, String effect, Vector3 position, float rotation, int row, int column, int height, MSTowerStats stats)
            : base(model, texture, effect, position, rotation, row, column, height)
        {
            this.Stats = stats;
            if( stats != null ) capacity = stats.GetVolunteerCost();
            this.targetList = new List<MSUnit>();
            System.Console.WriteLine(capacity);
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
                            break;
                        }
                    }
                }
            }
        }

        public void VolunteerReturned()
        {
            capacity++;
        }

        public override void load(System.IO.StreamReader sr)
        {
            
            base.load(sr);
            string stat = sr.ReadLine();
            if (stat.Equals("MSAntiretroviralCenterStats"))
            {
                this.Stats = MSAntiretroviralCenterStats.GetInstance();
            }
            else if (stat.Equals("MSApartmentStats"))
            {
                this.Stats = MSApartmentStats.GetInstance();
            }
            else if (stat.Equals("MSBedNetsCenterStats"))
            {
                this.Stats = MSBedNetsCenterStats.GetInstance();
            }
            else if (stat.Equals("MSChildrensHospitalStats"))
            {
                this.Stats = MSChildrensHospitalStats.GetInstance();
            }
            else if (stat.Equals("MSChildrensLibraryStats"))
            {
                this.Stats = MSChildrensLibraryStats.GetInstance();
            }
            else if (stat.Equals("MSEFASchoolStats"))
            {
                this.Stats = MSEFASchoolStats.GetInstance();
            }
            else if (stat.Equals("MSEmploymentCenterStats"))
            {
                this.Stats = MSEmploymentCenterStats.GetInstance();
            }
            else if (stat.Equals("MSEnvironmentalCenterStats"))
            {
                this.Stats = MSEnvironmentalCenterStats.GetInstance();
            }
            else if (stat.Equals("MSEpidemicsClinicStats"))
            {
                this.Stats = MSEpidemicsClinicStats.GetInstance();
            }
            else if (stat.Equals("MSFistulaTreatmentCenterStats"))
            {
                this.Stats = MSFistulaTreatmentCenterStats.GetInstance();
            }
            else if (stat.Equals("MSGeneralClinicStats"))
            {
                this.Stats = MSGeneralClinicStats.GetInstance();
            }
            else if (stat.Equals("MSGeneralRefugeStats"))
            {
                this.Stats = MSGeneralRefugeStats.GetInstance();
            }
            else if (stat.Equals("MSImmunizationOutreachStats"))
            {
                this.Stats = MSImmunizationOutreachStats.GetInstance();
            }
            else if (stat.Equals("MSInternationalCenterStats"))
            {
                this.Stats = MSInternationalCenterStats.GetInstance();
            }
            else if (stat.Equals("MSMaternalCareCenterStats"))
            {
                this.Stats = MSMaternalCareCenterStats.GetInstance();
            }
            else if (stat.Equals("MSPovertyRefugeStats"))
            {
                this.Stats = MSPovertyRefugeStats.GetInstance();
            }
            else if (stat.Equals("MSPublicAssistanceCenterStats"))
            {
                this.Stats = MSPublicAssistanceCenterStats.GetInstance();
            }
            else if (stat.Equals("MSSuppliesDonationCenterStats"))
            {
                this.Stats = MSSuppliesDonationCenterStats.GetInstance();
            }
            else if (stat.Equals("MSTechnoFarmhouseStats"))
            {
                this.Stats = MSTechnoFarmhouseStats.GetInstance();
            }
            else if (stat.Equals("MSTrashToCashCenterStats"))
            {
                this.Stats = MSTrashToCashCenterStats.GetInstance();
            }
            else if (stat.Equals("MSTutorialCenterStats"))
            {
                this.Stats = MSTutorialCenterStats.GetInstance();
            }
            else if (stat.Equals("MSVaccinationCenterStats"))
            {
                this.Stats = MSVaccinationCenterStats.GetInstance();
            }
            else if (stat.Equals("MSWaterTreatmentPlantStats"))
            {
                this.Stats = MSWaterTreatmentPlantStats.GetInstance();
            }
            else if (stat.Equals("MSWhiteScreensCenterStats"))
            {
                this.Stats = MSWhiteScreensCenterStats.GetInstance();
            }
            else if (stat.Equals("MSWomenCenterStats"))
            {
                this.Stats = MSWomenCenterStats.GetInstance();
            }
            else if (stat.Equals("MSWomenPoliticiansCenterStats"))
            {
                this.Stats = MSWomenPoliticiansCenterStats.GetInstance();
            }
            else if (stat.Equals("MSWomensHealthCenterStats"))
            {
                this.Stats = MSWomensHealthCenterStats.GetInstance();
            }
            else if (stat.Equals("MSWomensUniversityStats"))
            {
                this.Stats = MSWomensUniversityStats.GetInstance();
            }
            else if (stat.Equals("MSWorldWithoutBoundariesStats"))
            {
                this.Stats = MSWorldWithoutBoundariesStats.GetInstance();
            }

            capacity = Int32.Parse(sr.ReadLine());
            System.Console.WriteLine(capacity);
        }
        public override string toString()
        {
            String toReturn = "MSTower\n";
            toReturn += base.toString();
            if (Stats is MSAntiretroviralCenterStats)
            {
                toReturn += "MSAntiretroviralCenterStats";
            }
            else if (Stats is MSApartmentStats)
            {
                toReturn += "MSApartmentStats";
            }
            else if (Stats is MSBedNetsCenterStats)
            {
                toReturn += "MSBedNetsCenterStats";
            }
            else if (Stats is MSChildrensHospitalStats)
            {
                toReturn += "MSChildrensHospitalStats";
            }
            else if (Stats is MSChildrensLibraryStats)
            {
                toReturn += "MSChildrensLibraryStats";
            }
            else if (Stats is MSEFASchoolStats)
            {
                toReturn += "MSEFASchoolStats";
            }
            else if (Stats is MSEmploymentCenterStats)
            {
                toReturn += "MSEmploymentCenterStats";
            }
            else if (Stats is MSEnvironmentalCenterStats)
            {
                toReturn += "MSEnvironmentalCenterStats";
            }
            else if (Stats is MSEpidemicsClinicStats)
            {
                toReturn += "MSEpidemicsClinicStats";
            }
            else if (Stats is MSFistulaTreatmentCenterStats)
            {
                toReturn += "MSFistulaTreatmentCenterStats";
            }
            else if (Stats is MSGeneralClinicStats)
            {
                toReturn += "MSGeneralClinicStats";
            }
            else if (Stats is MSGeneralRefugeStats)
            {
                toReturn += "MSGeneralRefugeStats";
            }
            else if (Stats is MSImmunizationOutreachStats)
            {
                toReturn += "MSImmunizationOutreachStats";
            }
            else if (Stats is MSInternationalCenterStats)
            {
                toReturn += "MSInternationalCenterStats";
            }
            else if (Stats is MSMaternalCareCenterStats)
            {
                toReturn += "MSMaternalCareCenterStats";
            }
            else if (Stats is MSPovertyRefugeStats)
            {
                toReturn += "MSPovertyRefugeStats";
            }
            else if (Stats is MSPublicAssistanceCenterStats)
            {
                toReturn += "MSPublicAssistanceCenterStats";
            }
            else if (Stats is MSSuppliesDonationCenterStats)
            {
                toReturn += "MSSuppliesDonationCenterStats";
            }
            else if (Stats is MSTechnoFarmhouseStats)
            {
                toReturn += "MSTechnoFarmhouseStats";
            }
            else if (Stats is MSTrashToCashCenterStats)
            {
                toReturn += "MSTrashToCashCenterStats";
            }
            else if (Stats is MSTutorialCenterStats)
            {
                toReturn += "MSTutorialCenterStats";
            }
            else if (Stats is MSVaccinationCenterStats)
            {
                toReturn += "MSVaccinationCenterStats";
            }
            else if (Stats is MSWaterTreatmentPlantStats)
            {
                toReturn += "MSWaterTreatmentPlantStats";
            }
            else if (Stats is MSWhiteScreensCenterStats)
            {
                toReturn += "MSWhiteScreensCenterStats";
            }
            else if (Stats is MSWomenCenterStats)
            {
                toReturn += "MSWomenCenterStats";
            }
            else if (Stats is MSWomenPoliticiansCenterStats)
            {
                toReturn += "MSWomenPoliticiansCenterStats";
            }
            else if (Stats is MSWomensHealthCenterStats)
            {
                toReturn += "MSWomensHealthCenterStats";
            }
            else if (Stats is MSWomensUniversityStats)
            {
                toReturn += "MSWomensUniversityStats";
            }
            else if (Stats is MSWorldWithoutBoundariesStats)
            {
                toReturn += "MSWorldWithoutBoundariesStats";
            }
            toReturn += "\n";
            toReturn += (capacity + targetList.Count) + "\n";
            
            return toReturn;
        }
    }
}
