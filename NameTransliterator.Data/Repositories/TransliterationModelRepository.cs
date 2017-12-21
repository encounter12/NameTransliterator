namespace NameTransliterator.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Models.DomainModels;
    using NameTransliterator.Models.ViewModels;

    public class TransliterationModelRepository : AuditableEntityRepository<TransliterationModel>, ITransliterationModelRepository
    {
        public TransliterationModelRepository(IApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<SourceLanguageViewModel> GetSourceLanguages(
            bool transliterationModelOfficial, 
            bool transliterationModelActive)
        {
            IQueryable<TransliterationModel> transliterationModels = this.LoadTransliterationModels(
                transliterationModelOfficial,
                transliterationModelActive);

            IOrderedQueryable<SourceLanguageViewModel> sourceLanguages = null; 

            if (transliterationModels != null)
            {
                sourceLanguages = transliterationModels
                    .GroupBy(tm => tm.SourceLanguageId)
                    .Select(group => new SourceLanguageViewModel
                    {
                        Id = group.Key,
                        Name = group.Select(g => g.SourceLanguage.Name).First(),
                        TargetLanguageIds = group.Select(g => g.TargetLanguageId).ToList()
                    })
                    .OrderBy(tm => tm.Name);
            }

            return sourceLanguages;
        }

        public IQueryable<TargetLanguageViewModel> GetTargetLanguages(
            bool transliterationModelOfficial,
            bool transliterationModelActive)
        {
            IQueryable<TransliterationModel> transliterationModels = 
                this.LoadTransliterationModels(transliterationModelOfficial, transliterationModelActive);

            IOrderedQueryable<TargetLanguageViewModel> targetLanguages = null;

            if (transliterationModels != null)
            {
                targetLanguages = transliterationModels.Select(tm => new TargetLanguageViewModel
                {
                    Id = tm.TargetLanguageId,
                    Name = tm.TargetLanguage.Name
                })
                .OrderBy(tl => tl.Name);
            }

            return targetLanguages;
        }

        private IQueryable<TransliterationModel> LoadTransliterationModels(
            bool transliterationModelOfficial, 
            bool transliterationModelActive)
        {
            // return this.LoadTransliterationModelsFromTextFiles();
            return this.All().Where(
                tm => tm.IsOfficial == transliterationModelOfficial &&
                tm.IsActive == transliterationModelActive);
        }

        public List<TransliterationModel> LoadTransliterationModelsFromTextFiles()
        {
            var relativeFilePath = "TransliterationSetTextFiles";

            // var currentAssemblyDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            var currentAssemblyDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string transliterationFilesDirPath = Path.Combine(currentAssemblyDirectoryPath, relativeFilePath);

            IEnumerable<string> files =
                Directory.EnumerateFiles(transliterationFilesDirPath, "*.txt", SearchOption.AllDirectories);

            var deserializer = new Deserializer();

            var transliterationModels = new List<TransliterationModel>();

            int fileCounter = 1;

            foreach (var file in files)
            {
                try
                {
                    var transliterationModel = new TransliterationModel();

                    transliterationModel = deserializer.Deserialize(file, fileCounter);

                    transliterationModels.Add(transliterationModel);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                fileCounter++;
            }

            return transliterationModels;
        }
    }
}
