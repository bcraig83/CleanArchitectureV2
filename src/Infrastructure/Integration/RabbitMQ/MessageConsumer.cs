using CleanArchitecture.Application.Features.Books.Commands.CreateBook;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Integration.RabbitMQ
{
    public class MessageConsumer
    {
        private readonly RabbitMQConnection _connection;
        private readonly IMediator _mediator;

        public MessageConsumer(
            RabbitMQConnection connection,
            IMediator mediator)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public void Consume()
        {
            // Queue name could be configurable?
            var channel = _connection.CreateModel();
            channel.QueueDeclare("BookWorm", false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume("BookWorm", true, consumer);
        }

        private async Task ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            // I don't think we need this???
            if (e.RoutingKey != "BookWorm") // this will change to BookService, or DeathNotificationService, etc
            {
                return;
            }

            try
            {
                var body = Encoding.UTF8.GetString(e.Body.Span);
                JObject jobject = JsonConvert.DeserializeObject<JObject>(body);
                var type = jobject.Value<string>("Type");

                switch (type)
                {
                    case "CreateBookCommand":
                        var message = JsonConvert.DeserializeObject<Message<CreateBookCommand>>(body);
                        var command = message.Payload;
                        await _mediator.Send(command);
                        break;

                    default:
                        Console.WriteLine("Invalid type received");
                        break;
                }

                // TODO: do we need mapping between the command object and what comes off the queue? Probably.
            }
            catch (JsonSerializationException jse)
            {
                // TODO: use logger
                Console.WriteLine("Serialization error! Exception message: " + jse.Message);
            }
            catch (Exception exception)
            {
                // TODO: use logger
                Console.WriteLine("some other error! Exception message: " + exception.Message);
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}