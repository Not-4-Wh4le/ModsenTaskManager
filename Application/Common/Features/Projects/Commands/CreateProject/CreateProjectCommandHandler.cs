using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Domain.Entites;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        ICurrentUserService currentUser) : IRequestHandler<CreateProjectCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(Guid.NewGuid(), request.Name, currentUser.GetUserId());
            await projectRepository.AddAsync(project, cancellationToken);

            return project.Id;
        }
    }
}
