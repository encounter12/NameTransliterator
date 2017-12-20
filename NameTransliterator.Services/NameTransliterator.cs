using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

using NameTransliterator.Data.UnitOfWork;
using NameTransliterator.Models.DomainModels;
using NameTransliterator.Models.ViewModels;
using NameTransliterator.Services.Abstractions;

namespace NameTransliterator.Services
{
    public class NameTransliterator : INameTransliterator
    {
        private readonly IUnitOfWork unitOfWork;

        public NameTransliterator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public string GetTransliteratedName(
            string nameForTransliteration,
            int sourceLanguageId,
            int targetLanguageId,
            string sourceLanguageName)
        {
            if (sourceLanguageName.ToLower() == "english")
            {
                bool nameExistInDictionary = NameTransliteratorCollections
                    .LatinCyrillicNamesDictionary
                    .TryGetValue(nameForTransliteration, out string transliteratedNameFromDict);

                if (nameExistInDictionary)
                {
                    return transliteratedNameFromDict.CapitalizeEachWord();
                }
            }

            IQueryable<TransliterationModel> transliterationModels = null;

            try
            {
                transliterationModels = this.LoadTransliterationModels();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var searchedTransliterationModel = transliterationModels
                .Include(tm => tm.TransliterationRules)
                .FirstOrDefault(
                    tm =>
                        tm.SourceLanguageId == sourceLanguageId &&
                        tm.TargetLanguageId == targetLanguageId &&
                        !tm.IsDeleted);

            if (searchedTransliterationModel == null)
            {
                throw new Exception("The searchedTransliterationModel is null.");
            }

            string transliteratedName = string.Empty;

            try
            {
                transliteratedName =
                    this.TransliterateName(searchedTransliterationModel, nameForTransliteration);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return transliteratedName;
        }

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

            var sortedTransliterationRules = transliterationModel.TransliterationRules.OrderBy(tr => tr.ExecutionOrder);

            foreach (var transliterationRule in sortedTransliterationRules)
            {
                string pattern = transliterationRule.SourceExpression;
                transliteratedName = 
                    Regex.Replace(transliteratedName, pattern, transliterationRule.TargetExpression, RegexOptions.IgnoreCase);
            }

            transliteratedName = transliteratedName.CapitalizeEachWord();

            return transliteratedName;
        }

        public List<SourceLanguageViewModel> GetSourceLanguages()
        {
            IQueryable<TransliterationModel> transliterationModels = null;

            try
            {
                transliterationModels = this.LoadTransliterationModels();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var sourceLanguages = new List<SourceLanguageViewModel>();

            sourceLanguages = transliterationModels
                .GroupBy(tm => tm.SourceLanguageId)
                .Select(group => new SourceLanguageViewModel
                {
                    Id = group.Key,
                    Name = group.Select(g => g.SourceLanguage.Name).First(),
                    TargetLanguageIds = group.Select(g => g.TargetLanguageId).ToList()
                })
                .OrderBy(tm => tm.Name)
                .ToList();

            return sourceLanguages;
        }

        public List<TargetLanguageViewModel> GetTargetLanguages()
        {
            IQueryable<TransliterationModel> transliterationModels = null;

            try
            {
                transliterationModels = this.LoadTransliterationModels();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var targetLanguages = new List<TargetLanguageViewModel>();

            targetLanguages = transliterationModels.Select(tm => new TargetLanguageViewModel
            {
                Id = tm.TargetLanguageId,
                Name = tm.TargetLanguage.Name
            })
            .ToList();

            return targetLanguages;
        }

        public IQueryable<TransliterationModel> LoadTransliterationModels()
        {  
            // return this.LoadTransliterationModelsFromTextFiles();
            return this.unitOfWork.TransliterationModelRepository.All().Where(tm => tm.IsOfficial);
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
