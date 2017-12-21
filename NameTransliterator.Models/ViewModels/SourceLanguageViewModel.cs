namespace NameTransliterator.Models.ViewModels
{
    using System.Linq;
    using System.Collections.Generic;

    public class SourceLanguageViewModel : ILanguageViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public List<int?> TargetLanguageIds { get; set; }

        public IEnumerable<TargetLanguageViewModel> TargetLanguages { get; set; }
    }
}
