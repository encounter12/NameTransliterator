namespace NameTransliterator.Services
{
    using global::NameTransliterator.Services.Models;
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
