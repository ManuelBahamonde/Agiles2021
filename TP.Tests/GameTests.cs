using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp.Models;
using TP.Game;

namespace Tp.Tests
{
    [TestClass]
    public class GameTests
    {
        private readonly MainGame game = new MainGame("Manuel", Difficulty.Easy);

        #region Init
        #region Name
        [TestMethod]
        public void InitGame_NameIsCorrect()
        {
            // Arrange
            var name = "Manuel";
            var expected = "Manuel";

            // Act
            var game = new MainGame(name, Difficulty.Easy);

            // Assert
            Assert.AreEqual(expected, game.Name);
        }
        [TestMethod]
        public void InitGame_NameIsNullOrEmpty()
        {
            // Arrange
            var name = "";

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new MainGame(name, Difficulty.Easy), "The name is null or empty");
        }
        [TestMethod]
        public void InitGame_NameMaxLength()
        {
            // Arrange
            var name = "holaholaholaholaholahola";

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new MainGame(name, Difficulty.Easy), "Name maximum length is 20 characters");
        }
        [TestMethod]
        public void InitGame_NameInvalidCharacters()
        {
            // Arrange
            var name = "Juan!!";

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new MainGame(name, Difficulty.Easy), "Name characters are not valid");
        }
        #endregion

        #region Difficulty
        [TestMethod]
        public void InitGame_EasyDifficulty()
        {
            // Arrange
            var word = "puma";
            var difficulty = Difficulty.Easy;

            // Act
            var game = new MainGame("Manuel", difficulty);

            // Assert
            Assert.AreEqual(game.Difficulty, difficulty);
            Assert.IsTrue(game.TryWord(word).Match);
        }

        [TestMethod]
        public void InitGame_MediumDifficulty()
        {
            // Arrange
            var word = "leopardo";
            var difficulty = Difficulty.Medium;

            // Act
            var game = new MainGame("Manuel", difficulty);

            // Assert
            Assert.AreEqual(game.Difficulty, difficulty);
            Assert.IsTrue(game.TryWord(word).Match);
        }

        [TestMethod]
        public void InitGame_HardDifficulty()
        {
            // Arrange
            var word = "hipopotamo";
            var difficulty = Difficulty.Hard;

            // Act
            var game = new MainGame("Manuel", difficulty);

            // Assert
            Assert.AreEqual(game.Difficulty, difficulty);
            Assert.IsTrue(game.TryWord(word).Match);
        }
        #endregion
        #endregion

        #region TryLetter
        [TestMethod]
        public void TryLetter_IsCorrect()
        {
            // Arrange
            var letter = 'u';

            // Act
            var result = game.TryLetter(letter);

            // Assert
            Assert.IsTrue(result.Match);
        }

        [TestMethod]
        public void TryLetter_IsIncorrect()
        {
            // Arrange
            var letter = 'x';

            // Act
            var result = game.TryLetter(letter);

            // Assert
            Assert.IsFalse(result.Match);
            Assert.IsTrue(game.IncorrectChars.Contains(letter));
        }

        [TestMethod]
        public void TryLetter_IsNumber()
        {
            // Arrange
            var letter = '1';

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => game.TryLetter(letter), "Invalid Letter. It must be an alphabetic character.");
        }
        [TestMethod]
        public void TryLetter_IsEmpty()
        {
            // Arrange
            var letter = '\0';

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => game.TryLetter(letter), "Invalid Letter. It must be an alphabetic character.");
        }
        [TestMethod]
        public void TryLetter_IgnoreCase()
        {
            // Arrange
            var letter1 = 'a';
            var letter2 = 'A';

            // Act
            var result1 = game.TryLetter(letter1);
            var result2 = game.TryLetter(letter2);

            // Assert
            Assert.AreEqual(result1.Match, result2.Match);
        }

        [TestMethod]
        public void TryLetter_AttemptsLeft_Correct()
        {
            // Arrange
            var letter = 'u';
            var expectedAttemptsLeft = game.AttemptsLeft;

            // Act
            var response = game.TryLetter(letter);

            // Assert
            Assert.AreEqual(response.AttemptsLeft, expectedAttemptsLeft);
        }

        [TestMethod]
        public void TryLetter_AttemptsLeft_Incorrect()
        {
            // Arrange
            var letter = 'x';
            var expectedAttemptsLeft = game.AttemptsLeft - 1;

            // Act
            var response = game.TryLetter(letter);

            // Assert
            Assert.AreEqual(response.AttemptsLeft, expectedAttemptsLeft);
        }

        [TestMethod]
        public void TryLetter_Results()
        {
            // Arrange
            var letter = 'a';
            var expectedResult = "___a";

            // Act
            game.TryLetter(letter);

            // Assert
            Assert.AreEqual(game.Result, expectedResult);
        }
        #endregion

        #region TryWord
        [TestMethod]
        public void TryWord_IsCorrect()
        {
            // Arrange
            var word = "puma";
            var expected = true;

            // Act
            var response = game.TryWord(word);

            // Assert
            Assert.AreEqual(expected, response.Match);
        }

        [TestMethod]
        public void TryWord_IsIncorrect()
        {
            // Arrange
            var word = "Incorrecto";
            var expected = false;

            // Act
            var response = game.TryWord(word);

            // Assert
            Assert.AreEqual(expected, response.Match);
        }

        [TestMethod]
        public void TryWord_AttemptsLeft_Correct()
        {
            // Arrange
            var word = "Incorrecto";
            var expectedAttemptsLeft = game.AttemptsLeft - 1;

            // Act
            var response = game.TryWord(word);

            // Assert
            Assert.AreEqual(response.AttemptsLeft, expectedAttemptsLeft);
        }

        [TestMethod]
        public void TryWord_AttemptsLeft_Incorrect()
        {
            // Arrange
            var word = "puma";
            var expectedAttemptsLeft = game.AttemptsLeft;

            // Act
            var response = game.TryWord(word);

            // Assert
            Assert.AreEqual(response.AttemptsLeft, expectedAttemptsLeft);
        }
        #endregion

        #region Game Over
        [TestMethod]
        public void TryWord_WinGame()
        {
            // Arrange
            var word = "puma";
            var expected = true;

            // Act
            game.TryWord(word);

            // Assert
            Assert.AreEqual(game.IsGameOver && game.Win, expected);
        }
        [TestMethod]
        public void TryWord_LoseGame()
        {
            // Arrange
            var word = "error";
            var expected = true;

            // Act (6 incorrect trials)
            game.TryWord(word);
            game.TryWord(word);
            game.TryWord(word);
            game.TryWord(word);
            game.TryWord(word);
            game.TryWord(word);

            // Assert
            Assert.AreEqual(game.IsGameOver && !game.Win, expected);
        }

        [TestMethod]
        public void TryWord_InvalidWord()
        {
            // Arrange
            var word = "error!";

            // Act and Assert
            Assert.ThrowsException<InvalidOperationException>(() => game.TryWord(word), "Invalid Word.");
        }

        [TestMethod]
        public void TryLetter_WinGame()
        {
            // Arrange
            var letter1 = 'p';
            var letter2 = 'u';
            var letter3 = 'm';
            var letter4 = 'a';
            var expected = true;

            // Act
            game.TryLetter(letter1);
            game.TryLetter(letter2);
            game.TryLetter(letter3);
            game.TryLetter(letter4);

            // Assert
            Assert.AreEqual(game.IsGameOver && game.Win, expected);
        }
        [TestMethod]
        public void TryLetter_LoseGame()
        {
            // Arrange
            var letter1 = 'e';
            var letter2 = 'r';
            var letter3 = 'r';
            var letter4 = 'o';
            var letter5 = 'r';
            var letter6 = 's';
            var expected = true;

            // Act (6 incorrect trials)
            game.TryLetter(letter1);
            game.TryLetter(letter2);
            game.TryLetter(letter3);
            game.TryLetter(letter4);
            game.TryLetter(letter5);
            game.TryLetter(letter6);

            // Assert
            Assert.AreEqual(game.IsGameOver && !game.Win, expected);
        }

        [TestMethod]
        public void Try_CheckWonExceptions()
        {
            // Arrange
            var word = "puma";

            // Act
            game.TryWord(word);

            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => game.TryWord(word), "Game Over: the player won");
        }

        [TestMethod]
        public void Try_CheckLostExceptions()
        {
            // Arrange
            var letter = 'e';

            // Act (6 incorrect trials)
            game.TryLetter(letter);
            game.TryLetter(letter);
            game.TryLetter(letter);
            game.TryLetter(letter);
            game.TryLetter(letter);
            game.TryLetter(letter);

            // Assert
            Assert.ThrowsException<InvalidOperationException>(() => game.TryLetter(letter), "Game Over: the player lost");
        }
        #endregion
    }
}
