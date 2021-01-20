using CleanArchitecture.Domain.Common.Events;

namespace CleanArchitecture.Domain.Events
{
    public class BookCreatedEvent : DomainEvent
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}