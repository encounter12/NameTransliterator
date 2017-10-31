namespace NameTransliterator.Data.Repositories.Abstractions
{
    using NameTransliterator.Data.Context;
    using NameTransliterator.Models.IdentityModels;

    public interface IApplicationUserRepository : IAuditableEntityRepository<ApplicationUser>
    {
        ApplicationDbContext Context { get; }
    }
}
