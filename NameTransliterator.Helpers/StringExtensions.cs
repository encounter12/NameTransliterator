namespace NameTransliterator.Helpers
{
    using System;
    using System.Globalization;
    using System.Linq;
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

        public static string CapitalizeStringFirstChar(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return str.First().ToString().ToUpper() + str.Substring(1);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string CapitalizeEachWord(this string str)
        {
            TextInfo textInfo = new CultureInfo("bg-BG", false).TextInfo;

            str = textInfo.ToTitleCase(str);

            return str;
        }
    }
}
