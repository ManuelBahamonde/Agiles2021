using System;
using Tp.Models;

namespace TP.Game
{
    public class MainGame
    {
        #region Members
        private readonly string _secretWord;
        public int AttemptsLeft { get; private set; }
        public string Name { get; }
        public Difficulty Difficulty { get; }
        #endregion

        #region Constructor
        public MainGame(int numberOfAttempts, string name, Difficulty difficulty)
        {
            AttemptsLeft = numberOfAttempts;
            Name = name;
            Difficulty = difficulty;

            // TODO: Generate a random word and assign it to _secretWord
            _secretWord = difficulty switch
            {
                Difficulty.Easy => "puma",
                Difficulty.Medium => "leopardo",
                Difficulty.Hard => "hipopotamo",
                _ => throw new ArgumentException("Invalid difficulty. It must be easy, medium or hard")
            };
        }
        #endregion

        #region Methods
        public TryResponse TryWord(string word)
        {
            var response = new TryResponse();

            var isMatch = _secretWord == word.ToLower();
            if (!isMatch)
                AttemptsLeft--;

            response.AttemptsLeft = AttemptsLeft;
            response.Match = isMatch;

            return response;
        }

        public TryResponse TryLetter(char letter)
        {
            var response = new TryResponse();

            if (letter == '\0' || !char.IsLetter(letter))
                throw new ArgumentException("Invalid Letter. It must be an alphabetic character.");

            var isMatch = _secretWord.Contains(letter, StringComparison.InvariantCultureIgnoreCase);
            if (!isMatch)
                AttemptsLeft--;

            response.Match = isMatch;
            response.AttemptsLeft = AttemptsLeft;

            return response;
        }
        #endregion
    }
}
