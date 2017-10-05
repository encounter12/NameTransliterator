namespace NameTransliterator.Services
{
    using System;
    using System.Text.RegularExpressions;

    using Models;

    public class NameTransliterator
    {
        public string TransliterateName(NameTransliterationModel transliterationModel, string nameForTransliteration)
        {
            string transliteratedName = String.Copy(nameForTransliteration);

            foreach (var item in transliterationModel.TransliterationRegexDictionary)
            {
                string pattern = item.Key;
                transliteratedName = Regex.Replace(transliteratedName, pattern, item.Value, RegexOptions.IgnoreCase);
            }

            foreach (var item in transliterationModel.TransliterationDictionary)
            {
                transliteratedName = transliteratedName.Replace(item.Key, item.Value);
            }

            return transliteratedName;
        }
    }
}
