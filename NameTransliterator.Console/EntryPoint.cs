namespace NameTransliterator.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NameTransliterator.Services;
    using NameTransliterator.Services.Models;

    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            var nameTransliterator = new NameTransliterator();

            var validators = new Validators();

            var transliterationModels = new List<NameTransliterationModel>();

            try
            {
                transliterationModels = nameTransliterator.LoadTransliterationModels();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Environment.Exit(1);
            }

            string sourceInputLanguage = GetLanguageFromUser(LanguageType.Source, validators);

            Console.WriteLine();

            string targetInputLanguage = GetLanguageFromUser(LanguageType.Target, validators);

            //TODO: Create UserTransliterationGetModel and use data annotations and self-validating models (implement IValidatableObject), 
            // see: https://stackoverflow.com/a/3783328, and https://stackoverflow.com/a/29327343

            try
            {
                validators.ValidateSourceAndTargetLanguageDontMatch(sourceInputLanguage, targetInputLanguage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

            Console.WriteLine();

            string nameForTransliteration = GetTransliterationNameFromUser(validators);

            var searchedTransliterationModel = transliterationModels
                .FirstOrDefault(tm => tm.SourceLanguage == sourceInputLanguage && tm.TargetLanguage == targetInputLanguage);

            string transliteratedName = string.Empty;

            if (searchedTransliterationModel == null)
            {
                Console.WriteLine("The searchedTransliterationModel is null.");
                Environment.Exit(1);
            }

            try
            {
                transliteratedName =
                    nameTransliterator.TransliterateName(searchedTransliterationModel, nameForTransliteration);

                Console.WriteLine(
                    "Name transliterated ({0} - {1}): {2}",
                    searchedTransliterationModel.SourceLanguage, searchedTransliterationModel.TargetLanguage, transliteratedName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Environment.Exit(1);
            }
        }

        public static string GetLanguageFromUser(LanguageType languageType, Validators validators)
        {
            string prompt = string.Format("Enter {0} language:", languageType.ToString().ToLower());

            string inputLanguage = null;

            bool currentLanguageValid = false;

            do
            {
                try
                {
                    Console.WriteLine(prompt);

                    inputLanguage = Console.ReadLine().Trim().ConvertMultipleWhitespacesToSingleSpaces().ToLower();

                    validators.ValidateLanguage(inputLanguage, languageType);

                    currentLanguageValid = true;
                }
                catch (Exception ex)
                {
                    currentLanguageValid = false;

                    Console.WriteLine(ex.Message);

                    Console.WriteLine();
                }
            } while (!currentLanguageValid);

            return inputLanguage;
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
    }
}
