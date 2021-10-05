using System;

namespace TP.Game
{
    public class MainGame
    {
        #region Members
        private readonly string _secretWord;
        public int AttemptsLeft { get; private set; }
        public string Name { get; }
        #endregion

        #region Constructor
        public MainGame(int numberOfAttempts, string name)
        {
            _secretWord = "Correcta"; // TODO: Generate a random word and assign it to _secretWord
            AttemptsLeft = numberOfAttempts;
            Name = name;
        }
        #endregion

        #region Methods
        public bool Try(string word)
        {
            if (_secretWord != word)
            {
                AttemptsLeft--;
                return false;
            }

            return true;
        }
        #endregion
    }
}
