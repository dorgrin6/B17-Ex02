using System;
using System.Collections.Generic;

namespace Program
{

    class GameManager
    {
        private List<char> m_ValuesToGuess = new List<char>(k_GuessArraySize); // holds computer guesses

        private List<char> m_UserGuesses = new List<char>(k_GuessArraySize); // holds user guesses

        private char[,] m_GuessBoard;  // holds current play board

        private char[,] m_ResultsBoard; // holds result given

        private const ushort k_GuessArraySize = 4; // guesses array size

        private ushort m_userGuessNum; // holds amount of guesses wanted by user 

        internal enum eGuessAmountBounds
        {
            MinGuessNum = 4,
            MaxGuessNum = 10
        }

        internal enum eGuessLetterBounds
        {
            MinGuessLetter = 'A',
            MaxGuessLetter = 'H'
        }

        public ushort UserGuessNum
        {
            get
            {
                return m_userGuessNum;
            }
            set
            {
                m_userGuessNum = value;
            }

        }

        public void Run()
        {
            // get amount of guesses
            m_userGuessNum = UserInterface.GetGuessesAmount();

            Ex02.ConsoleUtils.Screen.Clear();
            getValuesToGuess();

            // set GuessBpard and ResultBoard
            m_GuessBoard = new char[m_userGuessNum,k_GuessArraySize];
            this.m_ResultsBoard = new char[this.m_userGuessNum, k_GuessArraySize];

            UserInterface.PrintArray<char>(m_ValuesToGuess);
        }

        private void getValuesToGuess()
        { 
            Random randInt = new Random();
            char letter;

            // insert letters to array
            for (int i = 0; i < m_userGuessNum; i++)
            {
                // check if letter was  already generated, keep going untill you find new one
                do
                {
                    letter = 
                        (char)randInt.Next((int)eGuessLetterBounds.MinGuessLetter, (int)eGuessLetterBounds.MaxGuessLetter);
                }while (m_ValuesToGuess.Contains(letter));

                m_ValuesToGuess.Add(letter);
            }

        }
    }
}
