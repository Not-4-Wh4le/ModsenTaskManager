using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : IEntity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<T>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
