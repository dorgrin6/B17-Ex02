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

        public BoardLine(ushort amount)
        {
            m_UserGuess = new char[amount];
            for (int i = 0; i < amount; i++)
            {
                m_UserGuess[i] = ' ';
            }
        }

        public ushort ExistWrongPlaceResult
        {
            get
            {
                return m_ExistWrongPlaceResult;
            }
        }

        public ushort ExistRightPlaceResult
        {
            get
            {
                return m_ExistRightPlaceResult;
            }
        }

        public char this[int i_index]
        {
            get
            {
                return m_UserGuess[i_index];
            }
            set
            {
                m_UserGuess[i_index] = value;
            }
        }
    }
}
