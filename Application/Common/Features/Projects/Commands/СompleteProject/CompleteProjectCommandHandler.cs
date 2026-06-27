using Application.Common.Features.Users;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.СompleteProject
{
    public class CompleteProjectCommandHandler
        : IRequestHandler<CompleteProjectCommand, Result>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUser;
        public CompleteProjectCommandHandler(
            IProjectRepository projectRepository,
            ICurrentUserService currentUser)
        {
            _projectRepository = projectRepository;
            _currentUser = currentUser;
        }
        public async Task<Result> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if(project == null)
            {
                return ProjectErrors.NotFound;
            }

            if(project.OwnerId != _currentUser.Id)
            {
                return UserErrors.Forbidden;
            }

            project.CompleteProject();
            await _projectRepository.UpdateAsync(project, cancellationToken);
            return Result.Success();
        }
    }
}
