namespace NameTransliterator.Models.DomainModels
{
    using NameTransliterator.Models.SystemModels;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Name : AuditableEntity
    {
        public int Id { get; set; }

        public string NameText { get; set; }

        [ForeignKey("Language")]
        public int? OriginLanguageId { get; set; }

        public virtual Language OriginLanguage { get; set; }

        [ForeignKey("Language")]
        public int AlphabetId { get; set; }

        public virtual Language Alphabet { get; set; }
    }
}
