using RabbitMQ.Client.Events;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IRabbitMQConsumer
    {
        public void Consume();
        protected void ReceivedEvent(object sender, BasicDeliverEventArgs e);
        public void Disconnect();
    }
}