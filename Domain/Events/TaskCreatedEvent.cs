using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public record TaskCreatedEvent(
        Guid TaskId, 
        Guid ProjectId, 
        string Title, 
        DateTimeOffset Timestamp) : IDomainEvent;
}
