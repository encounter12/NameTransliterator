namespace NameTransliterator.Data.Repositories
{
    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Models.DomainModels;

    public class LanguageRepository : AuditableEntityRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
