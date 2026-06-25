using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Enums;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Project : AggregateRoot, IEntity 
    {
        public Guid Id { get; init; }
        public string Name { get; private set; } = string.Empty;
        public Guid OwnerId { get; init; }
        public ProjectStatus ProjectStatus { get; private set; } = ProjectStatus.Active;
        public DateTimeOffset CreatedAt { get; init; }

        public Project(Guid id, string name, Guid ownerId)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty");

            if (ownerId == Guid.Empty)
                throw new ArgumentException("OwnerId cannot be empty");

            Id = id;
            OwnerId = ownerId;
            CreatedAt = DateTimeOffset.UtcNow;
            ChangeName(name);
            
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name cannot be empty");

            if (name.Length < 2)
                throw new ArgumentException("Name length cannot be shorter than 2 charactes");

            if (name.Length > 100)
                throw new ArgumentException("Name length cannot be longer than 100 characters");

            Name = name.Trim();
        }

        public void CompleteProject()
        {
            if (ProjectStatus == ProjectStatus.Completed)
                throw new InvalidOperationException("Project is already completed");

            ProjectStatus = ProjectStatus.Completed;
        }

        public void Delete()
            => AddDomainEvent(new ProjectDeletedEvent(Id, Name, OwnerId, DateTimeOffset.UtcNow));
        
    }
}
