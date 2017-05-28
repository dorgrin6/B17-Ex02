﻿using System;
using System.Text;
namespace Program
{
    class UserInterface
    {
        private const string k_WordDelimiter = " ";


        GameLogic m_Logic = new GameLogic();

        internal enum eGameKeys : ushort
        {
            QuitKey = 'Q',
            YesKey = 'Y',
            NoKey = 'N'
        }

        // holds the current state of the game
        internal enum eRunState : ushort
        {
            Continue,
            Lost,
            Won,
            UserQuit,
            EndGame
        }

        // kind of input validation to do
        internal enum eInputValidation : ushort
        {
            UserGuess,
            UserGuessesAmount,
            ExitScreen
        }


        public void Run()
        {
            eRunState runState; // current game's runState
            ushort stepsTaken; // amount of steps taken in session

            // game loop
            do
            {
                Ex02.ConsoleUtils.Screen.Clear();
                runState = this.gameSession(out stepsTaken); // start a new game session
                Ex02.ConsoleUtils.Screen.Clear();
                PrintCurrentBoardStatus();
                handleEndSession(ref runState, stepsTaken);
            } while (runState != eRunState.EndGame);
        }

        private void handleEndSession(ref eRunState io_RunState, ushort i_StepsTaken)
        {
            if (io_RunState == eRunState.Won)
            {
                Console.WriteLine("You guessed after {0} steps!", i_StepsTaken);
            }
            else if (io_RunState == eRunState.Lost)
            {
                Console.WriteLine("No more guesses allowed. You Lost.");
            }
            string userMessage = String.Format(
                "Would you like to start a new game? ({0}/{1})",
                (char)eGameKeys.YesKey,
                (char)eGameKeys.NoKey);

            string input = getUserInput(eInputValidation.ExitScreen, userMessage);

            if (input == ((char)eGameKeys.NoKey).ToString())
            {
                io_RunState = eRunState.EndGame;
                Console.WriteLine("Goodbye!");
            }
        }

        private string getUserInput(eInputValidation i_InputKind, string i_UserMessage)
        {
            string userInput;
            bool isLegalInput = false;

            do
            {
                Console.WriteLine(i_UserMessage);
                userInput = Console.ReadLine();

                // check input by kind
                switch (i_InputKind)
                {
                    case eInputValidation.UserGuess:
                        {
                            isLegalInput = isLegalGuess(userInput);
                        }
                        break;
                    case eInputValidation.UserGuessesAmount:
                        {
                            isLegalInput = isLegalGuessesAmount(userInput);
                        }
                        break;
                    case eInputValidation.ExitScreen:
                        {
                            isLegalInput = isLegalExit(userInput);
                        }
                        break;
                }
                if (!isLegalInput)
                {
                    Console.WriteLine("Wrong input, please try again");
                }
            }
            while (!isLegalInput);

            return userInput;
        }

        private bool isLegalExit(string i_UserInput)
        {
            return (i_UserInput == ((char)(eGameKeys.YesKey)).ToString() || i_UserInput == ((char)eGameKeys.NoKey).ToString());
        }

        private ushort getGuessesAmount()
        {
            ushort result;

            ushort minGuessBound = (ushort)GameLogic.eGuessAmountBounds.MinGuessNum;
            ushort maxGuessBound = (ushort)GameLogic.eGuessAmountBounds.MaxGuessNum;

            string userMessage = 
                string.Format("Please enter the maximum amount of guesses you wish ({0} - {1})",
                  minGuessBound, maxGuessBound);


            // get validated user input
            string input = getUserInput(eInputValidation.UserGuessesAmount, userMessage);

            result = ushort.Parse(input);
            return result;
        }

        /* gameSession: a single game session progress */
        private eRunState gameSession(out ushort o_StepsTaken)
        {
            
            eRunState runState = eRunState.Continue; // current game run state
            ushort guessesAmount = getGuessesAmount();
            m_Logic.UserGuessesAmount = guessesAmount; // set guessAmount
            m_Logic.initiateGame();
            o_StepsTaken = 0;

            for (int i = 1; i < m_Logic.Board.Length && runState == eRunState.Continue; i++)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                PrintCurrentBoardStatus();
                runState = handleGuessInput(i);
                ++o_StepsTaken;
            }
            
