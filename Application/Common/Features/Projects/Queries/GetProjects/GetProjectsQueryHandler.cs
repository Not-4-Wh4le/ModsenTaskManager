using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Queries.GetProjects
{
    public class GetProjectsQueryHandler
        : IRequestHandler<GetProjectsQuery, Result<PagedResultDto<ProjectDto>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResultDto<ProjectDto>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            string? normalizedSortBy = request.SortBy?.ToLower().Trim();
            string? cleanedSearch = request.NameSearch?.Trim();

            var (projects, totalCount) = await _projectRepository.GetPagedAsync(
                nameSearch: cleanedSearch,
                filteringStatus: request.FilteringStatus,
                sortBy: normalizedSortBy,
                isDescending: request.IsDescending,
                page: request.Page,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken);

            var dtos = _mapper.Map<IReadOnlyCollection<ProjectDto>>(projects);

            var result = new PagedResultDto<ProjectDto>(
                Items: dtos,
                Page: request.Page,
                PageSize: request.PageSize,
                TotalCount: totalCount);

            return result;
        }
    }
}
