using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Project name is required")
                .MinimumLength(2).WithMessage("Name length cannot be shorter than 2 characters")
                .MaximumLength(100).WithMessage("Name length cannot be longer than 100 characters");
        }
    }
}
