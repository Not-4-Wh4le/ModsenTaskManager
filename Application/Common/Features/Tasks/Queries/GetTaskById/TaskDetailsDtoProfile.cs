using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Queries.GetTaskById
{
    public class TaskDetailsDtoProfile : Profile
    {
        public TaskDetailsDtoProfile()
        {
            CreateMap<Domain.Entites.Task, TaskDetailsDto>();
        }
    }
}
