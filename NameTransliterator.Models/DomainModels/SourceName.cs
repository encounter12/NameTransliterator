namespace NameTransliterator.Models.DomainModels
{
    public class SourceName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TargetNameId { get; set; }

        public virtual TargetName TargetName { get; set; }

        public int TransliterationModelId { get; set; }

        public virtual TransliterationModel TransliterationModel { get; set; }
    }
}
