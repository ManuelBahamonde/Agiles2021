using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP.Utilities;

namespace TP.Tests
{
    [TestClass]
    public class ValidationsTests
    {
        [TestMethod]
        public void ValidateName_CorrectName()
        {
            // Arrange
            string name = "Manuel";
            string expected = null;

            // Act
            var actual = Validations.ValidateName(name);

            // Assert
            Assert.AreEqual(expected, actual.Error);
        }

        [TestMethod]
        public void ValidateName_NullName()
        {
            // Arrange
            string name = null;
            string expected = "El nombre es nulo o vacio.";

            // Act
            var actual = Validations.ValidateName(name);

            // Assert
            Assert.AreEqual(expected, actual.Error);
        }

        [TestMethod]
        public void ValidateName_SpecialCharactersInName()
        {
            // Arrange
            string name = "Manuel!";
            string expected = "Los caracteres ingresados no son validos.";

            // Act
            var actual = Validations.ValidateName(name);

            // Assert
            Assert.AreEqual(expected, actual.Error);
        }

        [TestMethod]
        public void ValidateName_LongName()
        {
            // Arrange
            string name = "ExtremelyLongNameTest1234567890";
            string expected = "El nombre debe tener una longitud maxima de 20 caracteres.";

            // Act
            var actual = Validations.ValidateName(name);

            // Assert
            Assert.AreEqual(expected, actual.Error);
        }
    }
}
