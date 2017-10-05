namespace NameTransliterator.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using NameTransliterator.Services;
    using NameTransliterator.Services.Models;

    public class EntryPoint
    {
        private static NameTransliterator nameTransliterator;

        private static NameTransliterationModel transliterationModel;

        public static void Main(string[] args)
        {
            InitializeTransliteration();

            Console.WriteLine("Enter source language:");

            string sourceLanguage = Console.ReadLine();

            sourceLanguage = sourceLanguage.ToLower();

            Console.WriteLine("Enter target language:");

            string targetLanguage = Console.ReadLine();

            targetLanguage = targetLanguage.ToLower();

            Console.WriteLine("Name for transliteration:");

            string nameForTransliteration = Console.ReadLine();

            bool sourceLanguageValid = Enum.GetNames(typeof(Languages)).Contains(sourceLanguage);

            bool targetLanguageValid = Enum.GetNames(typeof(Languages)).Contains(targetLanguage);

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

            var searchedTransliterationModel = transliterationModels
                .FirstOrDefault(tm => tm.SourceLanguage == sourceLanguage && tm.TargetLanguage == targetLanguage);

            string transliteratedName = null;

            if (sourceLanguageValid && targetLanguageValid && searchedTransliterationModel != null)
            {
                transliteratedName = nameTransliterator.TransliterateName(searchedTransliterationModel, nameForTransliteration);

                Console.WriteLine("Name transliterated ({0} - {1}): {2}",
                    searchedTransliterationModel.SourceLanguage, searchedTransliterationModel.TargetLanguage, transliteratedName);
            }
            else if (!sourceLanguageValid && !targetLanguageValid && searchedTransliterationModel == null)
            {
                Console.WriteLine("Source and target languages are not valid. NameTransliterationModel is null. Please, try again ....");
            }
            else if (!sourceLanguageValid)
            {
                Console.WriteLine("Source language is not valid. Please, try again ....");
            }
            else if (!targetLanguageValid)
            {
                Console.WriteLine("Target language is not valid. Please, try again ....");
            }
            else if (searchedTransliterationModel == null)
            {
                Console.WriteLine("NameTransliterationModel is null. Please, try again ....");
            }
        }

        public static void InitializeTransliteration()
        {
            var relativeFilePath = "TransliterationSets";

            var transliterationDictionaryFileName = "Bulgarian-English.txt";

            var fullPath = GetFileFullPath(relativeFilePath, transliterationDictionaryFileName);

            var deserializer = new Deserializer();

            transliterationModel = deserializer.Deserialize(fullPath);

            nameTransliterator = new NameTransliterator();
        }

        public static string GetFileFullPath(string relativeFilePath, string fileName)
        {
            // var currentAssemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var currentAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var fileFullPath = Path.Combine(currentAssemblyDirectory, relativeFilePath, fileName);

            return fileFullPath;
        }
    }
}
