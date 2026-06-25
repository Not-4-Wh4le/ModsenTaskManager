using Application.Common.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(
            IUserRepository userRepository)
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email address format");

            RuleFor(c => c.Password)
               .NotEmpty().WithMessage("Password cannot be empty")
               .MinimumLength(6).WithMessage("Password cannot be shorter then 6 characters")
               .Matches(@"[0-9]").WithMessage("Password must contain at least number");      
        }
    }
}
