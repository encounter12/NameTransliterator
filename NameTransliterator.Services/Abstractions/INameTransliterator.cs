using System.Collections.Generic;

using NameTransliterator.Models.ViewModels;

namespace NameTransliterator.Services.Abstractions
{
    public interface INameTransliterator
    {
        List<SourceLanguageViewModel> GetSourceLanguages();

        List<TargetLanguageViewModel> GetTargetLanguages();

        string GetTransliteratedName(
            string nameForTransliteration,
            int sourceLanguageId,
            int targetLanguageId,
            string sourceLanguageName);
    }
}
