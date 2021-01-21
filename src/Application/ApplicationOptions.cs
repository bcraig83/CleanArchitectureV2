namespace CleanArchitecture.Application
{
    public class ApplicationOptions
    {
        public bool StoreAuthorInLowercase { get; set; } = false;
        public bool IsRabbitMqEnabled { get; set; } = false;
    }

    public class RabbitMQOptions
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}