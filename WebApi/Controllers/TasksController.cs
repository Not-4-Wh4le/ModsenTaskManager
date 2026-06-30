using Application.Common.Features.Projects.Queries.GetProjects;
using Application.Common.Features.Tasks.Commands.ChangeTaskStatus;
using Application.Common.Features.Tasks.Commands.CreateTask;
using Application.Common.Features.Tasks.Commands.DeleteTask;
using Application.Common.Features.Tasks.Commands.UpdateTaskDetais;
using Application.Common.Features.Tasks.Queries.GetTaskById;
using Application.Common.Features.Tasks.Queries.GetTasks;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Tasks;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ISender _sender;
        public TasksController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTaskCommand command)
        {
            var result = await _sender.Send(command);

            return result.IsSuccess
                ? Ok(result.Value)
                : this.HandleFailure(result.Error);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskDetailsDto>> GetById(Guid id)
        {
            var result = await _sender.Send(new GetTaskByIdQuery(id));

            return result.IsSuccess
                ? Ok(result.Value)
                : this.HandleFailure(result.Error);
        }

        [HttpGet("search")]
        public async Task<ActionResult<PagedResultDto<TaskDto>>> GetAll([FromQuery] GetTasksQuery query)
        {
            var result = await _sender.Send(query);

            return result.IsSuccess
                ? Ok(result.Value)
                : this.HandleFailure(result.Error);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _sender.Send(new DeleteTaskCommand(id));

            return result.IsSuccess
                ? NoContent()
                : this.HandleFailure(result.Error);
        }

        [Authorize]
        [HttpPut("change-status/{id:guid}")]    
        public async Task<ActionResult> ChangeStatus(Guid id, [FromBody] ChangeTaskStatusRequest request)
        {
            var result = await _sender.Send(new ChangeTaskStatusCommand(id, request.NewStatus));

            return result.IsSuccess
                ? NoContent()
                : this.HandleFailure(result.Error);
        }

        [Authorize]
        [HttpPut("{id:guid}")] 
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request)
        {
            var result = await _sender.Send(new UpdateTaskDetailsCommand(
                Id: id,
                Title: request.Title,
                Description: request.Description,
                DueDate: request.DueDate,
                TaskPriorityLevel: request.TaskPriorityLevel,
                Tags: request.Tags));

            return result.IsSuccess
               ? NoContent()
               : this.HandleFailure(result.Error);
        }       
    }
}
