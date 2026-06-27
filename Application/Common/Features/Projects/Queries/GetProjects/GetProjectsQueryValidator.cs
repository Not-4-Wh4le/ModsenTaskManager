using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Queries.GetProjects
{
    public class GetProjectsQueryValidator : AbstractValidator<GetProjectsQuery>
    {
        private static readonly string[] AllowedSortFields = ["date", "name"];
        public GetProjectsQueryValidator()
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
                .Must(s => AllowedSortFields.Contains(s!.ToLower().Trim()))
                .When(q => !string.IsNullOrEmpty(q.SortBy))
                .WithMessage($"You can only sort by {string.Join(", ", AllowedSortFields)}");
        }
    }
}
