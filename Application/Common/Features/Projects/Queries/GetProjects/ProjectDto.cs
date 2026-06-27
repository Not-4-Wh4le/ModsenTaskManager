using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Queries.GetProjects
{
    public record ProjectDto(
        Guid Id,
        string Name,
        ProjectStatus ProjectStatus);
}
