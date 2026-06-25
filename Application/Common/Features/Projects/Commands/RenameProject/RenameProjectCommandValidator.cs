using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.UpdateProject
{
    public class RenameProjectCommandValidator : AbstractValidator<RenameProjectCommand>
    {
        public RenameProjectCommandValidator()
        {
            RuleFor(c => c.Id)
               .NotEmpty().WithMessage("Id is required");

            RuleFor(c => c.NewName)
                .NotEmpty().WithMessage("Project name is required")
                .MinimumLength(2).WithMessage("Name length cannot be shorter than 2 characters")
                .MaximumLength(100).WithMessage("Name length cannot be longer than 100 characters");
        }
    }
}
