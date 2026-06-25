using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public record ProjectDeletedEvent(
        Guid ProjectId,
        string ProjectName,
        Guid UserId,
        DateTimeOffset Timestamp)
       : IDomainEvent;
}
