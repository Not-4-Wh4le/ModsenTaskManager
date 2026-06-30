using Application.Common.Features.Users.Commands.LoginUser;
using Application.Common.Features.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;
        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserCommand command)
        {
            var result = await _sender.Send(command);
         
            return result.IsSuccess
                ? Ok(result.Value)
                : this.HandleFailure(result.Error);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _sender.Send(command);

            return result.IsSuccess
                ? Ok(result.Value)
                : this.HandleFailure(result.Error);
        }
    }
}
