namespace NameTransliterator.Services
{
    using System.Collections.Generic;
    using Models;

    public static class TestData
    {
        private static Language Bulgarian = new Language() { Id = 1, Name = "Bulgarian" };

        private static Language English = new Language() { Id = 2, Name = "English" };

        public static List<Language> Languages = new List<Language>()
        {
            Bulgarian,
            English
        };
    }
}
