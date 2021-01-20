using CleanArchitecture.Domain.Common.Events;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    internal interface IDomainEventService
    {
        Task PublishAsync(DomainEvent domainEvent);
    }
}