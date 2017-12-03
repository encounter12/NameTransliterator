using NameTransliterator.Models.SystemModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace NameTransliterator.Models.DomainModels
{
    public class TransliterationDictionary : AuditableEntity
    {
        public int Id { get; set; }

        [ForeignKey("Name")]
        public int SourceNameId { get; set; }

        public virtual Name SourceName { get; set; }

        [ForeignKey("Name")]
        public int? TargetNameId { get; set; }

        public virtual Name TargetName { get; set; }

        public int? TransliterationModelId { get; set; }

        public virtual TransliterationModel TransliterationModel { get; set; }
    }
}
