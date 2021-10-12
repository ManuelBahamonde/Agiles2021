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
        public bool TryWord(string word)
        {
            if (_secretWord != word)
            {
                AttemptsLeft--;
                return false;
            }

            return true;
        }

        public bool TryLetter(char letter)
        {
            if (letter == '\0' || !char.IsLetter(letter))
                throw new ArgumentException("Invalid Letter. It must be an alphabetic character.");

            AttemptsLeft--;

            return _secretWord.Contains(letter);
        }
        #endregion
    }
}
