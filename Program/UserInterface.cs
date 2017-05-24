using System;
using System.Collections.Generic;
namespace Program
{
    using System.Text;

    class UserInterface
    {

        GameLogic m_Logic = new GameLogic();

        internal enum eGameKeys
        {
            quitKey = 'Q',
            yesKey = 'Y',
            noKey = 'N'
        }

        public void Run()
        {
            GetGuessesAmount();
            m_Logic.initiateGame();

            for (int i = 0; i < m_Logic.UserGuessesAmount; i++)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                PrintCurrentBoardStatus();
                GetUserGuess(i);
            }

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

        public void GetUserGuess(int index)
        {
            bool legalInput;
            string userInput;

            do
            {
                Console.Write("Please type your next guess <");
                for (int i = 0; i < m_Logic.GuessArraySize; i++)
                {
                    Console.Write("{0}", 'A' + i);
                    if (i < m_Logic.GuessArraySize - 1)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("> or '{0}' to quit", eGameKeys.quitKey);

                userInput = Console.ReadLine();
                legalInput = CheckIfLegal(userInput);

                if (!(legalInput))
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            } while (!(legalInput));
        }

        public bool CheckIfLegal(string guess)
        {
            bool legalInput = true;

            // checks if the string is too short or too long
            if ((guess.Length != m_Logic.GuessArraySize * 2 - 1) && legalInput)
            {
                legalInput = false;
            }

            // checks if each letter is legal logically and if they are seperated by spaces
            for (int i = 0; i < guess.Length; i++)
            {
                if (m_Logic.CheckIfLetterLegal(guess[i]))
                {
                    i++;
                }
                else
                {
                    legalInput = false;
                    break;
                }

                if (i < guess.Length)
                {
                    if (guess[i] != ' ')
                    {
                        legalInput = false;
                        break;
                    }
                }
            }

            return legalInput;
        }

        public void PrintCurrentBoardStatus()
        {
            ushort barSize = calculateBarSize();
            string pinsString = "Pins:";
            string resultsString = "Results:";

            Console.WriteLine("Current board status:{0}",System.Environment.NewLine);

            Console.Write("|{0}", pinsString);
            printDuplicateChar(' ', (ushort)(barSize - pinsString.Length));

            Console.Write("|{0}", resultsString);
            printDuplicateChar(' ', (ushort)((barSize-1) - resultsString.Length));


            // TODO: we need to print here the # # # #, but we should do it more efficient

            Console.Write(System.Environment.NewLine);
           
            printBoard();

       
        }

        public void printBoard()
        {
            for(int i = 0; i < m_Logic.UserGuessesAmount; i++)
            {
                printBoardLine(i);
            }
        }
 
        public void printBoardLine(int line)
        {
            ushort barSize = calculateBarSize();
            ushort resultsAmount = (ushort)(m_Logic.Board[line].ExistRightPlaceResult + m_Logic.Board[line].ExistWrongPlaceResult);

            Console.Write("| ");
            for (int i = 0; i < m_Logic.GuessArraySize; i++)
            {
                printGuess(line, i);
                Console.Write(' ');
            }
            Console.Write('|');

            // TODO: duplication of code!!!!!!!!!!!!!!!!!
            for (int i = 0; i<m_Logic.Board[line].ExistRightPlaceResult; i++)
            {
                Console.Write("{0} ", BoardLine.eResultLetter.ExistRightPlace);
            }
            for (int i = 0; i<m_Logic.Board[line].ExistWrongPlaceResult; i++)
            {
                Console.Write("{0} ", BoardLine.eResultLetter.ExistWrongPlace);
            }

            printDuplicateChar(' ', (ushort)((barSize-1) - resultsAmount * 2));
            Console.Write("|{0}",System.Environment.NewLine);
            printBorder(barSize);
        }

        public void printGuess(int line, int col)
        {
            Console.Write("{0}", m_Logic.Board[line][col]);
        }

        public void printBorder(ushort barSize)
        {
            Console.Write('|');
            printDuplicateChar('=', barSize);
            Console.Write('|');
            printDuplicateChar('=', (ushort)(barSize - 1));
            Console.WriteLine('|');
        }

        private ushort calculateBarSize()
        {
            return (ushort)(m_Logic.GuessArraySize * 2 + 1);
        }

        private void printDuplicateChar(char ch, ushort repeats)
        {
            for (int i = 0; i < repeats; i++)
            {
                Console.Write(ch);
            }
        }
    }
}
