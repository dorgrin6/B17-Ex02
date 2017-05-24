using System;
using System.Collections.Generic;

namespace Program
{
    class GameLogic
    {
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

        
        private const short k_NotExists = -1;

        private const ushort k_GuessArraySize = 4; // guesses array size

        private ushort m_UserGuessesAmount; // holds amount of guesses wanted by user

        private short[] m_GameGoal; // holds computer values

        private BoardLine[] m_Board;

        //TODO: we dont need the following variables, should be deleted
        //private char[] m_CurrentUserGuesses = new char[k_GuessArraySize]; // holds user guesses
        //private char[,] m_GuessesBoard;  // holds current play board
        //private char[,] m_ResultsBoard; // holds result given to user

        

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

        public ushort GuessArraySize
        {
            get
            {
                return k_GuessArraySize;
            }
        }

        public BoardLine[] Board
        {
            get
            {
                return m_Board;
            }
        }

        public BoardLine this[int i_index]
        {
            get
            {
                return m_Board[i_index];
            }
        }


    
        public void initiateGame()
        {
            m_Board = new BoardLine[m_UserGuessesAmount];
            for (int i = 0; i < m_UserGuessesAmount; i++)
            {
                m_Board[i] = new BoardLine (k_GuessArraySize);
            }

            RandomGameGoal();

        }

        public void RandomGameGoal()
        {
            Random randInt;
            int hashIndex;
            int minBound = 0;
            int maxBound = (int)(eGuessLetterBounds.MaxGuessLetter - eGuessLetterBounds.MinGuessLetter+1); //we need to add 1 to the sum because the method Next return value that is less than maxBound

            initiateGameGoalValues();
            randInt = new Random();

            for (int i=0; i < k_GuessArraySize; i++)
            {
                do
                {
                    hashIndex = randInt.Next(minBound, maxBound);
                } while (m_GameGoal[hashIndex] != k_NotExists);
                m_GameGoal[hashIndex] = (short)i;
            }
        }

        private void initiateGameGoalValues()
        {
            ushort valuesAmount = eGuessLetterBounds.MaxGuessLetter - eGuessLetterBounds.MinGuessLetter + 1;
            m_GameGoal = new short[valuesAmount];
            for (int i = 0; i < valuesAmount; i++)
            {
                m_GameGoal[i] = k_NotExists;
            }
        }

        public bool CheckIfLetterLegal(char guess)
        {
            return ((int)guess >= (int)eGuessLetterBounds.MinGuessLetter && (int)guess <= (int)eGuessLetterBounds.MaxGuessLetter);
            // TODO: needs also to check that there is no letter then returns twice. not sure if its supose to be here or in the interface checks
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
