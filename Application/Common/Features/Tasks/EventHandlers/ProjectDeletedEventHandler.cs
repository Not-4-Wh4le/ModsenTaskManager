using Application.Common.Interfaces.Repositories;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.EventHandlers
{
    public class ProjectDeletedEventHandler : INotificationHandler<ProjectDeletedEvent>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<ProjectDeletedEventHandler> _logger;
        public ProjectDeletedEventHandler(
            ITaskRepository taskRepository,
            ILogger<ProjectDeletedEventHandler> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task Handle(ProjectDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Timestamp: [{Timestamp}]\nProject [{ProjectName}] (Id: {ProjectId}) has been deleted by user with id [{UserId}]",
                notification.Timestamp,
                notification.ProjectName,
                notification.ProjectId,
                notification.UserId);

            await _taskRepository.DeleteAllByProjectIdAsync(notification.ProjectId, cancellationToken);
        }
    }
}
