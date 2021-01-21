using CleanArchitecture.Application.Features.Books.Commands.CreateBook;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Integration.RabbitMQ
{
    public class CreateBookCommandConsumer
    {
        private readonly RabbitMQConnection _connection;
        private readonly IMediator _mediator;

        public CreateBookCommandConsumer(
            RabbitMQConnection connection,
            IMediator mediator)
        {
            _connection = connection ?? throw new System.ArgumentNullException(nameof(connection));
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        public void Consume()
        {
            // Queue name could be configurable?
            var channel = _connection.CreateModel();
            channel.QueueDeclare("CreateBook", false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            //Create event when something receive
            consumer.Received += ReceivedEvent;

            channel.BasicConsume("CreateBook", true, consumer);
        }

        private async Task ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == "CreateBook")
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var command = JsonConvert.DeserializeObject<CreateBookCommand>(message);

                // TODO: do we need mapping between the command object and what comes off the queue? Probably.

                var result = await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}