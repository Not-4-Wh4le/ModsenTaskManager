using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Commands.СompleteProject
{
    public class CompleteProjectCommandValidator : AbstractValidator<CompleteProjectCommand>
    {
        public CompleteProjectCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
