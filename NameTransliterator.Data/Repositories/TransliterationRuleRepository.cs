namespace NameTransliterator.Data.Repositories
{
    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Models.DomainModels;

    public class TransliterationRuleRepository : AuditableEntityRepository<TransliterationRule>, ITransliterationRuleRepository
    {
        public TransliterationRuleRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
