﻿using System;
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
    public class MSCivilianHandler
    {
        private static MSCivilianHandler civilianHandler = null;

        public static MSCivilianHandler getInstance()
        {
            if (civilianHandler == null)
                civilianHandler = new MSCivilianHandler();
            return civilianHandler;
        }

        private const int PROBABILITY = 400;
        private List<MSPerson> census;
        private Random random;

        private MSCivilianHandler() 
        {
            census = new List<MSPerson>();
            random = new Random();
        }

        public MSPerson tryForBaby( MSMap map )
        {
            int rnd = random.Next(5000);
            if (rnd < 400)
            {
                Vector2 start = map.getRandomBuilding();
                Vector2 end = map.getRandomBuilding();
                Node path = map.getPath(start, end);

                if (path == null) 
                    System.Console.WriteLine("NULL!!!");
                MSPerson person =  new MSPerson(MoodSwing.getInstance().Content.Load<Model>("person"), 
                    (map.MapArray[(int)start.X, (int)start.Y] as MS3DComponent).Position + new Vector3(0, 0, 20),
                    path);

                census.Add(person);
                return person;
            }

            return null;
        }

        public List<MS3DComponent> update(MSMap map)
        {
            List<MS3DComponent> list = new List<MS3DComponent>();

            foreach (MSPerson person in census)
            {
                if( !person.IsThere )
                    person.walk(map.MapArray);
                else
                    list.Add(person);
            }

            foreach (MSPerson person in list)
            {
                System.Console.WriteLine("REMOVE");
                census.Remove(person);
            }
            return list;
        }

        
        
    }
}
