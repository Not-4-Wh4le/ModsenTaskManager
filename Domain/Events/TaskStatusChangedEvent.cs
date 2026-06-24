using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public record TaskStatusChangedEvent(
        Guid TaskId,
        string Title,
        Enums.TaskStatus OldStatus,
        Enums.TaskStatus NewStatus,
        Guid ChangedById,
        DateTimeOffset Timestamp) : IDomainEvent;
}
