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

            this.SwappedTransliterationDictionary = new SortedDictionary<string, string>(lengthComparer);

            this.SwappedTransliterationRegexDictionary = new SortedDictionary<string, string>(lengthComparer);
        }

        public SortedDictionary<string, string> TransliterationDictionary { get; set; }

        public SortedDictionary<string, string> TransliterationRegexDictionary { get; set; }

        public SortedDictionary<string, string> SwappedTransliterationDictionary { get; set; }

        public SortedDictionary<string, string> SwappedTransliterationRegexDictionary { get; set; }

        public string SourceLanguage { get; set; }

        public string TargetLanguage { get; set; }
    }
}
