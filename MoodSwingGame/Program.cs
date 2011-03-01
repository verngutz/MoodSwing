using System;

namespace MoodSwingGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

            MoodSwing.GetInstance().Run();
        }
    }

}

