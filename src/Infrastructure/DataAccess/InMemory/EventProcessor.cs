using CleanArchitecture.Domain.Common;
using MediatR;
using System.Threading.Tasks;

namespace CleanArchitecture.DataAccess.InMemory
{
    public class EventProcessor
    {
        private readonly IMediator _mediator;

        public EventProcessor(IMediator mediator)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        public async Task ProcessEvents(DomainEntity entity)
        {
            var events = entity.DomainEvents.ToArray();
            entity.DomainEvents.Clear();

            foreach (var domainEvent in events)
            {
                await _mediator
                    .Publish(domainEvent)
                    .ConfigureAwait(false);
            }
        }
    }
}