namespace NameTransliterator.Services
{
    using System.Collections.Generic;

    public static class DictionaryExtensions
    {
        public static SortedDictionary<string, string> SwapDictionaryKeysWithValues(
            this SortedDictionary<string, string> initialDictionary, 
            IComparer<string> comparer)
        {
            var swappedDictionary = new SortedDictionary<string, string>(comparer);

            foreach (var item in initialDictionary)
            {
                if (!swappedDictionary.ContainsKey(item.Value))
                {
                    swappedDictionary.Add(item.Value, item.Key);
                }
            }

            return swappedDictionary;
        }

        public static SortedDictionary<string, string> SwapDictionaryKeysWithValues(
            this IDictionary<string, string> initialDictionary,
            IComparer<string> comparer)
        {
            var swappedDictionary = new SortedDictionary<string, string>(comparer);

            foreach (var item in initialDictionary)
            {
                if (!swappedDictionary.ContainsKey(item.Value))
                {
                    swappedDictionary.Add(item.Value, item.Key);
                }
            }

            return swappedDictionary;
        }
    }
}
