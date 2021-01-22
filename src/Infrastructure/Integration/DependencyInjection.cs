using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Integration.Email;
using CleanArchitecture.Integration.Messaging.RabbitMQ;
using CleanArchitecture.Integration.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CleanArchitecture.Integration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIntegration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new IntegrationOptions
            {
                IsRabbitMqEnabled = configuration.GetValue<bool>("Infrastructure:Integration:IsRabbitMQEnabled")
            };
            services.AddSingleton(x => options);

            if (options.IsRabbitMqEnabled)
            {
                var rabbitMqOptions = BuildRabbitMQOptions(configuration);
                services.AddSingleton(x => rabbitMqOptions);
                services.AddRabbitMQ(rabbitMqOptions);
            }

            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }

        private static RabbitMQOptions BuildRabbitMQOptions(IConfiguration configuration)
        {
            return new RabbitMQOptions
            {
                HostName = configuration.GetValue<string>("Infrastructure:Integration:RabbitMQ:HostName"),
                UserName = configuration.GetValue<string>("Infrastructure:Integration:RabbitMQ:UserName"),
                Password = configuration.GetValue<string>("Infrastructure:Integration:RabbitMQ:Password")
            };
        }

        private static IServiceCollection AddRabbitMQ(
            this IServiceCollection services,
            RabbitMQOptions options)
        {
            services.AddSingleton(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = options.HostName
                };

                if (!string.IsNullOrEmpty(options.UserName))
                {
                    factory.UserName = options.UserName;
                }

                if (!string.IsNullOrEmpty(options.Password))
                {
                    factory.Password = options.Password;
                }

                factory.DispatchConsumersAsync = true;

                return new RabbitMQConnection(factory);
            });

            services.AddSingleton<MessageConsumer>();

            return services;
        }
    }
}