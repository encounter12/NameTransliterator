namespace NameTransliterator.Data.Repositories.Abstractions
{
    using System.Linq;

    using NameTransliterator.Models.DomainModels;
    using NameTransliterator.Models.ViewModels;

    public interface ITransliterationModelRepository : IAuditableEntityRepository<TransliterationModel>
    {
        IQueryable<SourceLanguageViewModel> GetSourceLanguages(
            bool transliterationModelOfficial,
            bool transliterationModelActive);
    }
}
