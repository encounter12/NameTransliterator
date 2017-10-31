namespace NameTransliterator.Data.Context
{
    using Microsoft.EntityFrameworkCore;
    using NameTransliterator.Models.IdentityModels;

    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }

        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
