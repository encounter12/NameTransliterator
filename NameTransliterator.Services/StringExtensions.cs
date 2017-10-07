namespace NameTransliterator.Services
{
    using System;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string ConvertMultipleWhitespacesToSingleSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
