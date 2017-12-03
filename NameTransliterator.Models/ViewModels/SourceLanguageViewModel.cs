namespace NameTransliterator.Models.ViewModels
{
    using System.Collections.Generic;

    public class SourceLanguageViewModel : ILanguageViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public List<int?> TargetLanguageIds { get; set; }
    }
}
