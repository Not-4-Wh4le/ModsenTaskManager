namespace WebApi.Contracts.Tasks
{
    public record ChangeTaskStatusRequest(Domain.Enums.TaskStatus NewStatus);
}
