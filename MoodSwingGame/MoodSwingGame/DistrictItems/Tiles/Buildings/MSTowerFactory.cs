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
    public class MSTowerFactory
    {
        public static MSTower CreateMSTower( MSTowerStats stats, Vector3 position, float rotation, Vector2 tileCoords )
        {
            MoodSwing moodSwing = MoodSwing.GetInstance();
            String model = "";
            String texture = "";
            String effect = "";

            if (stats is MSAntiretroviralCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingGamot";
                effect = "Mood";
            }
            else if (stats is MSApartmentStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingSabaw";
                effect = "Mood";
            }
            else if (stats is MSBedNetsCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingGamot";
                effect = "Mood";
            }
            else if (stats is MSChildrensHospitalStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/BuildingTeddy";
                effect = "Mood";
            }
            else if (stats is MSChildrensLibraryStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingLapis";
                effect = "Mood";
            }
            else if (stats is MSEFASchoolStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingLapis";
                effect = "Mood";
            }
            else if (stats is MSEmploymentCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingSabaw";
                effect = "Mood";
            }
            else if (stats is MSEnvironmentalCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/Environment";
                effect = "Mood";
            }
            else if (stats is MSEpidemicsClinicStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/BuildingGamot";
                effect = "Mood";
            }
            else if (stats is MSFistulaTreatmentCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingBuntis";
                effect = "Mood";
            }
            else if (stats is MSGeneralClinicStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/Clinic";
                effect = "Mood";
            }
            else if (stats is MSGeneralRefugeStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/Refuge";
                effect = "Mood";
            }
            else if (stats is MSImmunizationOutreachStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingTeddy";
                effect = "Mood";
            }
            else if (stats is MSInternationalCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/Global";
                effect = "Mood";
            }
            else if (stats is MSMaternalCareCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingBuntis";
                effect = "Mood";
            }
            else if (stats is MSPovertyRefugeStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/BuildingSabaw";
                effect = "Mood";
            }
            else if (stats is MSPublicAssistanceCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/BuildingBigDefault";
                effect = "Mood";
            }
            else if (stats is MSSuppliesDonationCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingLapis";
                effect = "Mood";
            }
            else if (stats is MSTechnoFarmhouseStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingSabaw";
                effect = "Mood";
            }
            else if (stats is MSTrashToCashCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingSabaw";
                effect = "Mood";
            }
            else if (stats is MSTutorialCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/BuildingLapis";
                effect = "Mood";
            }
            else if (stats is MSVaccinationCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingTeddy";
                effect = "Mood";
            }
            else if (stats is MSWaterTreatmentPlantStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingDahon";
                effect = "Mood";
            }
            else if (stats is MSWhiteScreensCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingCross";
                effect = "Mood";
            }
            else if (stats is MSWomenCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/BuildingCross";
                effect = "Mood";
            }
            else if (stats is MSWomenPoliticiansCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingCross";
                effect = "Mood";
            }
            else if (stats is MSWomensHealthCenterStats)
            {
                model = "MModels/BuildingBig";
                texture = "MTextures/BuildingBuntis";
                effect = "Mood";
            }
            else if (stats is MSWomensUniversityStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingCross";
                effect = "Mood";
            }
            else if (stats is MSWorldWithoutBoundariesStats)
            {
                model = "MModels/BuildingBig";
                texture = "Ulti/BuildingTropa";
                effect = "Mood";
            }

            return new MSTower(model, texture, effect, position, rotation, (int)tileCoords.X, (int)tileCoords.Y, MSMap.tallheight, stats);
                    
        }
    }
}
