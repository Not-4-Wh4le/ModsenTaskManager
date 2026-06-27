using Domain.Enums;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Commands.UpdateTaskDetais
{
    public record UpdateTaskDetailsCommand(
        Guid Id,
        string Title,
        string Description,
        DateTimeOffset DueDate,
        TaskPriorityLevel TaskPriorityLevel,
        IEnumerable<string>? Tags) : IRequest<Result>;
}
