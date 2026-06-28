using Application.Common.Interfaces.Repositories;
using Domain.Common.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public abstract class MongoRepositoryBase<T>
        : IRepositoryBase<T> where T : IEntity
    {
        protected readonly IMongoCollection<T> Collection;

        protected MongoRepositoryBase(MongoDbContext context, string collectionName)
        {
            Collection = context.GetCollection<T>(collectionName);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await Collection.DeleteOneAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Collection.Find(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<T>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.In(e => e.Id, ids);
            return await Collection.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity, cancellationToken: cancellationToken);
        }
    }
}
