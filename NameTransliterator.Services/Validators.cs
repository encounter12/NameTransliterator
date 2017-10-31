namespace NameTransliterator.Services
{
    using System;

    public class Validators
    {
        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name is not valid.");
            }
        }
    }
}
