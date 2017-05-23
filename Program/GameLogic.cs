using System;
using System.Collections.Generic;

namespace Program
{
    class GameLogic
    {
        private const ushort k_GuessArraySize = 4; // guesses array size

        private ushort m_UserGuessesAmount; // holds amount of guesses wanted by user

        private char[] m_RandomValues = new char[k_GuessArraySize]; // holds computer values

        private char[] m_CurrentUserGuesses = new char[k_GuessArraySize]; // holds user guesses

        private char[,] m_GuessesBoard;  // holds current play board

        private char[,] m_ResultsBoard; // holds result given to user




        internal enum eGuessAmountBounds : ushort
        {
            MinGuessNum = 4,
            MaxGuessNum = 10
        }

        internal enum eGuessLetterBounds : ushort
        {
            MinGuessLetter = 'A',
            MaxGuessLetter = 'H'
        }

        public ushort UserGuessesAmount
        {
            get
            {
                return this.m_UserGuessesAmount;
            }
            set
            {
                this.m_UserGuessesAmount = value;
            }

        }



        /*private void getValuesToGuess()
        {
            Random randInt = new Random();
            char letter;

            // insert letters to array
            for (int i = 0; i < this.m_UserGuessesAmount; i++)
            {
                // check if letter was  already generated, keep going untill you find new one
                do
                {
                    letter =
                        (char)randInt.Next((int)eGuessLetterBounds.MinGuessLetter, (int)eGuessLetterBounds.MaxGuessLetter);
                } while (this.m_RandomValues.Contains(letter));

                this.m_RandomValues.Add(letter);
            }

        }*/
    }
}
