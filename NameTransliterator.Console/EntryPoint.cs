namespace NameTransliterator.Console
{
    using System;
    using System.IO;
    using System.Reflection;

    using NameTransliterator.Services;
    using NameTransliterator.Services.Models;

    public class EntryPoint
    {
        private static NameTransliterator nameTransliterator;

        private static NameTransliterationModel transliterationModel;

        public static void Main(string[] args)
        {
            InitializeTransliteration();

            Console.WriteLine("Name for transliteration (BG):");

            string nameForTransliteration = Console.ReadLine();

            string transliteratedName = nameTransliterator.TransliterateName(transliterationModel, nameForTransliteration);

            Console.WriteLine("Name transliterated (EN): {0}", transliteratedName);
        }

        public static void InitializeTransliteration()
        {
            var relativeFilePath = "TransliterationSets";

            var transliterationDictionaryFileName = "Bulgarian-English.txt";

            var fullPath = GetFileFullPath(relativeFilePath, transliterationDictionaryFileName);

            var deserializer = new Deserializer();

            transliterationModel = deserializer.Deserialize(fullPath);

            nameTransliterator = new NameTransliterator();
        }

        public static string GetFileFullPath(string relativeFilePath, string fileName)
        {
            // var currentAssemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var currentAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var fileFullPath = Path.Combine(currentAssemblyDirectory, relativeFilePath, fileName);

            return fileFullPath;
        }
    }
}
