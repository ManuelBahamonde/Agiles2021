using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tp.Models;

namespace TP.Game
{
    public class MainGame
    {
        #region Members
        private readonly string _secretWord;
        public string Result { 
            get 
            {
                // Getting not guessed chars 
                var notGuessedChars = _secretWord.Where(c => !CorrectChars.Contains(c)).ToList();

                // Replacing not guessed chars by _
                return notGuessedChars.Aggregate(_secretWord, (str, notGuessedChar) => str.Replace(notGuessedChar, '_'));
            }
        }
        public HashSet<char> CorrectChars { get; private set; } = new HashSet<char>();
        public HashSet<char> IncorrectChars { get; private set; } = new HashSet<char>();

        public int AttemptsLeft { get; private set; } = 6;
        public string Name { get; }
        public Difficulty Difficulty { get; }
        public bool IsGameOver { get => Result == _secretWord || AttemptsLeft == 0; }
        public bool Win { get => IsGameOver && AttemptsLeft > 0; }
        #endregion

        #region Constructor
        public MainGame(string name, Difficulty difficulty)
        {
            // Validate
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("The name is null or empty");

            if (name.Length > 20)
                throw new ArgumentException("Name maximum length is 20 characters");

            if (!new Regex("^[a-zA-Z]*$").IsMatch(name))
                throw new ArgumentException("Name characters are not valid");

            Name = name;
            Difficulty = difficulty;

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
        private void ValidateTry()
        {
            if (IsGameOver)
            {
                string gameOverMessage;
                if (Win)
                    gameOverMessage = "Game Over: the player won";
                else
                    gameOverMessage = "Game Over: the player lost";

                throw new InvalidOperationException(gameOverMessage);
            }
        }

        public TryResponse TryWord(string word)
        {
            ValidateTry();

            var response = new TryResponse();

            if (string.IsNullOrEmpty(word) || word.Any(x => !char.IsLetter(x)))
                throw new ArgumentException("Invalid Word.");

            var isMatch = _secretWord == word.ToLower();
            if (isMatch)
            {
                foreach (char c in _secretWord)
                    CorrectChars.Add(c);
            }
            else
            {
                AttemptsLeft--;
            }

            response.AttemptsLeft = AttemptsLeft;
            response.Match = isMatch;

            return response;
        }

        public TryResponse TryLetter(char letter)
        {
            ValidateTry();

            var response = new TryResponse();

            if (letter == '\0' || !char.IsLetter(letter))
                throw new ArgumentException("Invalid Letter. It must be an alphabetic character.");

            letter = char.ToLower(letter);
            var isMatch = _secretWord.Contains(letter);
            if (isMatch)
            {
                CorrectChars.Add(letter);
            }
            else
            {
                AttemptsLeft--;
                IncorrectChars.Add(letter);
            }

            response.Match = isMatch;
            response.AttemptsLeft = AttemptsLeft;

            return response;
        }
        #endregion
    }
}
