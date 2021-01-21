namespace CleanArchitecture.Application
{
    public class ApplicationOptions
    {
        public bool StoreAuthorInLowercase { get; set; } = false;
        public bool IsRabbitMqEnabled { get; set; } = false;

        public override string ToString()
        {
            return "{{" +
                $"{nameof(StoreAuthorInLowercase)}={StoreAuthorInLowercase}, " +
                $"{nameof(IsRabbitMqEnabled)}={IsRabbitMqEnabled}" +
                "}}";
        }
    }

    public class RabbitMQOptions
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}