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

            string transliteratedName = String.Copy(nameForTransliteration).Trim().ToLower();

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

        public List<NameTransliterationModel> LoadTransliterationModels()
        {
            var relativeFilePath = "TransliterationSets";

            var transliterationDictionaryFileName = "Bulgarian-English.txt";

            NameTransliterationModel transliterationModel = GetTransliterationModel(relativeFilePath, transliterationDictionaryFileName);

            var transliterationModels = new List<NameTransliterationModel>()
            {
                transliterationModel
            };

            var comparer = new LengthComparer();

            var reversedModels = new List<NameTransliterationModel>();

            foreach (var model in transliterationModels)
            {
                bool reversedTransliterationSetExists = transliterationModels.Any(
                    tm => tm.SourceLanguage == model.TargetLanguage && tm.TargetLanguage == model.SourceLanguage);

                if (!reversedTransliterationSetExists)
                {
                    var reversedModel = new NameTransliterationModel()
                    {
                        TransliterationDictionary = model.TransliterationDictionary.SwapDictionaryKeysWithValues(comparer),
                        TransliterationRegexDictionary = new SortedDictionary<string, string>(comparer),
                        SourceLanguage = model.TargetLanguage,
                        TargetLanguage = model.SourceLanguage
                    };

                    reversedModels.Add(reversedModel);
                }
            }

            transliterationModels.AddRange(reversedModels);

            return transliterationModels;
        }

        public static NameTransliterationModel GetTransliterationModel(string relativeFilePath, string transliterationDictionaryFileName)
        {
            var fullPath = GetFileFullPath(relativeFilePath, transliterationDictionaryFileName);

            var deserializer = new Deserializer();

            NameTransliterationModel transliterationModel = new NameTransliterationModel();

            try
            {
                transliterationModel = deserializer.Deserialize(fullPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return transliterationModel;
        }

        private static string GetFileFullPath(string relativeFilePath, string fileName)
        {
            // var currentAssemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var currentAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var fileFullPath = Path.Combine(currentAssemblyDirectory, relativeFilePath, fileName);

            return fileFullPath;
        }
    }
}
