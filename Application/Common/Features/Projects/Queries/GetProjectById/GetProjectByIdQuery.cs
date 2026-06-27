using Application.Common.Models;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Queries.GetProjectById
{
    public record GetProjectByIdQuery(Guid Id) : IRequest<Result<ProjectDetailsDto>>;
}
