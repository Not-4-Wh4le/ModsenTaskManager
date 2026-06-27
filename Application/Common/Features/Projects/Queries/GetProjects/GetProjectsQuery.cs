using Application.Common.Models;
using Domain.Enums;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Queries.GetProjects
{
    public record GetProjectsQuery(
        string? NameSearch = null,
        ProjectStatus? FilteringStatus = null,
        string? SortBy = null,
        bool IsDescending = false,
        int Page = 1,
        int PageSize = 10) : IRequest<Result<PagedResultDto<ProjectDto>>>; 
}
