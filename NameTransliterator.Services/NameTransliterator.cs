namespace NameTransliterator.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using Models;

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

            foreach (var item in transliterationModel.TransliterationRules)
            {
                string pattern = item.SourceExpression;
                transliteratedName = Regex.Replace(transliteratedName, pattern, item.TargetExpression, RegexOptions.IgnoreCase);
            }

            transliteratedName = transliteratedName.CapitalizeEachWord();

            return transliteratedName;
        }

        public List<LanguageSet> GetLanguageSets(List<NameTransliterationModel> transliterationModels)
        {
            var languageSets = new List<LanguageSet>();

            languageSets = transliterationModels.Select(tm => tm.LanguageSet).ToList();

            return languageSets;
        }

        public string TransliterateName(IDictionary<string, string> namesDictionary, string nameForTransliteration)
        {
            string transliteratedName;

            bool nameExists = namesDictionary.TryGetValue(nameForTransliteration, out transliteratedName);

            return transliteratedName;
        }

        public List<NameTransliterationModel> LoadTransliterationModels()
        {
            var transliterationModels = new List<NameTransliterationModel>();
  
            // transliterationModels = this.LoadTransliterationModelsFromTextFiles();
            transliterationModels = TransliterationModels.GetTransliterationModels();

            return transliterationModels;
        }

        public List<NameTransliterationModel> LoadTransliterationModelsFromTextFiles()
        {
            var relativeFilePath = "TransliterationSetTextFiles";

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

            return transliterationModels;
        }
    }
}
