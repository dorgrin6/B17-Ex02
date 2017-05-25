using System;
using System.Text;
namespace Program
{
    class UserInterface
    {
        GameLogic m_Logic = new GameLogic();

        internal enum eGameKeys : ushort
        {
            quitKey = 'Q',
            yesKey = 'Y',
            noKey = 'N'
        }

        // holds the current state of the game
        internal enum eRunState : ushort
        {
            Continue,
            Lost,
            Won,
            EndSession,
            EndGame
        }

        public void Run()
        {
            eRunState runState; // current game's runState
            ushort stepsTaken; // amount of steps taken

            // game loop
            do
            {
                Ex02.ConsoleUtils.Screen.Clear();
                runState = this.gameSession(out stepsTaken); // start a new game session
                this.PrintCurrentBoardStatus();

                if (runState == eRunState.Won)
                { 
                    Console.WriteLine("You guessed after {0} steps!", stepsTaken);
                }
                else if (runState == eRunState.Lost)
                {
                    Console.WriteLine("No more guesses allowed. You Lost.");
                }

                Console.WriteLine("Would you like to start a new game? (Y/N)");
                string input = Console.ReadLine();


                // TODO: input validation, how do we make those smarter?
                if (input == "N")
                {
                    Console.WriteLine("Goodbye!");
                    runState = eRunState.EndGame;
                }
            }
            while (runState != eRunState.EndGame);
        }

        /* gameSession: a single game session progress */
        private eRunState gameSession(out ushort o_StepsTaken)
        {
            eRunState runState = eRunState.Continue; // current game run state
            getGuessesAmount(); // get amount of guesses
            m_Logic.initiateGame();
            o_StepsTaken = 0;

            for (int i = 0; i < m_Logic.UserGuessesAmount && runState == eRunState.Continue; i++)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                PrintCurrentBoardStatus();
                runState = this.handleGuessInput(i);
                ++o_StepsTaken;
            }
            
            // check if user had too many steps
            if (runState == eRunState.Continue && o_StepsTaken == this.m_Logic.UserGuessesAmount)
            {
                runState = eRunState.Lost;
            }

            return runState;
        }

        private eRunState handleGuessInput(int i_BoardIndex)
        {
            eRunState result = eRunState.Continue;
            string userGuess = this.getUserGuess(); // get next user guess
          

            if (userGuess == ((char)eGameKeys.quitKey).ToString()) // user opted to quit
            {
                result = eRunState.EndSession;
            }
            else // keep going
            {
                BoardLine currentLine = this.m_Logic.Board[i_BoardIndex];

                // split guess by whitespaces and insert to board
                currentLine.UserGuess = userGuess.Replace(" ", string.Empty).ToCharArray();

                setExistingLettersInGuess(currentLine);

                if (hasUserWon(currentLine))
                {
                    result = eRunState.Won;
                }
            }

            return result;
        }

        private bool hasUserWon(BoardLine CurrentBoardLine)
        {
            return (CurrentBoardLine.ExistRightPlaceResult == this.m_Logic.GuessArraySize);
        }


        private void setExistingLettersInGuess(BoardLine CurrentBoardLine)
        {
            ushort rightPlaceCount;
            ushort wrongPlaceCount;

            // count right place and wrong place guesses
            this.m_Logic.CountLettersExistsInGuess(CurrentBoardLine.UserGuess, out rightPlaceCount, out wrongPlaceCount);

            // set results
            CurrentBoardLine.ExistRightPlaceResult = rightPlaceCount;
            CurrentBoardLine.ExistWrongPlaceResult = wrongPlaceCount;
        }


        // TODO: 2 almost duplicate input string methods here, how to bind them? would like function pointer as boolean: getGuessesAmount, getUserGuess

        private void getGuessesAmount()
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

        private string getUserGuess()
        {
            string userInput; // function's return value
            bool legalInput;

            do
            {
                Console.Write("Please type your next guess <");
                for (int i = 0; i < m_Logic.GuessArraySize; i++)
                {
                    Console.Write("{0}", (char)('A' + i) );
                    if (i < m_Logic.GuessArraySize - 1)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("> or '{0}' to quit", (char)eGameKeys.quitKey);

                userInput = Console.ReadLine();
                legalInput = this.IsLegalInput(userInput);

                if (!legalInput)
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            } while (!legalInput);

            return userInput;
        }


        /* Made to replace IsLegalInput if needed
        private bool inputHasSpaces(string i_Input)
        {
            bool result = true;

            for (int i = 1; i < i_Input.Length && result; i+=2)
            {
                if (i_Input[i] != ' ')
                {
                    result = false;
                }
            }

            return result;
        }
        */


        public bool IsLegalInput(string i_UserGuess)
        {
            bool result;
            bool hasSpaces = true;
            bool hasLegalLetters = true;

            bool isCorrectSize =
              (i_UserGuess.Length == m_Logic.GuessArraySize * 2 - 1);

            for (int i = 0; i < i_UserGuess.Length; i++)
            {
                if (m_Logic.isLetterLegal(i,i_UserGuess))
                {
                    i++;
                }
                else
                {
                    hasLegalLetters = false;
                    break;
                }

                if (i < i_UserGuess.Length)
                {
                    if (i_UserGuess[i] != ' ')
                    {
                        hasSpaces = false;
                        break;
                    }
                }
            }

            result = (i_UserGuess == ((char)eGameKeys.quitKey).ToString()) || 
                (hasLegalLetters && hasSpaces && isCorrectSize && !m_Logic.hasDuplicateLetters(i_UserGuess));

            return result;
        }





        // Print methods

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

        private void insertBoardLine(StringBuilder i_Builder, int i_LineNum)
        {
            ushort barSize = calculateBarSize();
            BoardLine currentLine = this.m_Logic.Board[i_LineNum];

            ushort resultsAmount =
                      (ushort)(currentLine.ExistRightPlaceResult + currentLine.ExistWrongPlaceResult);

            i_Builder.Append("| ");

            for (int col = 0; col < m_Logic.GuessArraySize; col++)
            {
                i_Builder.Append(m_Logic.Board[i_LineNum][col]);
                Console.Write(' ');

                // boardPrint.Append(' ');
            }
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
            ushort resultsAmount = 
                (ushort)(m_Logic.Board[line].ExistRightPlaceResult + m_Logic.Board[line].ExistWrongPlaceResult);

            Console.Write("| ");


            for (int i = 0; i < m_Logic.GuessArraySize; i++)
            {
                printGuess(line, i);
                Console.Write(' ');
            }
            Console.Write('|');


            for (int i = 0; i<m_Logic.Board[line].ExistRightPlaceResult; i++)
            {
                Console.Write("{0} ", (char)BoardLine.eResultLetter.ExistRightPlace);
            }
            for (int i = 0; i<m_Logic.Board[line].ExistWrongPlaceResult; i++)
            {
                Console.Write("{0} ", (char)BoardLine.eResultLetter.ExistWrongPlace);
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
