using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Queries.GetTasks
{
    public class GetTasksQueryHandler
        : IRequestHandler<GetTasksQuery, Result<PagedResultDto<TaskDto>>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResultDto<TaskDto>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var (tasks, totalCount)= await _taskRepository.GetPagedAsync(
                titleSearch: request.TitleSearch,
                filteringProjectId: request.FilteringProjectId,
                filteringStatus: request.FilteringStatus,
                startDueDate: request.StartDueDate,
                endDueDate: request.EndDueDate,
                sortBy: request.SortBy,
                tags: request.Tags,
                isDescending: request.IsDescending,
                page: request.Page,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken);

            var dtos = _mapper.Map<IReadOnlyCollection<TaskDto>>(tasks);

            var result = new PagedResultDto<TaskDto>(
                Items: dtos,
                Page: request.Page,
                PageSize: request.PageSize,
                TotalCount: totalCount);
            
            return result;
        }
    }
}
