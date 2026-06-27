using Domain.Enums;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand(
        Guid ProjectId,
        string Title,
        string Description,
        DateTimeOffset DueDate,
        TaskPriorityLevel PriorityLevel,
        IEnumerable<string>? Tags) : IRequest<Result<Guid>>;
}
