using Application.Common.Features.Users;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.UpdateProject
{
    public class RenameProjectCommandHandler(
        IProjectRepository projectRepository,
        ICurrentUserService currentUser)
        : IRequestHandler<RenameProjectCommand, Result>
    {
        public async Task<Result> Handle(RenameProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (project == null)
                return ProjectErrors.NotFound;

            if (project.OwnerId != currentUser.Id)
                return UserErrors.Forbidden;

            project.ChangeName(request.NewName);
            await projectRepository.UpdateAsync(project, cancellationToken);
            return Result.Success();
        }
    }
}
