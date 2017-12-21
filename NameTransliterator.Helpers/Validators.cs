namespace NameTransliterator.Helpers
{
    using System;
    using System.Text.RegularExpressions;

    public class Validators
    {
        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name is not valid.");
            }
        }

        public bool IsRegexPatternValid(string pattern)
        {
            try
            {
                new Regex(pattern);

                return true;
            }
            catch
            {
            }

            return false;
        }
    }
}
