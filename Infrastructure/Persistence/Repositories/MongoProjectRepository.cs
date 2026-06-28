using Application.Common.Interfaces.Repositories;
using Domain.Entites;
using Domain.Enums;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using Application.Common.Features.Projects.Queries.GetProjects;

namespace Infrastructure.Persistence.Repositories
{
    public class MongoProjectRepository
        : MongoRepositoryBase<Project>, IProjectRepository
    {
        public MongoProjectRepository(MongoDbContext context)
            : base(context, MongoCollectionNames.Projects)
        {
        }

        public async Task<(IReadOnlyCollection<Project> Items, int TotalCount)> GetPagedAsync(
            string? nameSearch, 
            ProjectStatus? filteringStatus, 
            string? sortBy, 
            bool isDescending, 
            int page, 
            int pageSize, 
            CancellationToken cancellationToken)
        {
            var builder = Builders<Project>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(nameSearch))
            {
                filter &= builder.Regex(p => p.Name, new BsonRegularExpression(nameSearch, "i"));
            }

            if (filteringStatus.HasValue)
            {
                filter &= builder.Eq(p => p.ProjectStatus, filteringStatus);
            }

            var sortBuilder = Builders<Project>.Sort;

            var sort = sortBy?.Trim().ToLower() switch
            {
                ProjectSortFields.Date => isDescending
                    ? sortBuilder.Descending(p => p.CreatedAt)
                    : sortBuilder.Ascending(p => p.CreatedAt),

                ProjectSortFields.Name => isDescending
                    ? sortBuilder.Descending(p => p.Name)
                    : sortBuilder.Ascending(p => p.Name),

                _ => sortBuilder.Ascending(p => p.CreatedAt)
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
