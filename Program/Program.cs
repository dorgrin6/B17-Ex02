using System;

namespace Program
{

    class Program
    {
        public static void Main()
        {
           UserInterface GameInterface = new UserInterface();
            GameInterface.Run();

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

        }
    }
}