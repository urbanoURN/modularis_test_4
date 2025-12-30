using System;
using System.Collections.Generic;

namespace ValidateDelimiters.Core
{
    /// <summary>
    /// Validates if a formula with delimiters is well-formed
    /// </summary>
    public class DelimiterValidator : IDelimiterValidator
    {
        private readonly Dictionary<char, char> _bracketPairs;

        public DelimiterValidator()
        {
            // Mapping of closing brackets to opening brackets
            _bracketPairs = new Dictionary<char, char>
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' }
            };
        }

        /// <summary>
        /// Validates whether a formula is well formed
        /// </summary>
        public bool IsWellFormed(string formula)
        {
            if (string.IsNullOrEmpty(formula))
            {
                return true;
            }

            var stack = new Stack<char>();

            foreach (char character in formula)
            {
                if (IsOpeningBracket(character))
                {
                    stack.Push(character);
                }
                else if (IsClosingBracket(character))
                {
                    if (stack.Count == 0)
                    {
                        return false;
                    }

                    char lastOpening = stack.Pop();
                    
                    if (_bracketPairs[character] != lastOpening)
                    {
                        return false;
                    }
                }
            }

            return stack.Count == 0;
        }

        private bool IsOpeningBracket(char c)
        {
            return c == '(' || c == '[' || c == '{';
        }

        private bool IsClosingBracket(char c)
        {
            return _bracketPairs.ContainsKey(c);
        }
    }
}