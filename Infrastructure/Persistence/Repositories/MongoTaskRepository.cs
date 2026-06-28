using Application.Common.Features.Tasks.Queries.GetTasks;
using Application.Common.Interfaces.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public class MongoTaskRepository
        : MongoRepositoryBase<Domain.Entites.Task>, ITaskRepository
    {
        public MongoTaskRepository(MongoDbContext context)
            : base(context, MongoCollectionNames.Tasks)
        {
        }

        public async Task DeleteAllByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
        {
            await Collection.DeleteManyAsync(t => t.ProjectId == projectId, cancellationToken: cancellationToken);
        }

        public async Task<(IReadOnlyCollection<Domain.Entites.Task> Items, int TotalCount)> GetPagedAsync(
            string? titleSearch, 
            Guid? filteringProjectId, 
            Domain.Enums.TaskStatus? filteringStatus, 
            DateTimeOffset? startDueDate, 
            DateTimeOffset? endDueDate, 
            string? sortBy, 
            IReadOnlyCollection<string>? tags, 
            bool isDescending, 
            int page, 
            int pageSize, 
            CancellationToken cancellationToken)
        {
            var builder = Builders<Domain.Entites.Task>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(titleSearch))
            {
                filter &= builder.Regex(t => t.Title, new BsonRegularExpression(titleSearch, "i"));
            }

            if (filteringProjectId.HasValue)
            {
                filter &= builder.Eq(t => t.ProjectId, filteringProjectId);
            }

            if (filteringStatus.HasValue)
            {
                filter &= builder.Eq(t => t.TaskStatus, filteringStatus);
            }

            if (startDueDate.HasValue)
            {
                filter &= builder.Gt(t => t.DueDate, startDueDate);
            }

            if (endDueDate.HasValue)
            {
                filter &= builder.Lt(t => t.DueDate, endDueDate);
            }

            if (tags != null && tags.Count != 0)
            {
                filter &= builder.All(t => t.Tags, tags);
            }

            var sortBuilder = Builders<Domain.Entites.Task>.Sort;

            var sort = sortBy?.Trim().ToLower() switch
            {
                TaskSortFields.Status => isDescending ? sortBuilder.Descending(t => t.TaskStatus) : sortBuilder.Ascending(t => t.TaskStatus),
                TaskSortFields.DueDate => isDescending ? sortBuilder.Descending(t => t.DueDate) : sortBuilder.Ascending(t => t.DueDate),
                TaskSortFields.CreatedAt => isDescending ? sortBuilder.Descending(t => t.CreatedAt) : sortBuilder.Ascending(t => t.CreatedAt),
                _ => sortBuilder.Ascending(t => t.CreatedAt),
            };

            var totalCount = await Collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

            var items = await Collection
                .Find(filter)
                .Sort(sort)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync(cancellationToken);

            return (items, (int)totalCount);

        }
    }
}
