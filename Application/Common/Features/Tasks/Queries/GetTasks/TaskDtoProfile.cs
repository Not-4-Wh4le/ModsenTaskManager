using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Queries.GetTasks
{
    public class TaskDtoProfile : Profile
    {
        private const int MaxTags = 3;
        public TaskDtoProfile()
        {
            CreateMap<Domain.Entites.Task, TaskDto>()
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Take(MaxTags).ToArray())
                );
        }
    }
}
