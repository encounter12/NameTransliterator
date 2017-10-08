namespace NameTransliterator.Services.Models
{
    using System.Collections.Generic;

    public class NameTransliterationModel
    {
        public NameTransliterationModel()
        {
            IComparer<string> lengthComparer = new LengthComparer();

            this.TransliterationDictionary = new SortedDictionary<string, string>(lengthComparer);

            this.TransliterationRegexDictionary = new SortedDictionary<string, string>(lengthComparer);
        }

        public SortedDictionary<string, string> TransliterationDictionary { get; set; }

        public SortedDictionary<string, string> TransliterationRegexDictionary { get; set; }

        public int LanguageSetId { get; set; }

        public virtual LanguageSet LanguageSet { get; set; }
    }
}
