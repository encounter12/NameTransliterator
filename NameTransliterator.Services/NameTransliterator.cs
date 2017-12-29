using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using NameTransliterator.Data.UnitOfWork;
using NameTransliterator.Helpers;
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

            var transliterationModelOfficial = true;
            var transliterationModelActive = true;

            var selectedModelTransliterationRules = this.unitOfWork
                .TransliterationRuleRepository
                .GetSelectedModelTransliterationRules(
                    transliterationModelOfficial, 
                    transliterationModelActive, 
                    sourceLanguageId, 
                    targetLanguageId);

            if (selectedModelTransliterationRules == null)
            {
                throw new Exception("The searchedTransliterationModel is null.");
            }

            string transliteratedName = string.Empty;

            try
            {
                transliteratedName =
                    this.TransliterateName(selectedModelTransliterationRules, nameForTransliteration);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return transliteratedName;
        }

        private string TransliterateName(IQueryable<TransliterationRule> transliterationRules, string nameForTransliteration)
        {
            if (transliterationRules == null)
            {
                throw new ArgumentNullException("The transliteration rules are null");
            }

            if (string.IsNullOrEmpty(nameForTransliteration))
            {
                throw new ArgumentNullException("The name for transliteration is null or empty");
            }

            string transliteratedName = String.Copy(nameForTransliteration)
                .Trim().ConvertMultipleWhitespacesToSingleSpaces().ToLower();

            foreach (var transliterationRule in transliterationRules)
            {
                string pattern = transliterationRule.SourceExpression;

                transliteratedName = Regex.Replace(
                    transliteratedName, 
                    pattern, 
                    transliterationRule.TargetExpression, 
                    RegexOptions.IgnoreCase);
            }

            transliteratedName = transliteratedName.CapitalizeEachWord();

            return transliteratedName;
        }

        public List<SourceLanguageViewModel> GetSourceLanguages()
        {
            var transliterationModelOfficial = true;

            var transliterationModelActive = true;

            var sourceLanguages = this.unitOfWork
                .TransliterationModelRepository
                .GetSourceAlphabets(transliterationModelOfficial, transliterationModelActive)
                .ToList();

            return sourceLanguages;
        }
    }
}
