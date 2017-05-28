namespace Program
{
    class BoardLine
    {
        private char[] m_UserGuess;
        private ushort m_ExistWrongPlaceResult = 0;
        private ushort m_ExistRightPlaceResult = 0;

        public BoardLine(ushort i_Amount, char i_Letter)
        {
            UserGuess = new char[i_Amount];
            for (int i = 0; i < i_Amount; i++)
            {
                UserGuess[i] = i_Letter;
            }
        }

        internal enum eResultLetter : ushort
        {
            ExistWrongPlace = 'X',
            ExistRightPlace = 'V'
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

        public char this[int i_Index]
        {
            get
            {
                return UserGuess[i_Index];
            }

            set
            {
                UserGuess[i_Index] = value;
            }
        }
    }
}
