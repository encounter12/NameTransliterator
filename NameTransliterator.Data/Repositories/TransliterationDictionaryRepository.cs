namespace NameTransliterator.Data.Repositories
{
    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Models.DomainModels;

    public class TransliterationDictionaryRepository : AuditableEntityRepository<TransliterationDictionary>, ITransliterationDictionaryRepository
    {
        public TransliterationDictionaryRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
