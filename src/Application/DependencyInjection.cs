using AutoMapper;
using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Features.Books.Commands.CreateBook.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = BuildApplicationOptions(configuration);
            services.AddSingleton(x => options);

            services.AddTransient<BookMapper>();

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
                StoreAuthorInLowercase = configuration.GetValue<bool>("Application:StoreAuthorInLowercase")
            };
        }
    }
}