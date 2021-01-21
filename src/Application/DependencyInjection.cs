using AutoMapper;
using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Common.RabbitMQ;
using CleanArchitecture.Application.Features.Books.Commands.CreateBook.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        // TODO: refine the config / options stuff...
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = BuildApplicationOptions(configuration);
            services.AddScoped(x => options);

            if (options.IsRabbitMqEnabled)
            {
                RabbitMQOptions rabbitMqOptions = BuildRabbitMQOptions(configuration);
                services.AddScoped(x => rabbitMqOptions);
                services.AddRabbitMQ(rabbitMqOptions);
            }

            services.AddScoped<BookMapper>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }

        private static ApplicationOptions BuildApplicationOptions(IConfiguration configuration)
        {
            return new ApplicationOptions
            {
                StoreAuthorInLowercase = configuration.GetValue<bool>("Application:StoreAuthorInLowercase"),
                IsRabbitMqEnabled = configuration.GetValue<bool>("Application:IsRabbitMQEnabled")
            };
        }

        private static RabbitMQOptions BuildRabbitMQOptions(IConfiguration configuration)
        {
            return new RabbitMQOptions
            {
                HostName = configuration.GetValue<string>("Application:RabbitMQ:HostName"),
                UserName = configuration.GetValue<string>("Application:RabbitMQ:UserName"),
                Password = configuration.GetValue<string>("Application:RabbitMQ:Password")
            };
        }

        private static IServiceCollection AddRabbitMQ(
            this IServiceCollection services,
            RabbitMQOptions options)
        {
            services.AddSingleton<IRabbitMQConnection>(sp =>
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

            return services;
        }
    }
}