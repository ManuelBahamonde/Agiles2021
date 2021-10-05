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
        public void TryWord_IsCorrect()
        {
            // Arrange
            var word = "Correcta";
            var expected = true;

            // Act
            var actual = game.Try(word);

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
            var actual = game.Try(word);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
