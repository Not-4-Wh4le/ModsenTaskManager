using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public abstract class AggregateRoot
    {
        private List<IDomainEvent> events = new();
        public IReadOnlyCollection<IDomainEvent> Events => (events ??= new()).AsReadOnly();
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            events ??= new();
            events.Add(domainEvent);

        }
           

        public void ClearDomainEvents()
            => events?.Clear();
    }
}
