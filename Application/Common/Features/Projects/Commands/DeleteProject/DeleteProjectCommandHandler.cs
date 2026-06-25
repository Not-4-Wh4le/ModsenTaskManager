using Application.Common.Features.Users;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler(
        IProjectRepository projectRepository,
        ICurrentUserService currentUser,
        IDomainEventDispatcher eventDispatcher)
        : IRequestHandler<DeleteProjectCommand, Result>
    {
        public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await projectRepository.GetByIdAsync(request.Id, cancellationToken);
            if(project == null) 
                return ProjectErrors.NotFound;

            if (project.OwnerId != currentUser.Id)
                return UserErrors.Forbidden;

            project.Delete();
            await projectRepository.DeleteAsync(project.Id, cancellationToken);
            await eventDispatcher.DispatchAndClearAsync(project, cancellationToken);
            return Result.Success();
        }
    }
}
