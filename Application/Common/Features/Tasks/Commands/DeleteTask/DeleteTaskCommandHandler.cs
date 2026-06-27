using Application.Common.Extensions;
using Application.Common.Features.Projects;
using Application.Common.Features.Users;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler
        : IRequestHandler<DeleteTaskCommand, Result>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProjectRepository _projectRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public DeleteTaskCommandHandler(
            ITaskRepository taskRepository, 
            ICurrentUserService currentUserService,
            IProjectRepository projectRepository,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _taskRepository = taskRepository;
            _currentUserService = currentUserService;
            _projectRepository = projectRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

            if(task == null)
            {
                return TaskErrors.NotFound;
            }

            var project = await _projectRepository.GetByIdAsync(task.ProjectId, cancellationToken);

            if(project == null)
            {
                return ProjectErrors.NotFound;
            }

            if(_currentUserService.GetUserId() != project.OwnerId)
            {
                return UserErrors.Forbidden;
            }

            task.Delete();
            await _taskRepository.DeleteAsync(task.Id, cancellationToken);
            await _domainEventDispatcher.DispatchAndClearAsync(task, cancellationToken);

            return Result.Success();
        }
    }
}
