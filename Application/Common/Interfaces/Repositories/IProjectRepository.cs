using Domain.Entites;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces.Repositories
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        Task<(IReadOnlyCollection<Project> Items, int TotalCount)> GetPagedAsync(
            string? nameSearch,
            ProjectStatus? filteringStatus,
            string? sortBy,
            bool isDescending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
