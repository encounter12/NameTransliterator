using NameTransliterator.Models.SystemModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace NameTransliterator.Models.DomainModels
{
    public class TransliterationDictionary : AuditableEntity
    {
        public int Id { get; set; }

        [ForeignKey("Language")]
        public int NameOriginLanguageId { get; set; }

        public virtual Language NameOriginLanguage { get; set; }

        public string SourceName { get; set; }

        [ForeignKey("Language")]
        public int SourceNameAlphabetId { get; set; }

        public string TargetName { get; set; }

        [ForeignKey("Language")]
        public int TargetNameAlphabetId { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public int? TransliterationModelId { get; set; }

        public virtual TransliterationModel TransliterationModel { get; set; }
    }
}
