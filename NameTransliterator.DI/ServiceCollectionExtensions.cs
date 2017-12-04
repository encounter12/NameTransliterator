using Microsoft.Extensions.DependencyInjection;
using NameTransliterator.Data.Context;
using NameTransliterator.Data.Repositories;
using NameTransliterator.Data.Repositories.Abstractions;

namespace NameTransliterator.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(
            this IServiceCollection services, string dependencyInjectorName)
        {
            if (dependencyInjectorName == "BuiltInDependencyInjector")
            {
                BindDbContext(services);
                BindRepositories(services);
                BindServicesFromServiceProject(services);
            }

            return services;
        }

        public static void BindDbContext(IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static void BindRepositories(IServiceCollection services)
        {
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        }

        public static void BindServicesFromServiceProject(IServiceCollection services)
        {
        }
    }
}
