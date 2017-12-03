using NameTransliterator.Models.DomainModels;

namespace NameTransliterator.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Deserializer
    {
        public TransliterationModel Deserialize(string filePath, int languagePairId)
        {
            var transliterationModel = new TransliterationModel();

            using (StreamReader reader = File.OpenText(filePath))
            {
                String currentLine;

                var validators = new Validators();

                int lineCounter = 1;

                bool isLanguagePairSpecified = false;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    currentLine = currentLine
                        .Trim(new char[] { ' ', '<', '>', '(', ')' })
                        .ConvertMultipleWhitespacesToSingleSpaces()
                        .ToLowerInvariant();

                    if (lineCounter == 1)
                    {
                        string[] languagePairArray = 
                            currentLine.Split(new string[] { " : ", ":", ": ", " :" }, StringSplitOptions.RemoveEmptyEntries);

                        languagePairArray = languagePairArray.Select(s => s.Trim(new char[] { '"' }).Trim()).ToArray();

                        if (languagePairArray != null && languagePairArray.Length == 2)
                        {
                            string modelSourceLanguage = languagePairArray[0].CapitalizeStringFirstChar();
                            string modelTargetLanguage = languagePairArray[1].CapitalizeStringFirstChar();

                            Language sourceLanguage = TestData.Languages.FirstOrDefault(l => l.Name == modelSourceLanguage);

                            Language targetLanguage = TestData.Languages.FirstOrDefault(l => l.Name == modelTargetLanguage);

                            //checks if the current line elements are languages
                            //TODO: Set markup specifying the language set line (e.g. <Bulgarian - English>)
                            if (sourceLanguage != null && targetLanguage != null)
                            {
                                transliterationModel.SourceLanguage = sourceLanguage;
                                transliterationModel.SourceLanguageId = sourceLanguage.Id;

                                transliterationModel.TargetLanguage = targetLanguage;
                                transliterationModel.TargetLanguageId = targetLanguage.Id;

                                isLanguagePairSpecified = true;
                            }
                        }
                        else if (languagePairArray == null)
                        {
                            throw new ArgumentException("The language sets array is null");
                        }
                        else if (languagePairArray.Length != 2)
                        {
                            throw new ArgumentException(
                                string.Format("The language sets array on line {0} should contain 2 elements", lineCounter));
                        }
                    }
                    else
                    {
                        string[] transliterationRuleArray =
                            currentLine.Split(new string[] { " : ", ":", ": ", " :" }, StringSplitOptions.RemoveEmptyEntries);

                        transliterationRuleArray = transliterationRuleArray.Select(s => s.Trim(new char[] { '"' }).Trim()).ToArray();

                        if (transliterationRuleArray != null && transliterationRuleArray.Length == 3)
                        {
                            bool isKeyValidRegexPattern = IsRegexPatternValid(transliterationRuleArray[0]);
                            bool isValueValidRegexPattern = IsRegexPatternValid(transliterationRuleArray[1]);
                            bool transliterationRuleNotExist =
                                !transliterationModel.TransliterationRules.Any(rule => rule.SourceExpression == transliterationRuleArray[0]);

                            if (isKeyValidRegexPattern && isValueValidRegexPattern && transliterationRuleNotExist)
                            {
                                var transliterationRule = new TransliterationRule()
                                {
                                    SourceExpression = transliterationRuleArray[0],
                                    TargetExpression = transliterationRuleArray[1],
                                    ExecutionOrder = int.Parse(transliterationRuleArray[2])
                                };

                                transliterationModel.TransliterationRules.Add(transliterationRule);
                            }
                        }
                        else if (transliterationRuleArray == null)
                        {
                            throw new ArgumentException("The array from the splitted line is null");
                        }
                        else if (transliterationRuleArray.Length != 3)
                        {
                            string errorMessage = string.Format("Line {0} from transliteration model should consist of exactly 3 elements", lineCounter);

                            throw new ArgumentException(errorMessage);
                        }
                    }

                    lineCounter++;
                }

                if (!isLanguagePairSpecified)
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
