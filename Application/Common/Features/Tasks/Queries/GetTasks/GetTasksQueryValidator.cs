using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Queries.GetTasks
{
    public class GetTasksQueryValidator : AbstractValidator<GetTasksQuery>
    {
        public GetTasksQueryValidator()
        {
            RuleFor(q => q.Page)
               .GreaterThan(0)
               .WithMessage("Page number must be greater than 0");

            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0")
                .LessThanOrEqualTo(50)
                .WithMessage("Page size cannot exceed 50 items per page");

            RuleFor(q => q.SortBy)
                .Must(s => TaskSortFields.All.Contains(s!.Trim().ToLower(), StringComparer.OrdinalIgnoreCase))
                .When(q => !string.IsNullOrEmpty(q.SortBy))
                .WithMessage($"You can only sort by {string.Join(", ", TaskSortFields.All)}");

            RuleFor(q => q.FilteringStatus)
                .IsInEnum()
                .When(q => q.FilteringProjectId.HasValue)
                .WithMessage("Task status is invalid");

        }
    }
}
