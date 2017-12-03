namespace NameTransliterator.Data.Repositories
{
    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Models.DomainModels;

    public class TransliterationModelRepository : AuditableEntityRepository<TransliterationModel>, ITransliterationModelRepository
    {
        public TransliterationModelRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
