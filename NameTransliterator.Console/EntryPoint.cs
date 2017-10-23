namespace NameTransliterator.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NameTransliterator.Services;
    using NameTransliterator.Services.Models;

    public class EntryPoint
    {
        private static List<NameTransliterationModel> cachedTransliterationModels;

        public static void Main(string[] args)
        {
            var validators = new Validators();

            var languageSets = new List<LanguageSet>();

            try
            {
                languageSets = GetLanguageSets();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

            var sourceLanguages = languageSets.Select(ls => ls.SourceLanguage).ToList();

            int selectedSourceLanguageId = GetLanguageIdFromUser(sourceLanguages, LanguageType.Source);

            Console.WriteLine();

            var targetLanguages = languageSets
                .Where(ls => ls.SourceLanguage.Id == selectedSourceLanguageId)
                .Select(ls => ls.TargetLanguage)
                .ToList();

            int targetLanguageId = GetLanguageIdFromUser(targetLanguages, LanguageType.Target);

            //TODO: Create UserTransliterationInputModel and use data annotations and self-validating models (implement IValidatableObject), 
            // see: https://stackoverflow.com/a/3783328, and https://stackoverflow.com/a/29327343

            Console.WriteLine();

            var selectedLanguageSet = languageSets.FirstOrDefault(ls => ls.SourceLanguage.Id == selectedSourceLanguageId);

            if (selectedLanguageSet == null)
            {
                Console.WriteLine("The language set does not exist");
                Environment.Exit(1);
            }

            string nameForTransliteration = GetTransliterationNameFromUser(validators);

            var transliteratedName = 
                GetTransliteratedName(nameForTransliteration, selectedLanguageSet.Id, selectedLanguageSet.SourceLanguage.Name);

            Console.WriteLine(
                "Name transliterated ({0} - {1}): {2}",
                selectedLanguageSet.SourceLanguage.Name,
                selectedLanguageSet.TargetLanguage.Name, transliteratedName);
        }

        public static int GetLanguageIdFromUser(List<Language> sourceLanguages, LanguageType languageType)
        {
            bool selectedLanguageValid = false;

            int selectedLanguageId = 0;

            do
            {
                DisplayMenu(sourceLanguages, languageType);

                try
                {
                    selectedLanguageId = int.Parse(Console.ReadLine());

                    selectedLanguageValid = true;
                }
                catch (Exception ex)
                {
                    selectedLanguageValid = false;

                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }

            } while (!selectedLanguageValid);

            return selectedLanguageId;
        }

        public static void DisplayMenu(List<Language> languages, LanguageType languageType)
        {
            Console.WriteLine("Select {0} language:", languageType.ToString().ToLower());

            Console.WriteLine();

            foreach (var language in languages)
            {
                Console.WriteLine("{0}. {1}", language.Id, language.Name);
            }
        }

        public static string GetTransliterationNameFromUser(Validators validators)
        {
            string nameForTransliteration = null;

            bool nameValid = false;

            do
            {
                //TODO: Create UserTransliterationGetModel and use data annotations 
                try
                {
                    Console.WriteLine("Name for transliteration:");

                    nameForTransliteration = Console.ReadLine().Trim().ConvertMultipleWhitespacesToSingleSpaces().ToLower();

                    validators.ValidateName(nameForTransliteration);

                    nameValid = true;
                }
                catch (Exception ex)
                {
                    nameValid = false;

                    Console.WriteLine(ex.Message);

                    Console.WriteLine();
                }
            } while (!nameValid);

            return nameForTransliteration;
        }

        public static List<LanguageSet> GetLanguageSets()
        {
            var nameTransliterator = new NameTransliterator();

            try
            {
                cachedTransliterationModels = nameTransliterator.LoadTransliterationModels();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            List<LanguageSet> languageSets = nameTransliterator.GetLanguageSets(cachedTransliterationModels);

            return languageSets;
        }

        public static string GetTransliteratedName(
            string nameForTransliteration, 
            int selectedLanguageSetId, 
            string sourceLanguageName)
        {
            if (sourceLanguageName.ToLower() == "english")
            {
                string transliteratedNameFromDict;

                //bool nameExistInDictionary = NameTransliteratorCollections
                //    .LatinCyrillicNamesDictionary
                //    .TryGetValue(nameForTransliteration, out transliteratedNameFromDict);

                //if (nameExistInDictionary)
                //{
                //    return transliteratedNameFromDict.CapitalizeEachWord();
                //}
            }

            var transliterationModels = new List<NameTransliterationModel>();

            var nameTransliterator = new NameTransliterator();

            if (cachedTransliterationModels == null || cachedTransliterationModels.Count == 0)
            {
                try
                {
                    cachedTransliterationModels = nameTransliterator.LoadTransliterationModels();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            var searchedTransliterationModel = cachedTransliterationModels
                .FirstOrDefault(tm => tm.LanguageSet.Id == selectedLanguageSetId);

            if (searchedTransliterationModel == null)
            {
                throw new Exception("The searchedTransliterationModel is null.");
            }

            string transliteratedName = string.Empty;

            try
            {
                transliteratedName =
                    nameTransliterator.TransliterateName(searchedTransliterationModel, nameForTransliteration);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return transliteratedName;
        }
    }
}
