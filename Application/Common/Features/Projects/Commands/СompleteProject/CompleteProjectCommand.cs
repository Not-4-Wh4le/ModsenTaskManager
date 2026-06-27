using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.СompleteProject
{
    public record CompleteProjectCommand(Guid Id): IRequest<Result>;
}
