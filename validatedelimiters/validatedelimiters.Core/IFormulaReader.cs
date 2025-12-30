using System.Collections.Generic;

namespace ValidateDelimiters.Core
{
    /// <summary>
    /// Interface for formula readers from files
    /// </summary>
    public interface IFormulaReader
    {
        IEnumerable<string> ReadFormulas(string filePath);
    }
}