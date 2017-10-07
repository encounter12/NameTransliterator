namespace NameTransliterator.Services
{
    using System;
    using System.Linq;

    using Models;

    public class Validators
    {
        public void ValidateLanguage(string inputLanguage, LanguageType languageType)
        {
            bool languageValid = Enum.GetNames(typeof(Languages)).Contains(inputLanguage);

            if (!languageValid)
            {
                var errorMessage = string.Format(
                    "The {0} language is not valid. Current value: {1}", 
                    languageType.ToString().ToLower(), 
                    inputLanguage);

                throw new ArgumentException(errorMessage);
            }
        }

        public void ValidateSourceAndTargetLanguageDontMatch(string sourceLanguage, string targetLanguage)
        {
            bool sourceAndTargetLanguageMatch = (sourceLanguage == targetLanguage);

            if (sourceAndTargetLanguageMatch)
            {
                throw new ArgumentException("Source and target languages should be different.");
            }
        }

        public bool IsArrayValidLanguageSet(string[] languageSet)
        {
            bool sourceLanguageValid = false;
            bool targetLanguageValid = false;

            if (languageSet != null && languageSet.Length >= 2)
            {
                sourceLanguageValid = Enum.GetNames(typeof(Languages)).Contains(languageSet[0]);
                targetLanguageValid = Enum.GetNames(typeof(Languages)).Contains(languageSet[1]);
            }

            return sourceLanguageValid && targetLanguageValid;
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name is not valid.");
            }
        }
    }
}
