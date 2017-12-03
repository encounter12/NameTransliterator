namespace NameTransliterator.Models.DomainModels
{
    using NameTransliterator.Models.SystemModels;

    public class Language : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
