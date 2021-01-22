using CleanArchitecture.Integration.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Integration
{
    public static class ApplicationBuilderExtentions
    {
        public static MessageConsumer CreateBookCommandConsumer { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            CreateBookCommandConsumer = app.ApplicationServices.GetService<MessageConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            CreateBookCommandConsumer.Consume();
        }

        private static void OnStopping()
        {
            CreateBookCommandConsumer.Disconnect();
        }
    }
}