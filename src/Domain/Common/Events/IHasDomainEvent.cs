using System.Collections.Generic;

namespace CleanArchitecture.Domain.Common.Events
{
    internal interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
}