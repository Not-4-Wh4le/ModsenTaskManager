using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
        : IRequestHandler<LoginUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user == null)
                return UserErrors.FailedLogin;

            var isCorrectPassword = passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

            if(!isCorrectPassword)
                return UserErrors.FailedLogin;

            var token = tokenService.GenerateToken(user);
            return token;
        }
    }
}
