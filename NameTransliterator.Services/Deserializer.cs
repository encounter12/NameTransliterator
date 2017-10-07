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

                var validators = new Validators();

                int lineCounter = 1;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    currentLine = currentLine
                        .Trim(new char[] { ' ', '<', '>', '(', ')', '[', ']'})
                        .ConvertMultipleWhitespacesToSingleSpaces()
                        .ToLower();

                    string[] keyValueArray = currentLine.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

                    if (keyValueArray != null && keyValueArray.Length == 2)
                    {
                        keyValueArray = keyValueArray.Select(s => s.Trim().ToLowerInvariant()).ToArray();

                        if (validators.IsArrayValidLanguageSet(keyValueArray))
                        {
                            transliterationModel.SourceLanguage = keyValueArray[0];
                            transliterationModel.TargetLanguage = keyValueArray[1];
                        }
                        else
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
                    else if (keyValueArray == null)
                    {
                        throw new ArgumentException("The array from the splitted line is null");
                    }
                    else if (keyValueArray.Length != 2)
                    {
                        string errorMessage = string.Format("Line {0} from transliteration model should consist of exactly 2 elements", lineCounter);

                        throw new ArgumentException(errorMessage);
                    }

                    lineCounter++;
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
