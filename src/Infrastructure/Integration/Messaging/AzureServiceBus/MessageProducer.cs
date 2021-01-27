using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Integration.Messaging.AzureServiceBus
{
    public class MessageProducer
    {
        public async Task Publish<T>(string queueName, Message<T> publishModel)
        {
            var rawMessage = JsonConvert.SerializeObject(publishModel);

            // create a Service Bus client
            await using ServiceBusClient client = new ServiceBusClient(Constants.CONNECTION_STRING);

            // create a sender for the queue
            var sender = client.CreateSender(Constants.QUEUE_NAME);

            // create a message that we can send
            var message = new ServiceBusMessage("Hello World!");

            // send the message
            await sender.SendMessageAsync(message);
            Console.WriteLine($"Sent a single message to the queue: {queueName}");
        }
    }
}