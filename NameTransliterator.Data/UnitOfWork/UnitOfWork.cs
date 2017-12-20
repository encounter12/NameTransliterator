using NameTransliterator.Data.Context;
using NameTransliterator.Data.Repositories;
using NameTransliterator.Data.Repositories.Abstractions;

namespace NameTransliterator.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IApplicationUserRepository applicationUserRepository;

        private IAuthorRepository authorRepository;

        private ILanguageRepository languageRepository;

        private INameRepository nameRepository;

        private ITransliterationDictionaryRepository transliterationDictionaryRepository;

        private ITransliterationModelRepository transliterationModelRepository;

        private ITransliterationRuleRepository transliterationRuleRepository;

        private ITransliterationTypeRepository transliterationTypeRepository;

        public UnitOfWork(IApplicationDbContext applicationDbContext)
        {
            this.ApplicationDbContext = applicationDbContext;
        }

        public IApplicationDbContext ApplicationDbContext { get; }

        public IApplicationUserRepository ApplicationUserRepository
        {
            get
            {
                if (this.applicationUserRepository == null)
                {
                    this.applicationUserRepository = new ApplicationUserRepository(this.ApplicationDbContext);
                }

                return applicationUserRepository;
            }
        }

        public IAuthorRepository AuthorRepository
        {
            get
            {
                if (this.authorRepository == null)
                {
                    this.authorRepository = new AuthorRepository(this.ApplicationDbContext);
                }

                return authorRepository;
            }
        }

        public ILanguageRepository LanguageRepository
        {
            get
            {
                if (this.languageRepository == null)
                {
                    this.languageRepository = new LanguageRepository(this.ApplicationDbContext);
                }

                return languageRepository;
            }
        }

        public INameRepository NameRepository
        {
            get
            {
                if (this.nameRepository == null)
                {
                    this.nameRepository = new NameRepository(this.ApplicationDbContext);
                }

                return nameRepository;
            }
        }

        public ITransliterationDictionaryRepository TransliterationDictionaryRepository
        {
            get
            {
                if (this.transliterationDictionaryRepository == null)
                {
                    this.transliterationDictionaryRepository = new TransliterationDictionaryRepository(this.ApplicationDbContext);
                }

                return transliterationDictionaryRepository;
            }
        }

        public ITransliterationModelRepository TransliterationModelRepository
        {
            get
            {
                if (this.transliterationModelRepository == null)
                {
                    this.transliterationModelRepository = new TransliterationModelRepository(this.ApplicationDbContext);
                }

                return transliterationModelRepository;
            }
        }

        public ITransliterationRuleRepository TransliterationRuleRepository
        {
            get
            {
                if (this.transliterationRuleRepository == null)
                {
                    this.transliterationRuleRepository = new TransliterationRuleRepository(this.ApplicationDbContext);
                }

                return transliterationRuleRepository;
            }
        }

        public ITransliterationTypeRepository TransliterationTypeRepository
        {
            get
            {
                if (this.transliterationTypeRepository == null)
                {
                    this.transliterationTypeRepository = new TransliterationTypeRepository(this.ApplicationDbContext);
                }

                return transliterationTypeRepository;
            }
        }
    }
}
