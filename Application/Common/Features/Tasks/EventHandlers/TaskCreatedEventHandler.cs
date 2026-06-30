using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.EventHandlers
{
    internal class TaskCreatedEventHandler : INotificationHandler<TaskCreatedEvent>
    {
        private readonly ILogger<TaskCreatedEventHandler> _logger;
        public TaskCreatedEventHandler(ILogger<TaskCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TaskCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                 "Timestamp: [{Timestamp}]\nProject [{Title}] (Id: {ProjectId}) has been created",
                 notification.Timestamp,
                 notification.Title,
                 notification.ProjectId);

            return Task.CompletedTask;
        }
    }
}
