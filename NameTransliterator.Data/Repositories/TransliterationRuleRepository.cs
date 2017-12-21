namespace NameTransliterator.Data.Repositories
{
    using System.Linq;

    using NameTransliterator.Data.Context;
    using NameTransliterator.Data.Repositories.Abstractions;
    using NameTransliterator.Models.DomainModels;

    public class TransliterationRuleRepository : AuditableEntityRepository<TransliterationRule>, ITransliterationRuleRepository
    {
        public TransliterationRuleRepository(IApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<TransliterationRule> GetSelectedModelTransliterationRules(
            bool transliterationModelOfficial,
            bool transliterationModelActive,
            int sourceLanguageId,
            int targetLanguageId)
        {
            var transliterationRules = this.All()
                .Where
                (
                    tr =>
                        tr.TransliterationModel.SourceLanguageId == sourceLanguageId &&
                        tr.TransliterationModel.TargetLanguageId == targetLanguageId &&
                        tr.TransliterationModel.IsOfficial == transliterationModelOfficial &&
                        tr.TransliterationModel.IsActive == transliterationModelActive
                )
                .OrderBy(tr => tr.ExecutionOrder);

            return transliterationRules;
        }
    }
}
