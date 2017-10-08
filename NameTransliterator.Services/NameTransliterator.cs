namespace NameTransliterator.Services
{
    using System;
    using System.Text.RegularExpressions;
    using System.Linq;

    using Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class NameTransliterator
    {
        public string TransliterateName(NameTransliterationModel transliterationModel, string nameForTransliteration)
        {
            if (transliterationModel == null)
            {
                throw new ArgumentNullException("The transliteration model is null");
            }

            if (string.IsNullOrEmpty(nameForTransliteration))
            {
                throw new ArgumentNullException("The name for transliteration is null or empty");
            }

            string transliteratedName = String.Copy(nameForTransliteration)
                .Trim().ConvertMultipleWhitespacesToSingleSpaces().ToLower();

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

        public List<LanguageSet> GetLanguageSets(List<NameTransliterationModel> transliterationModels)
        {
            var languageSets = new List<LanguageSet>();

            languageSets = transliterationModels.Select(tm => tm.LanguageSet).ToList();

            return languageSets;
        }

        public List<NameTransliterationModel> LoadTransliterationModels()
        {
            var relativeFilePath = "TransliterationSets";

            // var currentAssemblyDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            var currentAssemblyDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string transliterationFilesDirPath = Path.Combine(currentAssemblyDirectoryPath, relativeFilePath);

            IEnumerable<string> files = 
                Directory.EnumerateFiles(transliterationFilesDirPath, "*.txt", SearchOption.AllDirectories);

            var deserializer = new Deserializer();

            var transliterationModels = new List<NameTransliterationModel>();

            int fileCounter = 1;

            foreach (var file in files)
            {
                try
                {
                    var transliterationModel = new NameTransliterationModel();

                    transliterationModel = deserializer.Deserialize(file, fileCounter);

                    transliterationModels.Add(transliterationModel);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                fileCounter++;
            }

            var comparer = new LengthComparer();

            return transliterationModels;
        }

        public NameTransliterationModel GetReversedTransliterationModel(NameTransliterationModel model, IComparer<string> comparer)
        {
            var reversedModel = new NameTransliterationModel()
            {
                TransliterationDictionary = model.TransliterationDictionary.SwapDictionaryKeysWithValues(comparer),
                TransliterationRegexDictionary = new SortedDictionary<string, string>(comparer),
                LanguageSet = new LanguageSet()
                {
                    SourceLanguage = model.LanguageSet.TargetLanguage,
                    TargetLanguage = model.LanguageSet.SourceLanguage
                }
            };

            return reversedModel;
        }
    }
}
