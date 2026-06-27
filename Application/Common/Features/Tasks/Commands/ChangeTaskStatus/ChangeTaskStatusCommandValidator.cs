using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Commands.ChangeTaskStatus
{
    public class ChangeTaskStatusCommandValidator : AbstractValidator<ChangeTaskStatusCommand>
    {
        public ChangeTaskStatusCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Task Id is required");

            RuleFor(c => c.TaskStatus)
                .IsInEnum()
                .WithMessage("A valid task status is required");
        }
    }
}
