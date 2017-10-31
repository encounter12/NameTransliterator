namespace NameTransliterator.Data.Repositories
{
    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Models.IdentityModels;

    public class ApplicationUserRepository : AuditableEntityRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly IApplicationDbContext context;

        public ApplicationUserRepository(IApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public ApplicationDbContext Context
        {
            get
            {
                return (ApplicationDbContext)this.context;
            }
        }
    }
}
