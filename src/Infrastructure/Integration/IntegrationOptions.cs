namespace CleanArchitecture.Integration
{
    public class IntegrationOptions
    {
        public bool IsRabbitMqEnabled { get; set; } = false;
    }

    public class RabbitMQOptions
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return "{{" +
                $"{nameof(HostName)}={HostName}, " +
                $"{nameof(UserName)}={UserName}, " +
                $"{nameof(Password)}={Password}" +
                "}}";
        }
    }
}