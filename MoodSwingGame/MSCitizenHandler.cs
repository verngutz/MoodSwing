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
    public class MSCitizenHandler
    {
        private static MSCitizenHandler citizenHandler = null;

        public static MSCitizenHandler getInstance()
        {
            if (citizenHandler == null)
                citizenHandler = new MSCitizenHandler();
            return citizenHandler;
        }

        private const int PROBABILITY = 300;
        private List<MSCitizen> census;
        private Random random;

        private MSCitizenHandler() 
        {
            census = new List<MSCitizen>();
            random = new Random();
        }

        private bool oneOnly = false;
        private bool checkOne = false;
        public MSCitizen TryForBaby( MSMap map )
        {
            int rnd = random.Next(5000);

            if (oneOnly && checkOne)
                return null;
            if (rnd < PROBABILITY)
            {
                checkOne = true;
                Vector2 start = map.GetRandomBuilding();
                Vector2 end = map.GetRandomBuilding();
                Node path = map.GetPath(start, end);

                MSCitizen person =  new MSCitizen(MoodSwing.getInstance().Content.Load<Model>("person"), 
                    (map.MapArray[(int)start.X, (int)start.Y] as MS3DComponent).Position + new Vector3(0, 0, 20),
                    path);

                census.Add(person);
                return person;
            }

            return null;
        }

        public List<MS3DComponent> Update(MSMap map)
        {
            List<MS3DComponent> list = new List<MS3DComponent>();

            foreach (MSCitizen person in census)
            {
                if( !person.IsThere )
                    person.Walk(map.MapArray);
                else
                    list.Add(person);
            }

            foreach (MSCitizen person in list)
            {
                census.Remove(person);
            }
            return list;
        }

        
        
    }
}
