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

using MoodSwingGUI;
using MoodSwingCoreComponents;
using System.IO;

namespace MoodSwingGame
{
    public class OpenCityScreen : ChangeScreen
    {
        private static OpenCityScreen instance;
        public static OpenCityScreen GetInstance()
        {
            if (instance == null)
                instance = new OpenCityScreen();
            return instance;
        }
        private OpenCityScreen() { }

        public override void PerformAction(Game game)
        {
            try
            {
                StreamReader sr = new StreamReader("small_save.txt");
                String s = sr.ReadLine();
                //System.Console.WriteLine(s);
                if (s != null && s.Equals("savefile"))
                {
                    new OpenDistrictScreen(MSDistrictScreen.DistrictName.FEAR).PerformAction(game);
                    sr.Close();
                    return;
                }
                sr.Close();
            }
            catch (FileNotFoundException)
            {
            }

            try
            {
                StreamReader sr = new StreamReader("medium_save.txt");
                String s = sr.ReadLine();
                //System.Console.WriteLine(s);
                if (s != null && s.Equals("savefile"))
                {
                    new OpenDistrictScreen(MSDistrictScreen.DistrictName.SADNESS).PerformAction(game);
                    sr.Close();
                    return;
                }
                sr.Close();
            }
            catch (FileNotFoundException)
            {
            }

            try
            {
                StreamReader sr = new StreamReader("large_save.txt");
                String s = sr.ReadLine();
                //System.Console.WriteLine(s);
                if (s != null && s.Equals("savefile"))
                {
                    new OpenDistrictScreen(MSDistrictScreen.DistrictName.ANGER).PerformAction(game);
                    sr.Close();
                    return;
                }
                sr.Close();
            }
            catch (FileNotFoundException)
            {
            }

            
            base.ChangeToScreen(MSCityScreen.getInstance(), game);
        }
    }
}
