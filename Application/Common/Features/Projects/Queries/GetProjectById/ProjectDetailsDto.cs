using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Queries.GetProjectById
{
    public record ProjectDetailsDto(
        Guid Id,
        string Name,
        string OwnerEmail,
        ProjectStatus ProjectStatus,
        DateTimeOffset CreatedAt);
}
