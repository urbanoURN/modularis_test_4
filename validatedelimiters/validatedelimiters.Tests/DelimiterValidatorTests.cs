using System;
using System.IO;
using System.Linq;
using Xunit;
using ValidateDelimiters.Core;

namespace ValidateDelimiters.Tests
{
    public class DelimiterValidatorTests
    {
        private readonly IDelimiterValidator _validator;
        private readonly IFormulaReader _reader;

        public DelimiterValidatorTests()
        {
            _validator = new DelimiterValidator();
            _reader = new FileFormulaReader();
        }

        [Theory]
        [InlineData("()")]
        [InlineData("[]")]
        [InlineData("{}")]
        [InlineData("()[]{}")]
        [InlineData("([])")]
        [InlineData("{[()]}")]
        [InlineData("")]
        public void IsWellFormed_ValidFormulas_ReturnsTrue(string formula)
        {
            // Act
            var result = _validator.IsWellFormed(formula);

            // Assert
            Assert.True(result, $"La fórmula '{formula}' debería ser válida");
        }

        [Theory]
        [InlineData("(]")]
        [InlineData("([)]")]
        [InlineData("((())")]
        [InlineData("())")]
        [InlineData("{[}]")]
        [InlineData("(")]
        [InlineData("]")]
        public void IsWellFormed_InvalidFormulas_ReturnsFalse(string formula)
        {
            // Act
            var result = _validator.IsWellFormed(formula);

            // Assert
            Assert.False(result, $"La fórmula '{formula}' debería ser inválida");
        }

        [Fact]
        public void IsWellFormed_WellFormedFile_AllFormulasAreValid()
        {
            // Arrange
            var testDataPath = Path.Combine("TestData", "WellFormedFormulas.txt");
            
            if (!File.Exists(testDataPath))
            {
                throw new FileNotFoundException($"Archivo no encontrado: {testDataPath}");
            }

            var formulas = _reader.ReadFormulas(testDataPath);

            // Act & Assert
            foreach (var formula in formulas)
            {
                var result = _validator.IsWellFormed(formula);
                Assert.True(result, $"La fórmula '{formula}' del archivo debería ser válida");
            }
        }

        [Fact]
        public void IsWellFormed_BadFormedFile_AllFormulasAreInvalid()
        {
            // Arrange
            var testDataPath = Path.Combine("TestData", "BadFormedFormulas.txt");
            
            if (!File.Exists(testDataPath))
            {
                throw new FileNotFoundException($"Archivo no encontrado: {testDataPath}");
            }

            var formulas = _reader.ReadFormulas(testDataPath);

            // Act & Assert
            foreach (var formula in formulas)
            {
                var result = _validator.IsWellFormed(formula);
                Assert.False(result, $"La fórmula '{formula}' del archivo debería ser inválida");
            }
        }

        [Fact]
        public void FileFormulaReader_NonExistentFile_ThrowsFileNotFoundException()
        {
            // Arrange
            var nonExistentPath = "archivo_que_no_existe.txt";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => _reader.ReadFormulas(nonExistentPath).ToList());
        }
    }
}