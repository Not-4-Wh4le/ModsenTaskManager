using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces.Repositories
{
    public interface ITaskRepository : IRepositoryBase<Domain.Entites.Task>
    {
        System.Threading.Tasks.Task DeleteAllByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
        Task<(IReadOnlyCollection<Domain.Entites.Task> Items, int TotalCount)> GetPagedAsync(
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
            CancellationToken cancellationToken);

    }
}
