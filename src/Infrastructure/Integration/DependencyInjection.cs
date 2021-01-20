using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Integration.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Integration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIntegration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = configuration.GetSection(IntegrationOptions.AppSettingsFileLocation).Get<IntegrationOptions>()
                ?? new IntegrationOptions();
            services.AddScoped(x => options);

            services.AddTransient<IDateTimeService, DateTimeService>();

            return services;
        }
    }
}