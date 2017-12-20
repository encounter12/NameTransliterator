using System.Collections.Generic;
using NameTransliterator.Models.DomainModels;
using NameTransliterator.Models.ViewModels;

namespace NameTransliterator.Services.Abstractions
{
    public interface INameTransliterator
    {
        List<SourceLanguageViewModel> GetSourceLanguages();

        // List<SourceLanguageViewModel> GetSourceLanguages(List<TransliterationModel> transliterationModels);

        List<TargetLanguageViewModel> GetTargetLanguages();

        // List<TargetLanguageViewModel> GetTargetLanguages(List<TransliterationModel> transliterationModels);

        string GetTransliteratedName(
            string nameForTransliteration,
            int sourceLanguageId,
            int targetLanguageId,
            string sourceLanguageName);

        //string TransliterateName(TransliterationModel transliterationModel, string nameForTransliteration);

        //string TransliterateName(IDictionary<string, string> namesDictionary, string nameForTransliteration);

        //List<TransliterationModel> LoadTransliterationModels();

        //List<TransliterationModel> LoadTransliterationModelsFromTextFiles();
    }
}
