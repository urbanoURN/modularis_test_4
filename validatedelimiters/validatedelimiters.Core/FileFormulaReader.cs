using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ValidateDelimiters.Core
{
    /// <summary>
    /// Read formulas from text files
    /// </summary>
    public class FileFormulaReader : IFormulaReader
    {
        public IEnumerable<string> ReadFormulas(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("La ruta del archivo no puede estar vacía", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"El archivo no existe: {filePath}");
            }

            return File.ReadAllLines(filePath)
                       .Where(line => !string.IsNullOrWhiteSpace(line))
                       .Select(line => line.Trim());
        }
    }
}