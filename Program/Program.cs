using System;

namespace Program
{

    class Program
    {
        public static void Main()
        {
            GameLogic game = new GameLogic();
            game.Run();

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

        }
    }
}