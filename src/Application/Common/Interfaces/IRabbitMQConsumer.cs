namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IRabbitMQConsumer
    {
        void Consume();
        void Disconnect();
    }
}