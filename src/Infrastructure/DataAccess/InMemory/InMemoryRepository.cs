using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.DataAccess.InMemory
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity>
        where TEntity : DomainEntity
    {
        private readonly IDictionary<int, TEntity> _dataStore;
        private readonly EventProcessor _eventProcessor;
        private readonly IDateTimeService _dateTimeService;

        private static int index = 1;

        public InMemoryRepository(
            EventProcessor eventProcessor,
            IDateTimeService dateTimeService)
        {
            _eventProcessor = eventProcessor ?? throw new System.ArgumentNullException(nameof(eventProcessor));
            _dateTimeService = dateTimeService ?? throw new System.ArgumentNullException(nameof(dateTimeService));

            _dataStore = new Dictionary<int, TEntity>();
        }

        // TODO: this is obviously very rough and ready, and needs proper defensive code!
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = index++;
            }

            var now = _dateTimeService.Now;
            entity.Created = now;
            entity.LastModified = now;

            _dataStore.TryAdd(entity.Id, entity);

            await _eventProcessor.ProcessEvents(entity);

            return entity;
        }

        public Task<IList<TEntity>> GetAllAsync()
        {
            var resultAsCollection = _dataStore.Values;
            IList<TEntity> resultAsList = resultAsCollection.ToList();

            return Task.FromResult(resultAsList);
        }

        public Task<TEntity> GetAsync(int id)
        {
            _dataStore.TryGetValue(id, out var result);

            return Task.FromResult(result);
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            _dataStore.Remove(entity.Id);
            await _eventProcessor.ProcessEvents(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dataStore.Remove(entity.Id);

            entity.LastModified = _dateTimeService.Now;

            _dataStore.TryAdd(entity.Id, entity);

            await _eventProcessor.ProcessEvents(entity);

            return entity;
        }
    }
}