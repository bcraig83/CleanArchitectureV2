using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Events
{
    public class BookCreatedEvent : DomainEvent
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}