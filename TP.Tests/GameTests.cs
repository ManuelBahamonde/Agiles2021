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
        private readonly MainGame game = new MainGame(3, "Manuel", Difficulty.Easy);

        #region Init
        #region Name
        [TestMethod]
        public void InitGame_NameIsCorrect()
        {
            // Arrange
            var name = "Manuel";
            var expected = "Manuel";

            // Act
            var game = new MainGame(3, name, Difficulty.Easy);

            // Assert
            Assert.AreEqual(expected, game.Name);
        }
        #endregion

        #region Difficulty
        [TestMethod]
        public void InitGame_EasyDifficulty()
        {
            // Arrange
            var word = "puma";

            // Act
            var game = new MainGame(3, "Manuel", Difficulty.Easy);

            // Assert
            Assert.IsTrue(game.TryWord(word).Match);
        }

        [TestMethod]
        public void InitGame_MediumDifficulty()
        {
            // Arrange
            var word = "leopardo";

            // Act
            var game = new MainGame(3, "Manuel", Difficulty.Medium);

            // Assert
            Assert.IsTrue(game.TryWord(word).Match);
        }

        [TestMethod]
        public void InitGame_HardDifficulty()
        {
            // Arrange
            var word = "hipopotamo";

            // Act
            var game = new MainGame(3, "Manuel", Difficulty.Hard);

            // Assert
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
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TryLetter_IsInvalid()
        {
            // Arrange
            var letter = '1';

            // Act
            game.TryLetter(letter);
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
    }
}
