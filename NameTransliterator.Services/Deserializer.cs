namespace NameTransliterator.Services
{
    using System;
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

                //TODO: Implement logic that will parse first line of the text file into SourceLanguage and TargetLanguage, e. g. <Bulgarian - English>
                transliterationModel.SourceLanguage = "bulgarian";
                transliterationModel.TargetLanguage = "english";

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
                            !transliterationModel.TransliterationRegexDictionary.ContainsKey(keyValueArray[0]))
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
    }
}
