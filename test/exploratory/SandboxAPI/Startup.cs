using CleanArchitecture.Integration;
using CleanArchitecture.Integration.Messaging.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace SandboxAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddRabbitMQ(services);

            services.AddSingleton<MessageProducer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SandboxAPI", Version = "v1" });
            });
        }

        private void AddRabbitMQ(IServiceCollection services)
        {
            var rabbitMqOptions = BuildRabbitMQOptions(Configuration);
            services.AddSingleton(x => rabbitMqOptions);

            services.AddSingleton(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = rabbitMqOptions.HostName
                };

                if (!string.IsNullOrEmpty(rabbitMqOptions.UserName))
                {
                    factory.UserName = rabbitMqOptions.UserName;
                }

                if (!string.IsNullOrEmpty(rabbitMqOptions.Password))
                {
                    factory.Password = rabbitMqOptions.Password;
                }

                factory.DispatchConsumersAsync = true;

                return new RabbitMQConnection(factory);
            });
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SandboxAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}