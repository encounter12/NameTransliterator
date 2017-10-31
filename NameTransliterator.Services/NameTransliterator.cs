﻿using NameTransliterator.Models.DomainModels;

namespace NameTransliterator.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class NameTransliterator
    {
        public string TransliterateName(TransliterationModel transliterationModel, string nameForTransliteration)
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

        public List<LanguagePair> GetLanguagePairs(List<TransliterationModel> transliterationModels)
        {
            var languagePairs = new List<LanguagePair>();

            languagePairs = transliterationModels.Select(tm => tm.LanguagePair).ToList();

            return languagePairs;
        }

        public string TransliterateName(IDictionary<string, string> namesDictionary, string nameForTransliteration)
        {
            string transliteratedName;

            bool nameExists = namesDictionary.TryGetValue(nameForTransliteration, out transliteratedName);

            return transliteratedName;
        }

        public List<TransliterationModel> LoadTransliterationModels()
        {
            var transliterationModels = new List<TransliterationModel>();
  
            // transliterationModels = this.LoadTransliterationModelsFromTextFiles();
            transliterationModels = TransliterationModelsData.GetTransliterationModels();

            return transliterationModels;
        }

        public List<TransliterationModel> LoadTransliterationModelsFromTextFiles()
        {
            var relativeFilePath = "TransliterationSetTextFiles";

            // var currentAssemblyDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            var currentAssemblyDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string transliterationFilesDirPath = Path.Combine(currentAssemblyDirectoryPath, relativeFilePath);

            IEnumerable<string> files = 
                Directory.EnumerateFiles(transliterationFilesDirPath, "*.txt", SearchOption.AllDirectories);

            var deserializer = new Deserializer();

            var transliterationModels = new List<TransliterationModel>();

            int fileCounter = 1;

            foreach (var file in files)
            {
                try
                {
                    var transliterationModel = new TransliterationModel();

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
