namespace NameTransliterator.Services.Models
{
    public class LanguageSet
    {
        public int Id { get; set; }

        public int SourceLanguageId { get; set; }

        public virtual Language SourceLanguage { get; set; }

        public int TargetLanguageId { get; set; }

        public virtual Language TargetLanguage { get; set; }
    }
}
