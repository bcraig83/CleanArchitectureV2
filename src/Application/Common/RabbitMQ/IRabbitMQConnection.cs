using RabbitMQ.Client;
using System;

namespace CleanArchitecture.Application.Common.RabbitMQ
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}