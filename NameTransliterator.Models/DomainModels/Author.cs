namespace NameTransliterator.Models.DomainModels
{
    using NameTransliterator.Models.SystemModels;

    public class Author : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
