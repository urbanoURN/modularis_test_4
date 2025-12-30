namespace ValidateDelimiters.Core
{
    /// <summary>
    /// Interface to delimiter validators
    /// </summary>
    public interface IDelimiterValidator
    {
        bool IsWellFormed(string formula);
    }
}