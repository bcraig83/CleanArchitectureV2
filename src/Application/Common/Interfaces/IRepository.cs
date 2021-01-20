using CleanArchitecture.Domain.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : DomainEntity
    {
        // TODO: Fill out this interface as we go along.
        // TODO: should these async methods accept a cancellation token?
        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> RemoveAsync(TEntity entity);
    }
}