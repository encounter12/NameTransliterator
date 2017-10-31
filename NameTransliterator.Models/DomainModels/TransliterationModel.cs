namespace NameTransliterator.Models.DomainModels
{
    using System.Collections.Generic;

    public class TransliterationModel
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

        public int LanguagePairId { get; set; }

        public virtual LanguagePair LanguagePair { get; set; }

        public int TransliterationTypeId { get; set; }

        public virtual TransliterationType TransliterationType { get; set; }
    }
}
