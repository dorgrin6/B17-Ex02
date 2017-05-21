using System;

namespace Program
{
    class UserInterface
    {
        private static ushort m_userGuessNum = 0; // TODO: check name convention
        
        private enum eGuessBoundaries
        {
            MinGuessNum = 4,
            MaxGuessNum = 10
        }

        public static void ProgramFlow()
        {
            GetGuessesAmount();
            
            Ex02.ConsoleUtils.Screen.Clear();


        }

        public static void GetGuessesAmount()
        {
            bool legalInput = true;
            ushort guesses;
            do
            {
                Console.WriteLine(
                    "Please enter the maximum amount of guesses you wish ({0} - {1})",
                   (ushort)eGuessBoundaries.MinGuessNum, (ushort)eGuessBoundaries.MaxGuessNum);

                string userInput = Console.ReadLine();

                legalInput = ushort.TryParse(userInput, out guesses) &&
                    guesses >= (ushort)eGuessBoundaries.MinGuessNum && guesses <= (ushort)eGuessBoundaries.MinGuessNum;


                if (!legalInput)
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            }
            while (!legalInput);

            // finally, insert legal guesses amount to member
            m_userGuessNum = guesses;
        }
    }
}
