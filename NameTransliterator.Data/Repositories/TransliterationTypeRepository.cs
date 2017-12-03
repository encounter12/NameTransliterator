namespace NameTransliterator.Data.Repositories
{
    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Models.DomainModels;

    public class TransliterationTypeRepository : AuditableEntityRepository<TransliterationType>, ITransliterationTypeRepository
    {
        public TransliterationTypeRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
