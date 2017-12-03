namespace NameTransliterator.Data.Repositories
{
    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Models.DomainModels;

    public class AuthorRepository : AuditableEntityRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
