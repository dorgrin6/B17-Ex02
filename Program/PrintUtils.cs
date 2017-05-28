using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class PrintUtils
    {
        public static void PrintCurrentBoardStatus(GameLogic i_Logic)
        {
            ushort barSize = calculateBarSize(i_Logic);
            string pinsString = "Pins:";
            string resultsString = "Results:";

            Console.WriteLine("Current board status:{0}", System.Environment.NewLine);

            Console.Write("|{0}", pinsString);
            printDuplicateChar(' ', (ushort)(barSize - pinsString.Length));

            Console.Write("|{0}", resultsString);
            printDuplicateChar(' ', (ushort)((barSize - 1) - resultsString.Length));

            Console.Write("|{0}", System.Environment.NewLine);

            printBoard(i_Logic);
        }


        private static void printBoard(GameLogic i_Logic)
        {
            for (int i = 0; i < i_Logic.Board.Length; i++)
            {
                printBoardLine(i_Logic, i);
            }
        }

        private static void printBoardLine(GameLogic i_Logic, int line)
        {
            ushort barSize = calculateBarSize(i_Logic);
            ushort resultsAmount =
                (ushort)(i_Logic.Board[line].ExistRightPlaceResult + i_Logic.Board[line].ExistWrongPlaceResult);

            Console.Write("| ");

            for (int i = 0; i < i_Logic.GuessArraySize; i++)
            {
                printBoardCell(i_Logic, line, i);
                Console.Write(' ');
            }

            Console.Write('|');


            for (int i = 0; i < i_Logic.Board[line].ExistRightPlaceResult; i++)
            {
                Console.Write("{0} ", (char)BoardLine.eResultLetter.ExistRightPlace);
            }
            for (int i = 0; i < i_Logic.Board[line].ExistWrongPlaceResult; i++)
            {
                Console.Write("{0} ", (char)BoardLine.eResultLetter.ExistWrongPlace);
            }

            printDuplicateChar(' ', (ushort)((barSize - 1) - resultsAmount * 2));
            Console.Write("|{0}", System.Environment.NewLine);
            printBorder(barSize);
        }

        private static void printBoardCell(GameLogic i_Logic, int line, int col)
        {
            Console.Write("{0}", i_Logic.Board[line][col]);
        }

        private static void printBorder(ushort barSize)
        {
            Console.Write('|');
            printDuplicateChar('=', barSize);
            Console.Write('|');
            printDuplicateChar('=', (ushort)(barSize - 1));
            Console.WriteLine('|');
        }

        private static ushort calculateBarSize(GameLogic i_Logic)
        {
            return (ushort)(i_Logic.GuessArraySize * 2 + 1);
        }

        private static void printDuplicateChar(char ch, ushort repeats)
        {
            for (int i = 0; i < repeats; i++)
            {
                Console.Write(ch);
            }
        }
    }
}
