namespace NameTransliterator.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Models;

    public class Deserializer
    {
        public NameTransliterationModel Deserialize(string filePath, int languageSetId)
        {
            var transliterationModel = new NameTransliterationModel();

            using (StreamReader reader = File.OpenText(filePath))
            {
                String currentLine;

                var validators = new Validators();

                int lineCounter = 1;

                bool isLanguageSetSpecified = false;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    currentLine = currentLine
                        .Trim(new char[] { ' ', '<', '>', '(', ')' })
                        .ConvertMultipleWhitespacesToSingleSpaces()
                        .ToLowerInvariant();

                    string[] keyValueArray = currentLine.Split(new string[] { " : ", ":" }, StringSplitOptions.RemoveEmptyEntries);

                    if (keyValueArray != null && keyValueArray.Length == 2)
                    {
                        keyValueArray = keyValueArray.Select(s => s.Trim(new char[] { '"' }).Trim()).ToArray();

                        string modelSourceLanguage = keyValueArray[0].CapitalizeStringFirstChar();
                        string modelTargetLanguage = keyValueArray[1].CapitalizeStringFirstChar();

                        Language sourceLanguage = TestData.Languages.FirstOrDefault(l => l.Name == modelSourceLanguage);

                        Language targetLanguage = TestData.Languages.FirstOrDefault(l => l.Name == modelTargetLanguage);

                        //checks if the current line elements are languages
                        //TODO: Set markup specifying the language set line (e.g. <Bulgarian - English>)
                        if (sourceLanguage != null && targetLanguage != null)
                        {
                            transliterationModel.LanguageSet = new LanguageSet()
                            {
                                Id = languageSetId,
                                SourceLanguage = sourceLanguage,
                                TargetLanguage = targetLanguage
                            };

                            isLanguageSetSpecified = true;
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

                if (!isLanguageSetSpecified)
                {
                    throw new ArgumentNullException("The language set is not specified");
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