            // check if user had too many steps
            if (runState == eRunState.Continue && o_StepsTaken == m_Logic.UserGuessesAmount)
            {
                runState = eRunState.Lost;
            }

            return runState;
        }

        private eRunState handleGuessInput(int i_BoardIndex)
        {
            eRunState result = eRunState.Continue;

            // build string
            StringBuilder userMessage = new StringBuilder();
            userMessage.Append("Please type your next guess <");
            for (int i = 0; i < m_Logic.GuessArraySize; i++)
            {
                userMessage.AppendFormat("{0}", 
                    (char)((char)GameLogic.eGuessLetterBounds.MinGuessLetter + i));
                if (i < GameLogic.k_GuessArraySize - 1)
                {
                    userMessage.Append(k_WordDelimiter);
                }
            }
            userMessage.AppendFormat("> or '{0}' to quit", (char)eGameKeys.QuitKey);

            // get user guess
            string userGuess = getUserInput(eInputValidation.UserGuess, userMessage.ToString());

            if (userGuess == ((char)eGameKeys.QuitKey).ToString()) // user opted to quit
            {
                result = eRunState.UserQuit;
            }
            else // keep going
            {
                BoardLine currentLine = this.m_Logic.Board[i_BoardIndex];

                // split guess by whitespaces and insert to board
                currentLine.UserGuess = userGuess.Replace(k_WordDelimiter, string.Empty).ToCharArray();

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

        private bool isLegalGuessesAmount(string i_UserInput)
        {
            bool result;
            ushort guessesAmount;
           
            ushort minGuessBound = (ushort)GameLogic.eGuessAmountBounds.MinGuessNum;
            ushort maxGuessBound = (ushort)GameLogic.eGuessAmountBounds.MaxGuessNum;

            result = ushort.TryParse(i_UserInput, out guessesAmount) &&
                    (guessesAmount >= minGuessBound && guessesAmount <= maxGuessBound);

            return result;
        }


        /*
        //Made to replace IsLegalGuess if needed
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

        private bool isGuessWithLegalLetters(string i_Input)
        {
            for (int i = 1; i < i_Input.Length; i+= 2)
            {
                
            }
        }
        */


        private bool isLegalGuess(string i_UserGuess)
        {
            bool hasSpaces = true;
            bool hasLegalLetters = true;

            bool isCorrectSize = (i_UserGuess.Length == m_Logic.GuessArraySize * 2 - 1);

            for (int i = 0; i < i_UserGuess.Length; i++)
            {
                if (m_Logic.isLetterLegal(i, i_UserGuess))
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

            return (i_UserGuess == ((char)eGameKeys.QuitKey).ToString())
                || (hasLegalLetters && hasSpaces && isCorrectSize && !m_Logic.hasDuplicateLetters(i_UserGuess));
        }



        // Print methods
        /*
        private void printCurrentBoardStatus()
        {
            StringBuilder boardPrint = new StringBuilder();
            ushort barSize = this.calculateBarSize();
            string pinsString = "Pins:";
            string resultsString = "Results:";

            boardPrint.AppendFormat("Current board status:{0}{0}", System.Environment.NewLine);
            boardPrint.AppendFormat("|{0}", pinsString);

        }*/

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

            Console.Write("|{0}", System.Environment.NewLine);

            // TODO: we need to print here the # # # #, but we should do it more efficient
           
            printBoard();
        }

        /*
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
            }
        }
        */

        private void printBoard()
        {
            for(int i = 0; i < m_Logic.Board.Length; i++)
            {
                printBoardLine(i);
            }
        }

        private void printBoardLine(int line)
        {
            ushort barSize = calculateBarSize();
            ushort resultsAmount = 
                (ushort)(m_Logic.Board[line].ExistRightPlaceResult + m_Logic.Board[line].ExistWrongPlaceResult);

            Console.Write("| ");

            for (int i = 0; i < m_Logic.GuessArraySize; i++)
            {
                printBoardCell(line, i);
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

        private void printBoardCell(int line, int col)
        {
            Console.Write("{0}", m_Logic.Board[line][col]);
        }

        private void printBorder(ushort barSize)
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
