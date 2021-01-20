using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.DataAccess.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = 
                configuration.GetSection(DataAccessOptions.AppSettingsFileLocation).Get<DataAccessOptions>() 
                ?? new DataAccessOptions();

            services.AddScoped(x => options);

            switch (options.PersistenceMechanism)
            {
                // As an example, you could add EF...
                //case "EntityFramework":
                //    services.AddPersistenceThroughEntityFramework(configuration, options);
                //    break;

                default:
                    services.AddPersistenceThroughInMemoryDatastore();
                    break;
            }

            return services;
        }

        // Obviously this is just for testing
        private static IServiceCollection AddPersistenceThroughInMemoryDatastore(
            this IServiceCollection services)
        {
            services.AddSingleton(typeof(IRepository<>), typeof(InMemoryRepository<>));
            services.AddSingleton<EventProcessor>();

            return services;
        }
    }
}