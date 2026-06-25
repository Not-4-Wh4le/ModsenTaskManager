using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(
        string Email,
        string Password) : IRequest<Result<Guid>>;
}
