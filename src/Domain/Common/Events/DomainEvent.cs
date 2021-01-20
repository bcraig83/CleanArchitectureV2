using System;

namespace CleanArchitecture.Domain.Common.Events
{
    public abstract class DomainEvent
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}