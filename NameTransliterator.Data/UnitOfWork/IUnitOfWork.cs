using NameTransliterator.Data.Context;
using NameTransliterator.Data.Repositories.Abstractions;

namespace NameTransliterator.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IApplicationDbContext ApplicationDbContext { get; }

        IApplicationUserRepository ApplicationUserRepository { get; }

        IAuthorRepository AuthorRepository { get; }

        ILanguageRepository LanguageRepository { get; }

        INameRepository NameRepository { get; }

        ITransliterationDictionaryRepository TransliterationDictionaryRepository { get; }

        ITransliterationModelRepository TransliterationModelRepository { get; }

        ITransliterationRuleRepository TransliterationRuleRepository { get; }

        ITransliterationTypeRepository TransliterationTypeRepository { get; }
    }
}
