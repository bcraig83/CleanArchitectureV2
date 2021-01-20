using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.IntegrationTests.Fakes;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.IntegrationTestDriverApi;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.IntegrationTests
{
    public class ApplicationTestFixture
    {
        private IConfigurationRoot Configuration { get; set; }
        public IServiceScopeFactory ScopeFactory { get; private set; }

        private readonly EmailSenderStub _emailSenderStub;

        public ApplicationTestFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.ApplicationIntegrationTests.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var startup = new Startup(Configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "ApplicationIntegrationTests" &&
                w.ApplicationName == "IntegrationTestDriverApi"));

            services.AddLogging();

            startup.ConfigureServices(services);

            // Replace service registration for email sender
            var currentEmailSenderServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IEmailSender));
            services.Remove(currentEmailSenderServiceDescriptor);

            // Register testing version
            _emailSenderStub = new EmailSenderStub();
            services.AddTransient<IEmailSender>(provider => _emailSenderStub);

            ScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = ScopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public async Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : DomainEntity
        {
            using var scope = ScopeFactory.CreateScope();

            var repository = scope.ServiceProvider.GetService<IRepository<TEntity>>();

            var result = await repository.GetAsync(id);

            return result;
        }

        public async Task<int> AddAsync<TEntity>(TEntity entity)
           where TEntity : DomainEntity
        {
            using var scope = ScopeFactory.CreateScope();

            var repository = scope.ServiceProvider.GetService<IRepository<TEntity>>();
            var result = await repository.AddAsync(entity);
            return result.Id;
        }

        public async Task RemoveAsync<TEntity>(TEntity entity)
            where TEntity : DomainEntity
        {
            using var scope = ScopeFactory.CreateScope();

            var repository = scope.ServiceProvider.GetService<IRepository<TEntity>>();
            await repository.RemoveAsync(entity);
        }

        public void ClearRecordedEmails()
        {
            _emailSenderStub.Clear();
        }

        public IList<EmailDetails> GetRecordedEmails()
        {
            return _emailSenderStub._recordedEmails;
        }
    }

    [CollectionDefinition("Non EF application test collection")]
    public class ApplicationTestCollection : ICollectionFixture<ApplicationTestFixture>
    {
    }
}