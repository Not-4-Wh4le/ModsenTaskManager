using Domain.Enums;

namespace WebApi.Contracts.Tasks
{
    public record UpdateTaskRequest(
        string Title,
        string Description,
        DateTimeOffset DueDate,
        TaskPriorityLevel TaskPriorityLevel,
        IEnumerable<string>? Tags);
}
