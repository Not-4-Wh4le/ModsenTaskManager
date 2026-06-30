using Application.Common.Features.Projects.Commands.CreateProject;
using Application.Common.Features.Projects.Commands.DeleteProject;
using Application.Common.Features.Projects.Commands.UpdateProject;
using Application.Common.Features.Projects.Commands.СompleteProject;
using Application.Common.Features.Projects.Queries.GetProjectById;
using Application.Common.Features.Projects.Queries.GetProjects;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Projects;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProjectsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProjectsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDetailsDto>> GetById(Guid id)
        {
            var result = await _sender.Send(new GetProjectByIdQuery(id));

            return result.IsSuccess
               ? Ok(result.Value)
               : this.HandleFailure(result.Error);

        }

        [HttpGet("search")]
        public async Task<ActionResult<PagedResultDto<ProjectDto>>> GetAll(
            [FromQuery] GetProjectsQuery query)
        {
            var result = await _sender.Send(query);

            return result.IsSuccess
              ? Ok(result.Value)
              : this.HandleFailure(result.Error);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProjectCommand command)
        {
            var result = await _sender.Send(command);

            return result.IsSuccess
                ? Ok(result.Value)
                : this.HandleFailure(result.Error);
        }

        [Authorize]
        [HttpPut("rename/{id:guid}")]
        public async Task<ActionResult> Rename(Guid id, [FromBody] RenameProjectRequest request)
        {
            var result = await _sender.Send(new RenameProjectCommand(id, request.NewName));

            return result.IsSuccess
               ? NoContent()
               : this.HandleFailure(result.Error);
        }

        [Authorize]
        [HttpPut("complete/{id:guid}")]
        public async Task<ActionResult> Complete(Guid id)
        {
            var result = await _sender.Send(new CompleteProjectCommand(id));

            return result.IsSuccess
               ? NoContent()
               : this.HandleFailure(result.Error);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _sender.Send(new DeleteProjectCommand(id));

            return result.IsSuccess
              ? NoContent()
              : this.HandleFailure(result.Error);
        }
    }
}
