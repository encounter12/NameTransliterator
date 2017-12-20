using Microsoft.Extensions.DependencyInjection;

using NameTransliterator.Data.Context;
using NameTransliterator.Data.UnitOfWork;
using NameTransliterator.Services.Abstractions;

namespace NameTransliterator.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(
            this IServiceCollection services, string dependencyInjectorName)
        {
            if (dependencyInjectorName == "BuiltInDependencyInjector")
            {
                services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
                services.AddScoped<IUnitOfWork, UnitOfWork>();

                BindServicesFromServiceProject(services);
            }

            return services;
        }

        public static void BindServicesFromServiceProject(IServiceCollection services)
        {
            services.AddScoped<INameTransliterator, Services.NameTransliterator>();
        }
    }
}
