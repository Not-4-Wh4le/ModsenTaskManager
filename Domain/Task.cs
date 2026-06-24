using Domain.Common;
using Domain.Common.Interfaces;
using Domain.Enums;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Domain
{
    public class Task : AggregateRoot, IEntity
    {
        private readonly List<string> tags = new();
        public Guid Id { get; init; }
        public Guid ProjectId { get; init; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public DateTimeOffset DueDate { get; private set; }
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public IReadOnlyList<string> Tags => tags.AsReadOnly();
        public Enums.TaskStatus TaskStatus { get; private set; } = Enums.TaskStatus.ToDo;
        public TaskPriorityLevel PriorityLevel { get; private set; }

        public Task(
            Guid id, 
            Guid projectId, 
            string title, 
            string description, 
            DateTimeOffset dueDate, 
            TaskPriorityLevel priorityLevel,
            IEnumerable<string>? tags)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Id is required");

            if (projectId == Guid.Empty)
                throw new ArgumentNullException("Project Id is required");

            Id = id;
            ProjectId = projectId;
            PriorityLevel = priorityLevel;
            ChangeTitle(title);
            ChangeDescription(description);
            ChangeDueDate(dueDate);
            ChangePriotityLevel(priorityLevel); 

            if(tags != null)
                foreach (var tag in tags)
                    AddTag(tag);

            AddDomainEvent(new TaskCreatedEvent(Id, ProjectId, Title, CreatedAt));
        }

        public void AddTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentNullException("Tag cannot be empty");

            string normalizedTag = tag.Trim().ToLowerInvariant();
            if(!tags.Contains(normalizedTag))
                tags.Add(normalizedTag);

        }

        public void RemoveTag(string tag)
            => tags.Remove(tag.Trim().ToLowerInvariant());
        

        public void ChangePriotityLevel(TaskPriorityLevel priorityLevel)
            => PriorityLevel = priorityLevel;

        public void ChangeStatus(Enums.TaskStatus newStatus, Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException("User Id is required");

            if (TaskStatus == newStatus)
                return;

            var oldStatus = TaskStatus;
            TaskStatus = newStatus;

            AddDomainEvent(new TaskStatusChangedEvent(Id, Title, oldStatus, newStatus, userId, DateTimeOffset.UtcNow));

        }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("Title is required");

            if (title.Length < 2)
                throw new ArgumentException("Title length cannot be shorter then 2 characters");

            if (title.Length > 100)
                throw new ArgumentException("Title length cannot be longer then 100 characters");

            Title = title.Trim();
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("Description is required");

            if (description.Length < 2)
                throw new ArgumentException("Description length cannot be shorter than 2 characters");

            if (description.Length > 200)
                throw new ArgumentException("Description length cannot be longer than 200 characters");

            Description = description.Trim();
        }

        public void ChangeDueDate(DateTimeOffset dueDate)
        {
            if (dueDate.UtcDateTime < CreatedAt.UtcDateTime)
                throw new InvalidOperationException("Due Date cannot be in the past");

            DueDate = dueDate;
        }

        public void Delete()
        {
            AddDomainEvent(new TaskDeletedEvent(Id, ProjectId, Title, DateTimeOffset.UtcNow));
        }
    }
}
