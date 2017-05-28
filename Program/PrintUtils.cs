using System;

namespace Program
{
    class PrintUtils
    {
        public static void PrintCurrentBoardStatus(BoardLine[] i_Board)
        {
            ushort barSize = calculateBarSize(i_Board);
            string pinsString = "Pins:";
            string resultsString = "Results:";

            Console.WriteLine("Current board status:{0}", System.Environment.NewLine);

            Console.Write("|{0}", pinsString);
            printDuplicateTimes(" ", (ushort)(barSize - pinsString.Length));

            Console.Write("|{0}", resultsString);
            printDuplicateTimes(" ", (ushort)((barSize - 1) - resultsString.Length));

            Console.Write("|{0}", System.Environment.NewLine);

            printBoard(i_Board);
        }

        private static void printBoard(BoardLine[] i_Board)
        {
            for (int i = 0; i < i_Board.Length; i++)
            {
                printBoardLine(i_Board, i);
            }
        }

        private static void printBoardLine(BoardLine[] i_Board, int line)
        {
            ushort barSize = calculateBarSize(i_Board);
            ushort resultsAmount =
                (ushort)(i_Board[line].ExistRightPlaceResult + i_Board[line].ExistWrongPlaceResult);
            string resultString;

            Console.Write("| ");

            for (int i = 0; i < GameLogic.k_GuessArraySize; i++)
            {
                printBoardCell(i_Board, line, i);
                Console.Write(' ');
            }

            Console.Write('|');

            resultString = string.Format("{0} ", (char)BoardLine.eResultLetter.ExistRightPlace);
            printDuplicateTimes(resultString, i_Board[line].ExistRightPlaceResult);
            resultString = string.Format("{0} ", (char)BoardLine.eResultLetter.ExistWrongPlace);
            printDuplicateTimes(resultString, i_Board[line].ExistWrongPlaceResult);

            printDuplicateTimes(" ", (ushort)(barSize - 1 - (resultsAmount * 2)));
            Console.Write("|{0}", System.Environment.NewLine);
            printBorder(barSize);
        }

        private static void printBoardCell(BoardLine[] i_Board, int line, int col)
        {
            Console.Write(i_Board[line][col]);
        }

        private static void printBorder(ushort barSize)
        {
            Console.Write('|');
            printDuplicateTimes("=", barSize);
            Console.Write('|');
            printDuplicateTimes("=", (ushort)(barSize - 1));
            Console.WriteLine('|');
        }

        private static ushort calculateBarSize(BoardLine[] i_Board)
        {
            return (ushort)((GameLogic.k_GuessArraySize * 2) + 1);
        }

        private static void printDuplicateTimes(string i_string, ushort i_times)
        {
            for (int i = 0; i < i_times; i++)
            {
                Console.Write(i_string);
            }
        }
    }
}
