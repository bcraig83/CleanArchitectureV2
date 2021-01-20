using CleanArchitecture.Domain.Common;
using MediatR;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IBaseEventHandler<T> : INotificationHandler<T> where T : DomainEvent
    {
    }
}