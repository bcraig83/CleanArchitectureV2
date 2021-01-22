using System;

namespace CleanArchitecture.Integration.RabbitMQ
{
    public class Message<T>
    {
        public DateTime Created { get; set; }
        public string Id { get; set; }
        public T Payload { get; set; }
    }
}