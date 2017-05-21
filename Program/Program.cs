using System;

namespace Program
{

    class Program
    {
        public static void Main()
        {
            GameManager game = new GameManager();
            game.Run();

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

        }
    }
}