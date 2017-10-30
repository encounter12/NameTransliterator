namespace NameTransliterator.Services.Models
{
    using System.Collections.Generic;

    public class NameTransliterationModel
    {
        public NameTransliterationModel()
        {
            IComparer<TransliterationRule> longestSourceExpressionComparer = new LongestSourceExpressionComparer();

            this.TransliterationRules = new List<TransliterationRule>();
        }

        public IList<TransliterationRule> TransliterationRules { get; set; }

        public int LanguageSetId { get; set; }

        public virtual LanguageSet LanguageSet { get; set; }
    }
}
