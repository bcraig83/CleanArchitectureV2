using MediatR;
using System;

namespace CleanArchitecture.Domain.Common
{
    public abstract class DomainEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}