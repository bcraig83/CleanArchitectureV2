using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.RabbitMQ;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text;

namespace CleanArchitecture.Application.Features.Books.Commands.CreateBook.Services
{
    public class CreateBookCommandConsumer : IRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;

        public CreateBookCommandConsumer(
            IRabbitMQConnection connection, 
            IMediator mediator)
        {
            _connection = connection ?? throw new System.ArgumentNullException(nameof(connection));
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        void IRabbitMQConsumer.Consume()
        {
            throw new System.NotImplementedException();
        }

        async void IRabbitMQConsumer.ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == "CreateBook")
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var command = JsonConvert.DeserializeObject<CreateBookCommand>(message);

                // TODO: do we need mapping between the command object and what comes off the queue? Probably.

                var result = await _mediator.Send(command);
            }
        }

        void IRabbitMQConsumer.Disconnect()
        {
            _connection.Dispose();
        }
    }
}