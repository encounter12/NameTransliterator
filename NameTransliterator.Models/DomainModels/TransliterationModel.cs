namespace NameTransliterator.Models.DomainModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using NameTransliterator.Models.SystemModels;
    using NameTransliterator.Models.IdentityModels;

    public class TransliterationModel : AuditableEntity
    {
        private ICollection<TransliterationRule> transliterationRules;

        public TransliterationModel()
        {
            this.TransliterationRules = new HashSet<TransliterationRule>();
        }

        public int Id { get; set; }

        public virtual ICollection<TransliterationRule> TransliterationRules
        {
            get { return this.transliterationRules; }
            set { this.transliterationRules = value; }
        }

        [ForeignKey("Language")]
        public int OriginLanguageId { get; set; }

        public virtual Language OriginLanguage { get; set; }

        [ForeignKey("Language")]
        public int? SourceAlphabetId { get; set; }

        public virtual Language SourceAlphabet { get; set; }

        [ForeignKey("Language")]
        public int? TargetAlphabetId { get; set; }

        public virtual Language TargetAlphabet { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public int TransliterationTypeId { get; set; }

        public virtual TransliterationType TransliterationType { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public bool IsOfficial { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
