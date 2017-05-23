using System;
using System.Collections.Generic;
namespace Program
{
    class UserInterface
    {
        GameLogic m_Logic = new GameLogic();

        public void Run()
        {
            // get amount of guesses
            GetGuessesAmount();

            Ex02.ConsoleUtils.Screen.Clear();
            //getValuesToGuess();

            PrintCurrentBoardStatus();

            // set GuessBoard and ResultBoard
           // this.m_GuessesBoard = new char[m_userGuessNum, k_GuessArraySize];
            //m_ResultsBoard = new char[m_userGuessNum, k_GuessArraySize];
        }


        public static void PrintCurrentBoardStatus()
        {

            Console.WriteLine("Current board status:");
            Console.WriteLine("|Pins:\t|Result:|");
            Console.WriteLine(" A C B D\t|X X X\t|");
        }

        public static void PrintArray<T>(List<T> i_ToPrint)
        {
            foreach (T element in i_ToPrint)
            {
                Console.Write("{0} ",element.ToString());
            }
            Console.WriteLine();
        }

        public void GetGuessesAmount()
        {
            bool legalInput = true;

            ushort guessesAmount;
            
            ushort minGuessBound = (ushort)GameLogic.eGuessAmountBounds.MinGuessNum;
            ushort maxGuessBound = (ushort)GameLogic.eGuessAmountBounds.MaxGuessNum;

            do
            {
                Console.WriteLine(
                    "Please enter the maximum amount of guesses you wish ({0} - {1})",
                  minGuessBound, maxGuessBound);

                string userInput = Console.ReadLine();

                legalInput = ushort.TryParse(userInput, out guessesAmount) &&
                    (guessesAmount >= minGuessBound && guessesAmount <= maxGuessBound);


                if (!legalInput)
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            }
            while (!legalInput);

            // finally, change guesses amount
            m_Logic.UserGuessesAmount = guessesAmount;
        }
    }
}
