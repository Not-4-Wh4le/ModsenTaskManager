using Application.Common.Interfaces.Repositories;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.EventHandlers
{
    internal class TaskDeletedEventHandler : INotificationHandler<TaskDeletedEvent>
    {
        private readonly ILogger<TaskDeletedEventHandler> _logger;
        
        public TaskDeletedEventHandler(ILogger<TaskDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TaskDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                 "Timestamp: [{Timestamp}]\nProject [{Title}] (Id: {ProjectId}) has been deleted",
                 notification.Timestamp,
                 notification.Title,
                 notification.ProjectId);

            return Task.CompletedTask;
        }
    }
}
