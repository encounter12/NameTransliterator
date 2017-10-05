namespace NameTransliterator.Services
{
    using System.Collections.Generic;

    public class LengthComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int lengthComparison = y.Length.CompareTo(x.Length);

            if (lengthComparison == 0)
            {
                return x.CompareTo(y);
            }
            else
            {
                return lengthComparison;
            }
        }
    }
}
