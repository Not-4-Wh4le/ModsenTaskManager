using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> events = new();
        
        public void AddDomainEvent(IDomainEvent domainEvent)
            => events.Add(domainEvent);

        public void ClearDomainEvents()
            => events.Clear();
    }
}
