using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.Game;

namespace Tp.Tests
{
    [TestClass]
    public class GameTests
    {
        private readonly MainGame game = new MainGame(3, "Manuel");

        [TestMethod]
        public void InitGame_NameIsCorrect()
        {
            // Arrange
            var name = "Manuel";
            var expected = "Manuel";

            // Act
            var game = new MainGame(3, name);

            // Assert
            Assert.AreEqual(expected, game.Name);
        }

        [TestMethod]
        public void TryLetter_IsCorrect()
        {
            // Arrange
            var letter = 'c';

            // Act
            var result = game.TryLetter(letter);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TryLetter_IsIncorrect()
        {
            // Arrange
            var letter = 'x';

            // Act
            var result = game.TryLetter(letter);

            // Assert
            Assert.IsFalse(result);
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
        public void TryWord_IsCorrect()
        {
            // Arrange
            var word = "Correcta";
            var expected = true;

            // Act
            var actual = game.TryWord(word);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TryWord_IsIncorrect()
        {
            // Arrange
            var word = "Incorrecto";
            var expected = false;

            // Act
            var actual = game.TryWord(word);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
