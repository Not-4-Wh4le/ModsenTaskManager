using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Queries.GetTasks
{
    public record TaskDto(
        Guid Id,
        string Title,
        Domain.Enums.TaskStatus TaskStatus,
        string[] Tags);
}
