using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class DomainEventDispathcer : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        public DomainEventDispathcer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task DispatchAndClearAsync(AggregateRoot root, CancellationToken cancellationToken)
        {
            var events = root.Events.ToList();
            
            if(events.Count == 0)
            {
                return;
            }

            root.ClearDomainEvents();

            foreach (var e in events)
            {
                await _mediator.Publish(e, cancellationToken);
            }
        }
    }
}
