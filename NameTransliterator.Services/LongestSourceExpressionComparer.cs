using NameTransliterator.Models.DomainModels;

namespace NameTransliterator.Services
{
    using System.Collections.Generic;

    public class LongestSourceExpressionComparer : IComparer<TransliterationRule>
    {
        public int Compare(TransliterationRule x, TransliterationRule y)
        {
            int comparison = y.SourceExpression.Length.CompareTo(x.SourceExpression.Length);

            return comparison;
        }
    }
}
