using System;

namespace Program
{
    class BoardLine
    {
        internal enum eResultLetter : ushort
        {
            ExistWrongPlace = 'X',
            ExistRightPlace = 'V'
        }


        private char[] m_UserGuess;
        private ushort m_ExistWrongPlaceResult = 0;
        private ushort m_ExistRightPlaceResult = 0;

        public BoardLine(ushort amount, char letter)
        {
            UserGuess = new char[amount];
            for (int i = 0; i < amount; i++)
            {
                UserGuess[i] = letter;
            }
        }

        public ushort ExistWrongPlaceResult
        {
            get
            {
                return m_ExistWrongPlaceResult;
            }
            set
            {
                this.m_ExistWrongPlaceResult = value;
            }
        }

        public ushort ExistRightPlaceResult
        {
            get
            {
                return m_ExistRightPlaceResult;
            }
            set
            {
                this.m_ExistRightPlaceResult = value;
            }
        }

        public char[] UserGuess
        {
            get
            {
                return this.m_UserGuess;
            }
            set
            {
                m_UserGuess = value;

            }
        }

        public char this[int i_index]
        {
            get
            {
                return UserGuess[i_index];
            }
            set
            {
                UserGuess[i_index] = value;
            }
        }
    }
}
