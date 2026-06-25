using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.CreateProject
{
    public record CreateProjectCommand(
        string Name) : IRequest<Result<Guid>>;
}
