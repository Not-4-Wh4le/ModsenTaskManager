using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(c => c.Email)
               .NotEmpty().WithMessage("Email is required")
               .EmailAddress().WithMessage("Invalid Email address format");

            RuleFor(c => c.Password)
               .NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}
