using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Integration.Email;
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
            // TODO: figure out why the regular binding approach isn't working!
            var options = new IntegrationOptions
            {
                SomeBoolean = configuration.GetValue<bool>("Infrastructure:Integration:SomeBoolean")
            };
            services.AddScoped(x => options);

            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}