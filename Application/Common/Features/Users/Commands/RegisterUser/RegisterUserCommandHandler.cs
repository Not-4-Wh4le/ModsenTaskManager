using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Domain;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user != null)
                return UserErrors.EmailNotUnique;

            var passwordHash = passwordHasher.HashPassword(request.Password);
            
            user = new User(Guid.NewGuid(), request.Email, passwordHash);

            await userRepository.AddAsync(user, cancellationToken);
            return user.Id;
        }
    }
}
