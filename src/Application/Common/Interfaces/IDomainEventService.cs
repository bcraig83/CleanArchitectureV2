using CleanArchitecture.Domain.Common.Events;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    internal interface IDomainEventService
    {
        Task PublishAsync(DomainEvent domainEvent);
    }
}