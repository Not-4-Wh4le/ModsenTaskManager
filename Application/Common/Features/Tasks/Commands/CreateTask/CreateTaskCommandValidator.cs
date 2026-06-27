using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(c => c.ProjectId)
                .NotEmpty()
                .WithMessage("Project Id is required");

            RuleFor(c => c.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MinimumLength(2)
                .WithMessage("Title length cannot be shorter than 2 characters")
                .MaximumLength(100)
                .WithMessage("Title length cannot be longer than 100 characters");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MinimumLength(2)
                .WithMessage("Description length cannot be shorter than 2 characters")
                .MaximumLength(200)
                .WithMessage("Description length cannot be longer than 200 characters");

            RuleFor(c => c.DueDate)
                .NotEmpty()
                .WithMessage("Due date is required")
                .Must(dueDate => dueDate > DateTimeOffset.UtcNow)
                .WithMessage("Due Date cannot be in the past");
        }
    }
}
