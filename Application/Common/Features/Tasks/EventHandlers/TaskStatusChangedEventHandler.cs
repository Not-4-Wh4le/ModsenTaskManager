using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.EventHandlers
{
    internal class TaskStatusChangedEventHandler : INotificationHandler<TaskStatusChangedEvent>
    {
        private readonly ILogger<TaskStatusChangedEventHandler> _logger;

        public TaskStatusChangedEventHandler(ILogger<TaskStatusChangedEventHandler> logger)
        {
            _logger = logger; 
        }

        public Task Handle(TaskStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Timestamp: [{Timestamp}]\nTask [{TaskId}] has changed status from [{OldStatus}] to [{NewStatus}]\nAuthor of changes: [{ChangedById}]",
                notification.Timestamp,
                notification.TaskId,
                notification.OldStatus,
                notification.NewStatus,
                notification.ChangedById);

            return Task.CompletedTask;
        }
    }
}
