using Newtonsoft.Json;
using System;
using System.Text;

namespace CleanArchitecture.Integration.RabbitMQ
{
    public class MessageProducer
    {
        private readonly RabbitMQConnection _connection;

        public MessageProducer(RabbitMQConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void Publish<T>(string queueName, Message<T> publishModel)
        {
            using var channel = _connection.CreateModel();

            channel.QueueDeclare(queueName, false, false, false, null);
            var message = JsonConvert.SerializeObject(publishModel);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.DeliveryMode = 2;

            //properties.Type = publishModel.Payload.GetType().Name;

            channel.ConfirmSelect();
            channel.BasicPublish("", queueName, true, properties, body);
            channel.WaitForConfirmsOrDie();

            channel.BasicAcks += (sender, eventArgs) =>
            {
                // Use logger
                Console.WriteLine("Sent RabbitMQ");
                //implement ack handle
            };
            channel.ConfirmSelect();
        }
    }
}