using Application.Common.Extensions;
using Application.Common.Features.Projects;
using Application.Common.Features.Users;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Domain;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler
        : IRequestHandler<CreateTaskCommand, Result<Guid>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IProjectRepository _projectRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        
        public CreateTaskCommandHandler(
            ITaskRepository taskRepository,
            ICurrentUserService currentUserService,
            IProjectRepository projectRepository,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _currentUserService = currentUserService;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public async Task<Result<Guid>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken); 
            
            if(project == null)
            {
                return ProjectErrors.NotFound;
            }

            if(_currentUserService.GetUserId() != project.OwnerId)
            {
                return UserErrors.Forbidden;
            }

            var task = new Domain.Entites.Task(
                id: Guid.NewGuid(),
                projectId: request.ProjectId,
                title: request.Title,
                description: request.Description,
                dueDate: request.DueDate,
                priorityLevel: request.PriorityLevel,
                tags: request.Tags);

            await _taskRepository.AddAsync(task, cancellationToken);
            await _domainEventDispatcher.DispatchAndClearAsync(task, cancellationToken);
            return task.Id;
        }
    }
}
