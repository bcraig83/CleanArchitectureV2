using System;

namespace CleanArchitecture.Integration.Messaging
{
    public class Message<T>
    {
        private T _payload;

        public DateTime Created { get; set; }
        public string Id { get; set; }

        public T Payload
        {
            get { return _payload; }
            set
            {
                _payload = value;
                Type = _payload.GetType().Name;
            }
        }

        public string Type { get; private set; }
    }
}