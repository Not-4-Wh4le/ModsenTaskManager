using Application.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdQueryHandler
        : IRequestHandler<GetTaskByIdQuery, Result<TaskDetailsDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        
        public GetTaskByIdQueryHandler(
            ITaskRepository taskRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<Result<TaskDetailsDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);

            if (task == null)
            {
                return TaskErrors.NotFound;
            }

            var dto = _mapper.Map<TaskDetailsDto>(task);

            return dto;
        }
    }
}
