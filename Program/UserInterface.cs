using System;
using System.Collections.Generic;
namespace Program
{
    class UserInterface
    {

        public static void PrintArray<T>(List<T> i_ToPrint)
        {
            foreach (T element in i_ToPrint)
            {
                Console.Write("{0} ",element.ToString());
            }
            Console.WriteLine();
        }

        public static ushort GetGuessesAmount()
        {
            bool legalInput = true;

            ushort guesses;
            // TODO: use getters?
            ushort minGuessBound = (ushort)GameManager.eGuessAmountBounds.MinGuessNum;
            ushort maxGuessBound = (ushort)GameManager.eGuessAmountBounds.MaxGuessNum;


            do
            {
                Console.WriteLine(
                    "Please enter the maximum amount of guesses you wish ({0} - {1})",
                  minGuessBound, maxGuessBound);

                string userInput = Console.ReadLine();

                legalInput = ushort.TryParse(userInput, out guesses) &&
                    (guesses >= minGuessBound && guesses <= maxGuessBound);


                if (!legalInput)
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            }
            while (!legalInput);

            // finally, return guesse
            return guesses;
        }
    }
}
