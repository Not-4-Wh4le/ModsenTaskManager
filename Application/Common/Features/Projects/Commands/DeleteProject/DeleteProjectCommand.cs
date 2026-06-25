using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.DeleteProject
{
    public record DeleteProjectCommand(
        Guid Id) : IRequest<Result>;
}
