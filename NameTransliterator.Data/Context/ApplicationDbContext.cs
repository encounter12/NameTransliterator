namespace NameTransliterator.Data.Context
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using NameTransliterator.Models.IdentityModels;
    using NameTransliterator.Models.DomainModels;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<LanguagePair> LanguagePairs { get; set; }

        public DbSet<SourceName> SourceNames { get; set; }

        public DbSet<TargetName> TargetNames { get; set; }

        public DbSet<TransliterationModel> TransliterationModels { get; set; }

        public DbSet<TransliterationRule> TransliterationRules { get; set; }

        public DbSet<TransliterationType> TransliterationTypes { get; set; }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            var newEntries = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added);


            var modifiedEntries = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in newEntries)
            {
                entry.Property("CreatedOn").CurrentValue = DateTime.UtcNow;
            }

            foreach (var entry in modifiedEntries)
            {
                entry.Property("LastUpdatedOn").CurrentValue = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
