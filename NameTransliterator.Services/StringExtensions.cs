namespace NameTransliterator.Services
{
    using System;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string TrimAndRemoveExtraWhiteSpaces(this string str)
        {
            return ConvertWhitespacesToSingleSpaces(str).Trim();
        }

        public static string ConvertWhitespacesToSingleSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
