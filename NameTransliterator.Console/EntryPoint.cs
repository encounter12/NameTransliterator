namespace NameTransliterator.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Seed;
    using NameTransliterator.DI;
    using NameTransliterator.Models.Enumerations;
    using NameTransliterator.Models.ViewModels;
    using NameTransliterator.Models.IdentityModels;
    using NameTransliterator.Services;
    using NameTransliterator.Services.Abstractions;

    public class EntryPoint
    {
        private static INameTransliterator nameTransliterator;

        public static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=NameTransliterator;Trusted_Connection=True;MultipleActiveResultSets=true"));

            services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 2;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 2;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDependencyInjection("BuiltInDependencyInjector");

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<ApplicationDbContext>().Database.Migrate();

            nameTransliterator = serviceProvider.GetService<INameTransliterator>();

            try
            {
                DatabaseInitializer.SeedData(serviceProvider).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

            var validators = new Validators();

            var sourceLanguages = new List<SourceLanguageViewModel>();

            var targetLanguages = new List<TargetLanguageViewModel>();

            try
            {
                sourceLanguages = nameTransliterator.GetSourceLanguages();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

            try
            {
                targetLanguages = nameTransliterator.GetTargetLanguages();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

            List<ILanguageViewModel> sourceLanguagesBase = sourceLanguages.ToList<ILanguageViewModel>();

            int selectedSourceLanguageId = GetLanguageIdFromUser(sourceLanguagesBase, LanguageType.Source);

            SourceLanguageViewModel selectedSourceLanguage = sourceLanguages.FirstOrDefault(sl => sl.Id == selectedSourceLanguageId);

            if (selectedSourceLanguage == null)
            {
                Console.WriteLine(string.Format("There is no source language with id: {0}", selectedSourceLanguageId));
                Environment.Exit(1);
            }

            Console.WriteLine();

            var filteredTargetLanguages = targetLanguages
                .Where(tl => selectedSourceLanguage.TargetLanguageIds.Contains(tl.Id))
                .ToList();

            List<ILanguageViewModel> filteredTargetLanguagesBase = filteredTargetLanguages.ToList<ILanguageViewModel>();

            int selectedTargetLanguageId = GetLanguageIdFromUser(filteredTargetLanguagesBase, LanguageType.Target);

            TargetLanguageViewModel selectedTargetLanguage = targetLanguages.FirstOrDefault(tl => tl.Id == selectedTargetLanguageId);

            if (selectedTargetLanguage == null)
            {
                Console.WriteLine(string.Format("There is no target language with id: {0}", selectedSourceLanguageId));
                Environment.Exit(1);
            }

            //TODO: Create UserTransliterationInputModel and use data annotations and self-validating models (implement IValidatableObject), 
            // see: https://stackoverflow.com/a/3783328, and https://stackoverflow.com/a/29327343

            Console.WriteLine();

            string nameForTransliteration = GetTransliterationNameFromUser(validators);

            var transliteratedName = nameTransliterator.GetTransliteratedName(
                nameForTransliteration, 
                selectedSourceLanguageId, 
                selectedTargetLanguageId, 
                selectedSourceLanguage.Name);

            Console.WriteLine(
                "Name transliterated ({0} - {1}): {2}",
                selectedSourceLanguage.Name,
                selectedTargetLanguage.Name, transliteratedName);
        }

        public static int GetLanguageIdFromUser(List<ILanguageViewModel> languages, LanguageType languageType)
        {
            bool selectedLanguageValid = false;

            int selectedLanguageId = 0;

            do
            {
                DisplayMenu(languages, languageType);

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

        public static void DisplayMenu(List<ILanguageViewModel> languages, LanguageType languageType)
        {
            Console.WriteLine("Select {0} language (choose language number):", languageType.ToString().ToLower());

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
    }
}

