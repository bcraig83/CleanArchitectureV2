namespace CleanArchitecture.Integration.Messaging.AzureServiceBus
{
    public static class Constants
    {
        // TODO: read from config
        public const string CONNECTION_STRING = 
            "Endpoint=sb://bookworm.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=i7921Ig8nlwkvGZbW87aE9XtQBbWiJSw+JO2o4zuVlw=";
        public const string QUEUE_NAME = "bookworm";
    }
}