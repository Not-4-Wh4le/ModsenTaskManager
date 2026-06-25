using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.UpdateProject
{
    public record RenameProjectCommand(
        Guid Id,
        string NewName) : IRequest<Result>;
}
