namespace NameTransliterator.Data.Repositories.Abstractions
{
    using System.Linq;

    using NameTransliterator.Models.DomainModels;

    public interface ITransliterationRuleRepository : IAuditableEntityRepository<TransliterationRule>
    {
        IQueryable<TransliterationRule> GetSelectedModelTransliterationRules(
            bool transliterationModelOfficial,
            bool transliterationModelActive,
            int sourceLanguageId,
            int targetLanguageId);
    }
}
