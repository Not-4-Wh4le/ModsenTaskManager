using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Commands.ChangeTaskStatus
{
    public record ChangeTaskStatusCommand(Guid Id, Domain.Enums.TaskStatus TaskStatus) : IRequest<Result>;
}
