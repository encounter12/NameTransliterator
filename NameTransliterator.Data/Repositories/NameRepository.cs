namespace NameTransliterator.Data.Repositories
{
    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Models.DomainModels;

    public class NameRepository : AuditableEntityRepository<Name>, INameRepository
    {
        public NameRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
