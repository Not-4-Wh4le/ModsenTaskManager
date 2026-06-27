using Application.Common.Models;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Queries.GetTasks
{
    public record GetTasksQuery(
        string? TitleSearch = null,
        Guid? FilteringProjectId = null,
        Domain.Enums.TaskStatus? FilteringStatus = null,
        DateTimeOffset? StartDueDate = null,
        DateTimeOffset? EndDueDate = null,
        string? SortBy = null,
        IReadOnlyCollection<string>? Tags = null,
        bool IsDescending = false,
        int Page = 1,
        int PageSize = 10) : IRequest<Result<PagedResultDto<TaskDto>>>;
}
