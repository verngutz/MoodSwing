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
            Model model = null;
            Texture2D texture = null;
            Effect effect = null;

            if (stats is MSAntiretroviralCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingGamot");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSApartmentStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingSabaw");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSBedNetsCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingGamot");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSChildrensHospitalStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingTeddy");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSChildrensLibraryStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingLapis");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSEFASchoolStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingLapis");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSEmploymentCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingSabaw");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSEnvironmentalCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingDahon");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSEpidemicsClinicStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingGamot");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSFistulaTreatmentCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingBuntis");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSGeneralClinicStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingGamot");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSGeneralRefugeStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingSabaw");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSImmunizationOutreachStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingTeddy");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSInternationalCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingTropa");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSMaternalCareCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingBuntis");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSPovertyRefugeStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingSabaw");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSPublicAssistanceCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingBigDefault");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSSuppliesDonationCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingLapis");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSTechnoFarmhouseStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingSabaw");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSTrashToCashCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingSabaw");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSTutorialCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingLapis");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSVaccinationCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingTeddy");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSWaterTreatmentPlantStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingDahon");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSWhiteScreensCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingCross");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSWomenCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingCross");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSWomenPoliticiansCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingCross");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSWomensHealthCenterStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingBuntis");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSWomensUniversityStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingCross");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }
            else if (stats is MSWorldWithoutBoundariesStats)
            {
                model = moodSwing.Content.Load<Model>("MModels/BuildingBig");
                texture = moodSwing.Content.Load<Texture2D>("MTextures/BuildingTropa");
                effect = moodSwing.Content.Load<Effect>("Mood");
            }

            return new MSTower(model, texture, effect, position, rotation, (int)tileCoords.X, (int)tileCoords.Y, MSMap.tallheight, stats);
                    
        }
    }
}
