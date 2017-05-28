using System;
namespace Program
{
    

    class Program
    {
        public static void Main()
        {
           UserInterface gameInterface = new UserInterface();
            gameInterface.Run();

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

        }
    }
}