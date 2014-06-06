using System;

namespace Test_Game
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TestGame game = new TestGame())
            {                
                game.Window.Title = "The Legend of Zelda!";
                game.Run();
            }
        }
    }
}

