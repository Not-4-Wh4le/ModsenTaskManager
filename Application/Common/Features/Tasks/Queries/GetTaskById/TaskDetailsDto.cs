using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Queries.GetTaskById
{
    public record TaskDetailsDto(
        Guid Id,
        string Title,
        string Description,
        DateTimeOffset CreatedAt,
        DateTimeOffset DueDate,
        IEnumerable<string> Tags,
        Domain.Enums.TaskStatus TaskStatus,
        TaskPriorityLevel PriorityLevel);
}
