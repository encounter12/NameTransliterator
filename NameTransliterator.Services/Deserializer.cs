namespace NameTransliterator.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Models;

    public class Deserializer
    {
        public NameTransliterationModel Deserialize(string filePath)
        {
            var transliterationModel = new NameTransliterationModel();

            using (StreamReader reader = File.OpenText(filePath))
            {
                String currentLine;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    currentLine = currentLine.TrimAndRemoveExtraWhiteSpaces().ToLower();

                    string[] keyValueArray = currentLine.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

                    if (keyValueArray != null &&
                        keyValueArray.Length >= 2 &&
                        !string.IsNullOrEmpty(keyValueArray[0]) &&
                        !string.IsNullOrEmpty(keyValueArray[1]))
                    {
                        bool isKeyValidRegexPattern = IsRegexPatternValid(keyValueArray[0]);
                        bool isValueValidRegexPattern = IsRegexPatternValid(keyValueArray[1]);

                        bool keyContainsLettersOrDigitsOnly = keyValueArray[0].All(Char.IsLetterOrDigit);

                        bool valueContainsLettersOrDigitsOnly = keyValueArray[1].All(Char.IsLetterOrDigit);

                        if (!keyContainsLettersOrDigitsOnly &&
                            isKeyValidRegexPattern &&
                            !transliterationModel.TransliterationRegexDictionary.ContainsKey(keyValueArray[0]));
                        {
                            transliterationModel.TransliterationRegexDictionary.Add(keyValueArray[0], keyValueArray[1]);
                        }

                        if (keyContainsLettersOrDigitsOnly &&
                            valueContainsLettersOrDigitsOnly &&
                            !transliterationModel.TransliterationDictionary.ContainsKey(keyValueArray[0]))
                        {
                            transliterationModel.TransliterationDictionary.Add(keyValueArray[0], keyValueArray[1]);
                        }
                    }
                }
            }

            IComparer<string> lengthComparer = new LengthComparer();

            transliterationModel.SwappedTransliterationDictionary = 
                this.SwapDictionaryKeysWithValues(transliterationModel.TransliterationDictionary, lengthComparer);

            transliterationModel.SwappedTransliterationRegexDictionary =
                this.SwapDictionaryKeysWithValues(transliterationModel.SwappedTransliterationRegexDictionary, lengthComparer);

            return transliterationModel;
        }

        public static bool IsRegexPatternValid(string pattern)
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

        public SortedDictionary<string, string> SwapDictionaryKeysWithValues(
            SortedDictionary<string, string> initialDictionary,
            IComparer<string> comparer)
        {
            var swappedDictionary = new SortedDictionary<string, string>(comparer);

            foreach (var item in initialDictionary)
            {
                if (!swappedDictionary.ContainsKey(item.Value))
                {
                    swappedDictionary.Add(item.Value, item.Key);
                }
            }

            return swappedDictionary;
        }

        public SortedDictionary<string, string> SwapDictionaryKeysWithValues(
            IDictionary<string, string> initialDictionary,
            IComparer<string> comparer)
        {
            var swappedDictionary = new SortedDictionary<string, string>(comparer);

            foreach (var item in initialDictionary)
            {
                if (!swappedDictionary.ContainsKey(item.Value))
                {
                    swappedDictionary.Add(item.Value, item.Key);
                }
            }

            return swappedDictionary;
        }
    }
}
